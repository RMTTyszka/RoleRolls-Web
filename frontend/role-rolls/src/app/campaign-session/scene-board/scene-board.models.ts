export type BoardTool = 'select' | 'pen' | 'erase';

export type BoardOperationKind =
  | 'stroke-added'
  | 'stroke-removed'
  | 'token-upserted'
  | 'token-removed'
  | 'board-cleared';

export interface BoardPoint {
  x: number;
  y: number;
}

export interface BoardViewport {
  x: number;
  y: number;
  scale: number;
}

export interface BoardStroke {
  id: string;
  color: string;
  width: number;
  points: number[];
  createdAt: string;
  createdBy: string;
}

export interface BoardToken {
  id: string;
  label: string;
  x: number;
  y: number;
  width: number;
  height: number;
  color: string;
  zIndex: number;
  imageUrl?: string | null;
  creatureId?: string | null;
  locked?: boolean;
}

export const BOARD_GRID_SIZE = 60;

export interface SceneBoardDocument {
  sceneId: string;
  version: number;
  viewport: BoardViewport;
  strokes: BoardStroke[];
  tokens: BoardToken[];
  backgroundUrl?: string | null;
}

export interface BoardCommand<TPayload> {
  opId: string;
  payload: TPayload;
}

export interface BoardDeleteCommand {
  opId: string;
}

export interface BoardOperationEnvelope<TPayload = unknown> {
  sceneId: string;
  version: number;
  opId: string;
  kind: BoardOperationKind;
  payload: TPayload;
}

export function createEmptySceneBoardDocument(sceneId: string): SceneBoardDocument {
  return {
    sceneId,
    version: 0,
    viewport: {
      x: 0,
      y: 0,
      scale: 1,
    },
    strokes: [],
    tokens: [],
    backgroundUrl: null,
  };
}

export function snapTokenCoordinate(value: number): number {
  const tokenRadius = BOARD_GRID_SIZE / 2;
  return Math.round((value - tokenRadius) / BOARD_GRID_SIZE) * BOARD_GRID_SIZE + tokenRadius;
}

export function snapPointToGrid(point: BoardPoint): BoardPoint {
  return {
    x: snapTokenCoordinate(point.x),
    y: snapTokenCoordinate(point.y),
  };
}

export function normalizeBoardToken(token: BoardToken): BoardToken {
  const snappedPosition = snapPointToGrid({
    x: token.x,
    y: token.y,
  });

  return {
    ...token,
    x: snappedPosition.x,
    y: snappedPosition.y,
    width: BOARD_GRID_SIZE,
    height: BOARD_GRID_SIZE,
  };
}

export function normalizeSceneBoardDocument(
  document: SceneBoardDocument | null | undefined,
  sceneId?: string | null
): SceneBoardDocument {
  const fallback = createEmptySceneBoardDocument(sceneId ?? document?.sceneId ?? '');
  if (!document) {
    return fallback;
  }

  return {
    sceneId: document.sceneId ?? fallback.sceneId,
    version: document.version ?? 0,
    viewport: {
      x: document.viewport?.x ?? 0,
      y: document.viewport?.y ?? 0,
      scale: document.viewport?.scale ?? 1,
    },
    strokes: (document.strokes ?? []).map(stroke => ({
      ...stroke,
      points: [...(stroke.points ?? [])],
    })),
    tokens: [...(document.tokens ?? [])].map(token => normalizeBoardToken(token)),
    backgroundUrl: document.backgroundUrl ?? null,
  };
}
