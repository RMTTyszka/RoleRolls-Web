import {
  AfterViewInit,
  Component,
  ElementRef,
  ViewChild,
  effect,
  inject,
  input,
  signal
} from '@angular/core';
import { NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ButtonDirective } from 'primeng/button';
import { CampaignScene } from '@app/campaigns/models/campaign-scene-model';
import { CreatureCategory } from '@app/campaigns/models/CreatureCategory';
import { Creature } from '@app/campaigns/models/creature';
import { SceneNotificationService } from '@app/campaign-session/services/scene-notification.service';
import { SubscriptionManager } from '@app/tokens/subscription-manager';
import { v4 as uuidv4 } from 'uuid';
import {
  BOARD_GRID_SIZE,
  BoardPoint,
  BoardStroke,
  BoardToken,
  createEmptySceneBoardDocument,
  snapPointToGrid,
  snapTokenCoordinate
} from '@app/campaign-session/scene-board/scene-board.models';
import { SceneBoardService } from '@app/campaign-session/scene-board/scene-board.service';
import { SceneBoardApiService } from '@app/campaign-session/scene-board/scene-board-api.service';
import { SceneBoardKonvaAdapter } from '@app/campaign-session/scene-board/scene-board-konva.adapter';
import { AuthenticationService } from '@app/authentication/services/authentication.service';
import { catchError, EMPTY, of } from 'rxjs';
import { CampaignSessionService } from '@app/campaign-session/campaign-session.service';

@Component({
  selector: 'rr-scene-board',
  imports: [
    ButtonDirective,
    FormsModule,
    NgIf,
  ],
  providers: [SceneBoardService],
  templateUrl: './scene-board.component.html',
  styleUrl: './scene-board.component.scss'
})
export class SceneBoardComponent implements AfterViewInit {
  public readonly campaignId = input<string | null>(null);
  public readonly scene = input<CampaignScene | null>(null);
  public readonly isMaster = input(false);
  public readonly readOnly = input(false);

  @ViewChild('stageHost', { static: true })
  private readonly stageHost?: ElementRef<HTMLDivElement>;

  public readonly boardStore = inject(SceneBoardService);

  private readonly boardApi = inject(SceneBoardApiService);
  private readonly sceneNotificationService = inject(SceneNotificationService);
  private readonly authenticationService = inject(AuthenticationService);
  private readonly campaignSessionService = inject(CampaignSessionService);
  private readonly subscriptionManager = new SubscriptionManager();

  public readonly tokenLabelDraft = signal('');

  private readonly viewReady = signal(false);
  private adapter: SceneBoardKonvaAdapter | null = null;
  private resizeObserver: ResizeObserver | null = null;
  private loadedSceneKey: string | null = null;

  constructor() {
    effect(() => {
      const document = this.boardStore.document();
      const selectedTokenId = this.boardStore.selectedTokenId();
      const readOnly = this.readOnly();
      if (!this.viewReady() || !this.adapter) {
        return;
      }

      this.adapter.setEditable(!readOnly);
      this.adapter.render(document, selectedTokenId);
    });

    effect(() => {
      const scene = this.scene();
      const campaignId = this.campaignId();
      if (!this.viewReady()) {
        return;
      }

      if (!scene || !campaignId) {
        this.loadedSceneKey = null;
        this.boardStore.clearState();
        this.adapter?.render(null, null);
        return;
      }

      const sceneKey = `${campaignId}_${scene.id}`;
      if (this.loadedSceneKey === sceneKey) {
        return;
      }

      this.loadedSceneKey = sceneKey;
      this.loadScene(campaignId, scene);
    });

    effect(() => {
      const selectedToken = this.boardStore.selectedToken();
      this.tokenLabelDraft.set(selectedToken?.label ?? '');
    });
  }

