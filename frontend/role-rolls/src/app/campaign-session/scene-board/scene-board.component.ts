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
import { catchError, firstValueFrom, of } from 'rxjs';
import { CampaignSessionService } from '@app/campaign-session/campaign-session.service';
import { getCssVar } from '@app/shared/utils/css-variable.utils';

interface SceneBoardHistoryEntry {
  undo: () => Promise<void>;
  redo: () => Promise<void>;
}

interface SceneBoardSnapshot {
  strokes: BoardStroke[];
  tokens: BoardToken[];
}

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
  private readonly undoStack: SceneBoardHistoryEntry[] = [];
  private readonly redoStack: SceneBoardHistoryEntry[] = [];
  private readonly maxUndoEntries = 10;

  public readonly tokenLabelDraft = signal('');

  private readonly viewReady = signal(false);
  private adapter: SceneBoardKonvaAdapter | null = null;
  private resizeObserver: ResizeObserver | null = null;
  private loadedSceneKey: string | null = null;
  private isHistoryReplayInProgress = false;
  private readonly handleWindowKeydown = (event: KeyboardEvent) => {
    if (this.shouldHandleUndoShortcut(event)) {
      event.preventDefault();
      void this.undoLastChange();
      return;
    }

    if (this.shouldHandleRedoShortcut(event)) {
      event.preventDefault();
      void this.redoLastChange();
    }
  };

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
        this.clearUndoHistory();
        this.boardStore.clearState();
        this.adapter?.render(null, null);
        return;
      }

      const sceneKey = `${campaignId}_${scene.id}`;
      if (this.loadedSceneKey === sceneKey) {
        return;
      }

      this.loadedSceneKey = sceneKey;
      this.clearUndoHistory();
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
      onStrokeCreated: points => void this.createStroke(points),
      onStrokeRemoved: strokeId => void this.removeStroke(strokeId),
      onTokenMoved: (tokenId, point) => void this.moveToken(tokenId, point),
      onTokenSelected: tokenId => this.boardStore.selectToken(tokenId),
      onTokenRemoved: tokenId => void this.removeToken(tokenId),
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

        void this.addCreatureToken(request.creature);
      })
    );
    if (typeof window !== 'undefined') {
      window.addEventListener('keydown', this.handleWindowKeydown);
    }
    this.viewReady.set(true);
  }

  public ngOnDestroy(): void {
    this.subscriptionManager.clear();
    this.resizeObserver?.disconnect();
    this.adapter?.destroy();
    if (typeof window !== 'undefined') {
      window.removeEventListener('keydown', this.handleWindowKeydown);
    }
  }

  public setTool(tool: 'select' | 'pen' | 'erase'): void {
    if (tool === 'erase' && !this.isMaster()) {
      return;
    }
    this.boardStore.setTool(tool);
  }

  public async addCreatureToken(creature: Creature): Promise<void> {
    const point = this.defaultSpawnPoint();
    const token = {
      id: uuidv4(),
      creatureId: creature.id,
      label: creature.name,
      x: point.x,
      y: point.y,
      width: BOARD_GRID_SIZE,
      height: BOARD_GRID_SIZE,
      color: creature.category === CreatureCategory.Ally
        ? getCssVar('--p-primary-color')
        : getCssVar('--p-primary-color'),
      zIndex: this.boardStore.document()?.tokens.length ?? 0,
      imageUrl: null,
      locked: false,
    } as BoardToken;

    await this.persistToken(token);
  }

  public async addGenericToken(): Promise<void> {
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

    await this.persistToken(token);
  }

  public selectedToken(): BoardToken | null {
    return this.boardStore.selectedToken();
  }

  public async removeSelectedToken(): Promise<void> {
    const token = this.selectedToken();
    if (token) {
      await this.removeToken(token.id);
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

  public async renameSelectedToken(): Promise<void> {
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

    await this.renameToken(token.id, nextLabel, token.label);
  }

  public async clearBoard(trackHistory: boolean = true): Promise<void> {
    const campaignId = this.campaignId();
    const scene = this.scene();
    const document = this.boardStore.document();
    if (!campaignId || !scene || !document) {
      return;
    }

    const snapshot = this.captureSnapshot(document);
    if (trackHistory && (snapshot.strokes.length > 0 || snapshot.tokens.length > 0) && !this.isHistoryReplayInProgress) {
      this.pushHistoryEntry({
        undo: () => this.restoreSnapshot(snapshot),
        redo: () => this.clearBoard(false)
      });
    }

    const opId = uuidv4();
    this.boardStore.clearBoard(opId);
    const operation = await firstValueFrom(
      this.boardApi.clearBoard(campaignId, scene.id, { opId }).pipe(
        catchError(error => {
          console.error('Failed to clear board', error);
          return of(null);
        })
      )
    );
    if (operation) {
      this.boardStore.acknowledgeOperation(operation);
    }
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

  private async createStroke(points: number[]): Promise<void> {
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

    if (!this.isHistoryReplayInProgress) {
      this.pushHistoryEntry({
        undo: () => this.removeStroke(stroke.id, false),
        redo: () => this.persistStroke(this.cloneStroke(stroke), false)
      });
    }

    this.boardStore.addStroke(stroke, opId);
    const operation = await firstValueFrom(
      this.boardApi.addStroke(campaignId, scene.id, { opId, payload: stroke }).pipe(
        catchError(error => {
          console.error('Failed to persist stroke', error);
          return of(null);
        })
      )
    );
    if (operation) {
      this.boardStore.acknowledgeOperation(operation);
    }
  }

  private async removeStroke(strokeId: string, trackUndo: boolean = true): Promise<void> {
    const campaignId = this.campaignId();
    const scene = this.scene();
    const stroke = this.boardStore.document()?.strokes.find(currentStroke => currentStroke.id === strokeId);
    if (!campaignId || !scene || !stroke) {
      return;
    }

    if (trackUndo && !this.isHistoryReplayInProgress) {
      const strokeSnapshot = this.cloneStroke(stroke);
      this.pushHistoryEntry({
        undo: () => this.persistStroke(strokeSnapshot, false),
        redo: () => this.removeStroke(strokeId, false)
      });
    }

    const opId = uuidv4();
    this.boardStore.removeStroke(strokeId, opId);
    const operation = await firstValueFrom(
      this.boardApi.removeStroke(campaignId, scene.id, strokeId, { opId }).pipe(
        catchError(error => {
          console.error('Failed to remove stroke', error);
          return of(null);
        })
      )
    );
    if (operation) {
      this.boardStore.acknowledgeOperation(operation);
    }
  }

  private async moveToken(tokenId: string, point: BoardPoint): Promise<void> {
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

    const nextPosition = {
      x: point.x,
      y: point.y,
    };

    if (token.x === nextPosition.x && token.y === nextPosition.y) {
      return;
    }

    await this.persistTokenPosition(
      tokenId,
      nextPosition,
      {
        x: token.x,
        y: token.y,
      });
  }

  private async removeToken(tokenId: string, trackUndo: boolean = true): Promise<void> {
    const campaignId = this.campaignId();
    const scene = this.scene();
    const token = this.boardStore.document()?.tokens.find(currentToken => currentToken.id === tokenId);
    if (!campaignId || !scene || !token) {
      return;
    }

    if (trackUndo && !this.isHistoryReplayInProgress) {
      const tokenSnapshot = this.cloneToken(token);
      this.pushHistoryEntry({
        undo: () => this.persistToken(tokenSnapshot, null, false),
        redo: () => this.removeToken(tokenId, false)
      });
    }

    const opId = uuidv4();
    this.boardStore.removeToken(tokenId, opId);
    const operation = await firstValueFrom(
      this.boardApi.removeToken(campaignId, scene.id, tokenId, { opId }).pipe(
        catchError(error => {
          console.error('Failed to remove token', error);
          return of(null);
        })
      )
    );
    if (operation) {
      this.boardStore.acknowledgeOperation(operation);
    }
  }

  private async persistToken(
    token: BoardToken,
    previousToken: BoardToken | null = this.boardStore.document()?.tokens.find(currentToken => currentToken.id === token.id) ?? null,
    trackUndo: boolean = true
  ): Promise<void> {
    const campaignId = this.campaignId();
    const scene = this.scene();
    if (!campaignId || !scene) {
      return;
    }

    if (trackUndo && !this.isHistoryReplayInProgress) {
      if (previousToken) {
        const tokenSnapshot = this.cloneToken(previousToken);
        const nextToken = this.cloneToken(token);
        this.pushHistoryEntry({
          undo: () => this.persistToken(tokenSnapshot, null, false),
          redo: () => this.persistToken(nextToken, null, false)
        });
      } else {
        const createdToken = this.cloneToken(token);
        this.pushHistoryEntry({
          undo: () => this.removeToken(token.id, false),
          redo: () => this.persistToken(createdToken, null, false)
        });
      }
    }

    const opId = uuidv4();
    this.boardStore.upsertToken(token, opId);
    const operation = await firstValueFrom(
      this.boardApi.upsertToken(campaignId, scene.id, { opId, payload: token }).pipe(
        catchError(error => {
          console.error('Failed to persist token', error);
          return of(null);
        })
      )
    );
    if (operation) {
      this.boardStore.acknowledgeOperation(operation);
    }
  }

  private async persistTokenPosition(
    tokenId: string,
    position: BoardPoint,
    previousPosition: BoardPoint,
    trackUndo: boolean = true
  ): Promise<void> {
    const campaignId = this.campaignId();
    const scene = this.scene();
    if (!campaignId || !scene) {
      return;
    }

    if (trackUndo && !this.isHistoryReplayInProgress) {
      const previousPoint = { ...previousPosition };
      const nextPoint = { ...position };
      this.pushHistoryEntry({
        undo: () => this.persistTokenPosition(tokenId, previousPoint, nextPoint, false),
        redo: () => this.persistTokenPosition(tokenId, nextPoint, previousPoint, false)
      });
    }

    const opId = uuidv4();
    this.boardStore.moveToken(tokenId, position, opId);
    const operation = await firstValueFrom(
      this.boardApi.moveToken(campaignId, scene.id, tokenId, { opId, payload: position }).pipe(
        catchError(error => {
          console.error('Failed to move token', error);
          return of(null);
        })
      )
    );
    if (operation) {
      this.boardStore.acknowledgeOperation(operation);
    }
  }

  private async renameToken(
    tokenId: string,
    label: string,
    previousLabel: string,
    trackUndo: boolean = true
  ): Promise<void> {
    const campaignId = this.campaignId();
    const scene = this.scene();
    if (!campaignId || !scene) {
      return;
    }

    if (trackUndo && !this.isHistoryReplayInProgress) {
      this.pushHistoryEntry({
        undo: () => this.renameToken(tokenId, previousLabel, label, false),
        redo: () => this.renameToken(tokenId, label, previousLabel, false)
      });
    }

    const opId = uuidv4();
    this.boardStore.renameToken(tokenId, label, opId);
    const operation = await firstValueFrom(
      this.boardApi.renameToken(campaignId, scene.id, tokenId, { opId, payload: { label } }).pipe(
        catchError(error => {
          console.error('Failed to rename token', error);
          return of(null);
        })
      )
    );
    if (operation) {
      this.boardStore.acknowledgeOperation(operation);
    }
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

  private async persistStroke(stroke: BoardStroke, trackUndo: boolean = true): Promise<void> {
    const campaignId = this.campaignId();
    const scene = this.scene();
    if (!campaignId || !scene) {
      return;
    }

    if (trackUndo && !this.isHistoryReplayInProgress) {
      const strokeSnapshot = this.cloneStroke(stroke);
      this.pushHistoryEntry({
        undo: () => this.removeStroke(stroke.id, false),
        redo: () => this.persistStroke(strokeSnapshot, false)
      });
    }

    const opId = uuidv4();
    this.boardStore.addStroke(stroke, opId);
    const operation = await firstValueFrom(
      this.boardApi.addStroke(campaignId, scene.id, { opId, payload: stroke }).pipe(
        catchError(error => {
          console.error('Failed to persist stroke', error);
          return of(null);
        })
      )
    );
    if (operation) {
      this.boardStore.acknowledgeOperation(operation);
    }
  }

  private captureSnapshot(document: ReturnType<SceneBoardService['document']>): SceneBoardSnapshot {
    return {
      strokes: (document?.strokes ?? []).map(stroke => this.cloneStroke(stroke)),
      tokens: (document?.tokens ?? []).map(token => this.cloneToken(token)),
    };
  }

  private async restoreSnapshot(snapshot: SceneBoardSnapshot): Promise<void> {
    for (const stroke of snapshot.strokes) {
      await this.persistStroke(this.cloneStroke(stroke), false);
    }

    for (const token of snapshot.tokens) {
      await this.persistToken(this.cloneToken(token), null, false);
    }
  }

  private cloneStroke(stroke: BoardStroke): BoardStroke {
    return {
      ...stroke,
      points: [...stroke.points],
    };
  }

  private cloneToken(token: BoardToken): BoardToken {
    return {
      ...token,
    };
  }

  private pushHistoryEntry(entry: SceneBoardHistoryEntry): void {
    this.redoStack.length = 0;
    this.pushStackEntry(this.undoStack, entry);
  }

  private clearUndoHistory(): void {
    this.undoStack.length = 0;
    this.redoStack.length = 0;
  }

  private async undoLastChange(): Promise<void> {
    if (this.readOnly() || this.undoStack.length === 0 || this.isHistoryReplayInProgress) {
      return;
    }

    const entry = this.undoStack.pop();
    if (!entry) {
      return;
    }

    this.isHistoryReplayInProgress = true;
    try {
      await entry.undo();
      this.pushStackEntry(this.redoStack, entry);
    } catch (error) {
      console.error('Failed to undo board action', error);
      this.undoStack.push(entry);
    } finally {
      this.isHistoryReplayInProgress = false;
    }
  }

  private async redoLastChange(): Promise<void> {
    if (this.readOnly() || this.redoStack.length === 0 || this.isHistoryReplayInProgress) {
      return;
    }

    const entry = this.redoStack.pop();
    if (!entry) {
      return;
    }

    this.isHistoryReplayInProgress = true;
    try {
      await entry.redo();
      this.pushStackEntry(this.undoStack, entry);
    } catch (error) {
      console.error('Failed to redo board action', error);
      this.redoStack.push(entry);
    } finally {
      this.isHistoryReplayInProgress = false;
    }
  }

  private shouldHandleUndoShortcut(event: KeyboardEvent): boolean {
    const isUndoShortcut = (event.ctrlKey || event.metaKey)
      && !event.shiftKey
      && event.key.toLowerCase() === 'z';

    if (!isUndoShortcut) {
      return false;
    }

    const target = event.target as HTMLElement | null;
    if (!target) {
      return true;
    }

    return !this.isTextEditingTarget(target);
  }

  private shouldHandleRedoShortcut(event: KeyboardEvent): boolean {
    const key = event.key.toLowerCase();
    const isRedoShortcut = (
      ((event.ctrlKey || event.metaKey) && event.shiftKey && key === 'z')
      || (event.ctrlKey && !event.metaKey && !event.shiftKey && key === 'y')
    );

    if (!isRedoShortcut) {
      return false;
    }

    const target = event.target as HTMLElement | null;
    if (!target) {
      return true;
    }

    return !this.isTextEditingTarget(target);
  }

  private pushStackEntry(stack: SceneBoardHistoryEntry[], entry: SceneBoardHistoryEntry): void {
    stack.push(entry);
    if (stack.length > this.maxUndoEntries) {
      stack.splice(0, stack.length - this.maxUndoEntries);
    }
  }

  private isTextEditingTarget(target: HTMLElement): boolean {
    const tagName = target.tagName;
    return target.isContentEditable
      || tagName === 'INPUT'
      || tagName === 'TEXTAREA'
      || tagName === 'SELECT';
  }
}
