import { Injectable, signal } from '@angular/core';
import {
  BoardOperationEnvelope,
  BoardTokenLockChangedPayload,
  BoardTokenMovedPayload,
  BoardTokenRenamedPayload,
  BoardPoint,
  BoardStroke,
  BoardToken,
  BoardTool,
  SceneBoardDocument,
  normalizeBoardToken,
  normalizeSceneBoardDocument
} from '@app/campaign-session/scene-board/scene-board.models';

@Injectable()
export class SceneBoardService {
  public readonly document = signal<SceneBoardDocument | null>(null);
  public readonly tool = signal<BoardTool>('select');
  public readonly strokeColor = signal('#111111');
  public readonly strokeWidth = signal(4);
  public readonly selectedTokenId = signal<string | null>(null);

  private readonly appliedOperationIds = new Set<string>();

  public load(document: SceneBoardDocument): void {
    this.appliedOperationIds.clear();
    this.document.set(normalizeSceneBoardDocument(document));
    this.selectedTokenId.set(null);
  }

  public clearState(): void {
    this.appliedOperationIds.clear();
    this.document.set(null);
    this.selectedTokenId.set(null);
    this.tool.set('select');
  }

  public setTool(tool: BoardTool): void {
    this.tool.set(tool);
    if (tool !== 'select') {
      this.selectedTokenId.set(null);
    }
  }

  public setStrokeColor(color: string): void {
    this.strokeColor.set(color);
  }

  public setStrokeWidth(width: number): void {
    this.strokeWidth.set(Math.max(1, Math.round(width)));
  }

  public selectToken(tokenId: string | null): void {
    this.selectedTokenId.set(tokenId);
  }

  public selectedToken(): BoardToken | null {
    const document = this.document();
    const selectedTokenId = this.selectedTokenId();
    if (!document || !selectedTokenId) {
      return null;
    }
    return document.tokens.find(token => token.id === selectedTokenId) ?? null;
  }

  public addStroke(stroke: BoardStroke, opId?: string, version?: number): void {
    this.rememberOperation(opId);
    this.updateDocument(version, document => {
      document.strokes = [...document.strokes, { ...stroke, points: [...stroke.points] }];
    });
  }

  public removeStroke(strokeId: string, opId?: string, version?: number): void {
    this.rememberOperation(opId);
    this.updateDocument(version, document => {
      document.strokes = document.strokes.filter(stroke => stroke.id !== strokeId);
    });
  }

  public upsertToken(token: BoardToken, opId?: string, version?: number): void {
    this.rememberOperation(opId);
    const normalizedToken = normalizeBoardToken(token);
    this.updateDocument(version, document => {
      const index = document.tokens.findIndex(currentToken => currentToken.id === normalizedToken.id);
      if (index >= 0) {
        const tokens = [...document.tokens];
        tokens[index] = normalizedToken;
        document.tokens = tokens;
      } else {
        document.tokens = [...document.tokens, normalizedToken];
      }
      document.tokens.sort((left, right) => left.zIndex - right.zIndex);
    });
  }

  public moveToken(tokenId: string, position: BoardPoint, opId?: string, version?: number): void {
    this.rememberOperation(opId);
    this.updateDocument(version, document => {
      document.tokens = document.tokens.map(token =>
        token.id === tokenId
          ? normalizeBoardToken({
              ...token,
              x: position.x,
              y: position.y,
            })
          : token
      );
    });
  }

  public renameToken(tokenId: string, label: string, opId?: string, version?: number): void {
    this.rememberOperation(opId);
    this.updateDocument(version, document => {
      document.tokens = document.tokens.map(token =>
        token.id === tokenId
          ? {
              ...token,
              label,
            }
          : token
      );
    });
  }

  public setTokenLocked(tokenId: string, locked: boolean, opId?: string, version?: number): void {
    this.rememberOperation(opId);
    this.updateDocument(version, document => {
      document.tokens = document.tokens.map(token =>
        token.id === tokenId
          ? {
              ...token,
              locked,
            }
          : token
      );
    });
  }

  public removeToken(tokenId: string, opId?: string, version?: number): void {
    this.rememberOperation(opId);
    this.updateDocument(version, document => {
      document.tokens = document.tokens.filter(token => token.id !== tokenId);
      if (this.selectedTokenId() === tokenId) {
        this.selectedTokenId.set(null);
      }
    });
  }

  public clearBoard(opId?: string, version?: number): void {
    this.rememberOperation(opId);
    this.updateDocument(version, document => {
      document.strokes = [];
      document.tokens = [];
      this.selectedTokenId.set(null);
    });
  }

  public acknowledgeOperation(operation: BoardOperationEnvelope<unknown> | null | undefined): void {
    if (!operation?.opId) {
      return;
    }

    this.appliedOperationIds.add(operation.opId);
    this.updateDocument(operation.version, () => undefined);
  }

  public applyRemoteOperation(operation: BoardOperationEnvelope<unknown>): void {
    if (!operation) {
      return;
    }

    if (this.appliedOperationIds.has(operation.opId)) {
      this.acknowledgeOperation(operation);
      return;
    }

    switch (operation.kind) {
      case 'stroke-added':
        this.addStroke(operation.payload as BoardStroke, operation.opId, operation.version);
        return;
      case 'stroke-removed': {
        const payload = operation.payload as { strokeId?: string } | string;
        const strokeId = typeof payload === 'string' ? payload : payload?.strokeId;
        if (strokeId) {
          this.removeStroke(strokeId, operation.opId, operation.version);
        }
        return;
      }
      case 'token-upserted':
        this.upsertToken(operation.payload as BoardToken, operation.opId, operation.version);
        return;
      case 'token-moved': {
        const payload = operation.payload as BoardTokenMovedPayload;
        if (payload?.tokenId) {
          this.moveToken(payload.tokenId, payload, operation.opId, operation.version);
        }
        return;
      }
      case 'token-renamed': {
        const payload = operation.payload as BoardTokenRenamedPayload;
        if (payload?.tokenId) {
          this.renameToken(payload.tokenId, payload.label, operation.opId, operation.version);
        }
        return;
      }
      case 'token-lock-changed': {
        const payload = operation.payload as BoardTokenLockChangedPayload;
        if (payload?.tokenId) {
          this.setTokenLocked(payload.tokenId, payload.locked, operation.opId, operation.version);
        }
        return;
      }
      case 'token-removed': {
        const payload = operation.payload as { tokenId?: string } | string;
        const tokenId = typeof payload === 'string' ? payload : payload?.tokenId;
        if (tokenId) {
          this.removeToken(tokenId, operation.opId, operation.version);
        }
        return;
      }
      case 'board-cleared':
        this.clearBoard(operation.opId, operation.version);
        return;
      default:
        return;
    }
  }

  private rememberOperation(opId?: string): void {
    if (opId) {
      this.appliedOperationIds.add(opId);
    }
  }

  private updateDocument(
    version: number | undefined,
    mutator: (document: SceneBoardDocument) => void
  ): void {
    const currentDocument = this.document();
    if (!currentDocument) {
      return;
    }

    const nextDocument = normalizeSceneBoardDocument(currentDocument);
    mutator(nextDocument);
    nextDocument.version = version !== undefined
      ? Math.max(version, currentDocument.version)
      : Math.max(nextDocument.version, currentDocument.version) + 1;
    this.document.set(nextDocument);
  }
}