  public ngAfterViewInit(): void {
    const host = this.stageHost?.nativeElement;
    if (!host) {
      return;
    }

    this.adapter = new SceneBoardKonvaAdapter(host, {
      canEdit: () => !this.readOnly(),
      getTool: () => this.boardStore.tool(),
      getStrokeColor: () => this.boardStore.strokeColor(),
      getStrokeWidth: () => this.boardStore.strokeWidth(),
      onStrokeCreated: points => this.createStroke(points),
      onStrokeRemoved: strokeId => this.removeStroke(strokeId),
      onTokenMoved: (tokenId, point) => this.moveToken(tokenId, point),
      onTokenSelected: tokenId => this.boardStore.selectToken(tokenId),
      onTokenRemoved: tokenId => this.removeToken(tokenId),
    });

    this.resizeObserver = new ResizeObserver(entries => {
      const entry = entries[0];
      if (!entry) {
        return;
      }

      this.adapter?.resize(entry.contentRect.width, entry.contentRect.height);
    });
    this.resizeObserver.observe(host);

    this.subscriptionManager.add(
      'boardOperationReceived',
      this.sceneNotificationService.boardOperationReceived.subscribe(operation => {
        const currentScene = this.scene();
        if (currentScene && operation.sceneId === currentScene.id) {
          this.boardStore.applyRemoteOperation(operation);
        }
      })
    );

    this.subscriptionManager.add(
      'creatureTokenRequested',
      this.campaignSessionService.creatureTokenRequested.subscribe(request => {
        const currentScene = this.scene();
        if (!currentScene || currentScene.id !== request.sceneId || this.readOnly()) {
          return;
        }

        this.addCreatureToken(request.creature);
      })
    );
    this.viewReady.set(true);
  }

  public ngOnDestroy(): void {
    this.subscriptionManager.clear();
    this.resizeObserver?.disconnect();
    this.adapter?.destroy();
  }

  public setTool(tool: 'select' | 'pen' | 'erase'): void {
    this.boardStore.setTool(tool);
  }

  public addCreatureToken(creature: Creature): void {
    const point = this.defaultSpawnPoint();
    const token = {
      id: uuidv4(),
      creatureId: creature.id,
      label: creature.name,
      x: point.x,
      y: point.y,
      width: BOARD_GRID_SIZE,
      height: BOARD_GRID_SIZE,
      color: creature.category === CreatureCategory.Ally ? '#2563eb' : '#dc2626',
      zIndex: this.boardStore.document()?.tokens.length ?? 0,
      imageUrl: null,
      locked: false,
    } as BoardToken;

    this.persistToken(token);
  }

  public addGenericToken(): void {
    const point = this.defaultSpawnPoint();
    const token = {
      id: uuidv4(),
      creatureId: null,
      label: `Marker ${(this.boardStore.document()?.tokens.length ?? 0) + 1}`,
      x: point.x,
      y: point.y,
      width: BOARD_GRID_SIZE,
      height: BOARD_GRID_SIZE,
      color: '#0f766e',
      zIndex: this.boardStore.document()?.tokens.length ?? 0,
      imageUrl: null,
      locked: false,
    } as BoardToken;

    this.persistToken(token);
  }

  public selectedToken(): BoardToken | null {
    return this.boardStore.selectedToken();
  }

  public removeSelectedToken(): void {
    const token = this.selectedToken();
    if (token) {
      this.removeToken(token.id);
    }
  }

  public canRenameSelectedToken(): boolean {
    const token = this.selectedToken();
    if (!token) {
      return false;
    }

    const nextLabel = this.tokenLabelDraft().trim();
    return nextLabel.length > 0 && nextLabel !== token.label;
  }

  public renameSelectedToken(): void {
    const token = this.selectedToken();
    if (!token || this.readOnly()) {
      return;
    }

    const nextLabel = this.tokenLabelDraft().trim();
    if (!nextLabel) {
      this.tokenLabelDraft.set(token.label);
      return;
    }

    if (nextLabel === token.label) {
      return;
    }

    this.persistToken({
      ...token,
      label: nextLabel,
    });
  }

  public clearBoard(): void {
    const campaignId = this.campaignId();
    const scene = this.scene();
    if (!campaignId || !scene) {
      return;
    }

    const opId = uuidv4();
    this.boardStore.clearBoard(opId);
    this.boardApi.clearBoard(campaignId, scene.id, { opId })
      .pipe(
        catchError(error => {
          console.error('Failed to clear board', error);
          return EMPTY;
        })
      )
      .subscribe(operation => this.boardStore.acknowledgeOperation(operation));
  }

  private loadScene(campaignId: string, scene: CampaignScene): void {
    this.sceneNotificationService.joinScene(scene.id);

    this.boardApi.getBoard(campaignId, scene.id).pipe(
      catchError(error => {
        console.error('Failed to load board state', error);
        return of(createEmptySceneBoardDocument(scene.id));
      })
    ).subscribe(result => {
      const currentScene = this.scene();
      if (!currentScene || currentScene.id !== scene.id) {
        return;
      }

      this.boardStore.load(result);
    });
  }

  private createStroke(points: number[]): void {
    const campaignId = this.campaignId();
    const scene = this.scene();
    if (!campaignId || !scene) {
      return;
    }

    const opId = uuidv4();
    const stroke = {
      id: uuidv4(),
      color: this.boardStore.strokeColor(),
      width: this.boardStore.strokeWidth(),
      points,
      createdAt: new Date().toISOString(),
      createdBy: this.authenticationService.userId ?? 'unknown',
    } as BoardStroke;

    this.boardStore.addStroke(stroke, opId);
    this.boardApi.addStroke(campaignId, scene.id, { opId, payload: stroke })
      .pipe(
        catchError(error => {
          console.error('Failed to persist stroke', error);
          return EMPTY;
        })
      )
      .subscribe(operation => this.boardStore.acknowledgeOperation(operation));
  }

  private removeStroke(strokeId: string): void {
    const campaignId = this.campaignId();
    const scene = this.scene();
    if (!campaignId || !scene) {
      return;
    }

    const opId = uuidv4();
    this.boardStore.removeStroke(strokeId, opId);
    this.boardApi.removeStroke(campaignId, scene.id, strokeId, { opId })
      .pipe(
        catchError(error => {
          console.error('Failed to remove stroke', error);
          return EMPTY;
        })
      )
      .subscribe(operation => this.boardStore.acknowledgeOperation(operation));
  }

  private moveToken(tokenId: string, point: BoardPoint): void {
    const campaignId = this.campaignId();
    const scene = this.scene();
    const document = this.boardStore.document();
    if (!campaignId || !scene || !document) {
      return;
    }

    const token = document.tokens.find(currentToken => currentToken.id === tokenId);
    if (!token) {
      return;
    }

    const opId = uuidv4();
    const updatedToken = {
      ...token,
      x: point.x,
      y: point.y,
    } as BoardToken;

    this.boardStore.upsertToken(updatedToken, opId);
    this.boardApi.upsertToken(campaignId, scene.id, { opId, payload: updatedToken })
      .pipe(
        catchError(error => {
          console.error('Failed to move token', error);
          return EMPTY;
        })
      )
      .subscribe(operation => this.boardStore.acknowledgeOperation(operation));
  }

  private removeToken(tokenId: string): void {
    const campaignId = this.campaignId();
    const scene = this.scene();
    if (!campaignId || !scene) {
      return;
    }

    const opId = uuidv4();
    this.boardStore.removeToken(tokenId, opId);
    this.boardApi.removeToken(campaignId, scene.id, tokenId, { opId })
      .pipe(
        catchError(error => {
          console.error('Failed to remove token', error);
          return EMPTY;
        })
      )
      .subscribe(operation => this.boardStore.acknowledgeOperation(operation));
  }

  private persistToken(token: BoardToken): void {
    const campaignId = this.campaignId();
    const scene = this.scene();
    if (!campaignId || !scene) {
      return;
    }

    const opId = uuidv4();
    this.boardStore.upsertToken(token, opId);
    this.boardApi.upsertToken(campaignId, scene.id, { opId, payload: token })
      .pipe(
        catchError(error => {
          console.error('Failed to persist token', error);
          return EMPTY;
        })
      )
      .subscribe(operation => this.boardStore.acknowledgeOperation(operation));
  }

  private defaultSpawnPoint(): BoardPoint {
    const center = snapPointToGrid(this.adapter?.viewportCenter() ?? { x: 180, y: 180 });
    const tokenCount = this.boardStore.document()?.tokens.length ?? 0;
    const offset = (tokenCount % 4) * BOARD_GRID_SIZE;

    return {
      x: snapTokenCoordinate(center.x + offset),
      y: snapTokenCoordinate(center.y + offset),
    };
  }
}
