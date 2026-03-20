import Konva from 'konva';
import {
  BOARD_GRID_SIZE,
  BoardPoint,
  BoardToken,
  BoardTool,
  SceneBoardDocument,
  snapPointToGrid
} from '@app/campaign-session/scene-board/scene-board.models';

const ZOOM_FACTOR = 1.05;
const MIN_SCALE = 0.6;
const MAX_SCALE = 2.5;
const BOARD_SURFACE_COLOR = '#e2e8f0';
const BOARD_GRID_COLOR = '#c2ccd8';

export interface SceneBoardKonvaAdapterOptions {
  canEdit: () => boolean;
  getTool: () => BoardTool;
  getStrokeColor: () => string;
  getStrokeWidth: () => number;
  onStrokeCreated: (points: number[]) => void;
  onStrokeRemoved: (strokeId: string) => void;
  onTokenMoved: (tokenId: string, point: BoardPoint) => void;
  onTokenSelected: (tokenId: string | null) => void;
  onTokenRemoved: (tokenId: string) => void;
}

export class SceneBoardKonvaAdapter {
  private readonly stage: Konva.Stage;
  private readonly backgroundLayer = new Konva.Layer();
  private readonly drawingLayer = new Konva.Layer();
  private readonly tokenLayer = new Konva.Layer();
  private readonly overlayLayer = new Konva.Layer();
  private isEditable = true;
  private currentDocument: SceneBoardDocument | null = null;
  private selectedTokenId: string | null = null;
  private isDrawing = false;
  private previewPoints: number[] = [];
  private previewLine: Konva.Line | null = null;

  constructor(
    host: HTMLDivElement,
    private readonly options: SceneBoardKonvaAdapterOptions
  ) {
    host.replaceChildren();

    this.stage = new Konva.Stage({
      container: host,
      width: Math.max(host.clientWidth, 640),
      height: Math.max(host.clientHeight, 520),
    });

    this.stage.add(this.backgroundLayer);
    this.stage.add(this.drawingLayer);
    this.stage.add(this.tokenLayer);
    this.stage.add(this.overlayLayer);

    this.renderGrid();
    this.bindEvents();
  }

  public destroy(): void {
    this.stage.destroy();
  }

  public resize(width: number, height: number): void {
    if (width <= 0 || height <= 0) {
      return;
    }

    this.stage.size({ width, height });
    this.renderGrid();
    this.stage.batchDraw();
  }

  public setEditable(isEditable: boolean): void {
    this.isEditable = isEditable;
  }

  public render(document: SceneBoardDocument | null, selectedTokenId: string | null): void {
    this.currentDocument = document;
    this.selectedTokenId = selectedTokenId;

    this.drawingLayer.destroyChildren();
    this.tokenLayer.destroyChildren();

    if (document) {
      for (const stroke of document.strokes) {
        const line = new Konva.Line({
          points: [...stroke.points],
          stroke: stroke.color,
          strokeWidth: stroke.width,
          lineCap: 'round',
          lineJoin: 'round',
          tension: 0.1,
          name: 'board-stroke',
        });
        line.setAttr('boardStrokeId', stroke.id);
        this.drawingLayer.add(line);
      }

      for (const token of [...document.tokens].sort((left, right) => left.zIndex - right.zIndex)) {
        this.tokenLayer.add(this.createTokenNode(token, token.id === selectedTokenId));
      }
    }

    this.drawingLayer.batchDraw();
    this.tokenLayer.batchDraw();
  }

  public viewportCenter(): BoardPoint {
    const scaleX = this.stage.scaleX() || 1;
    const scaleY = this.stage.scaleY() || 1;

    return {
      x: (-this.stage.x() / scaleX) + this.stage.width() / (2 * scaleX),
      y: (-this.stage.y() / scaleY) + this.stage.height() / (2 * scaleY),
    };
  }

  private bindEvents(): void {
    this.stage.on('pointerdown', event => {
      if (!this.isEditable || this.options.getTool() !== 'pen') {
        return;
      }

      if (!this.isBackgroundTarget(event.target)) {
        return;
      }

      const worldPointer = this.pointerWorldPosition();
      if (!worldPointer) {
        return;
      }

      this.isDrawing = true;
      this.previewPoints = [worldPointer.x, worldPointer.y];
      this.previewLine?.destroy();
      this.previewLine = new Konva.Line({
        points: [...this.previewPoints],
        stroke: this.options.getStrokeColor(),
        strokeWidth: this.options.getStrokeWidth(),
        lineCap: 'round',
        lineJoin: 'round',
        tension: 0.1,
        listening: false,
      });
      this.overlayLayer.add(this.previewLine);
      this.overlayLayer.batchDraw();
      this.options.onTokenSelected(null);
    });

    this.stage.on('pointermove', () => {
      if (!this.isDrawing || !this.previewLine) {
        return;
      }

      const worldPointer = this.pointerWorldPosition();
      if (!worldPointer) {
        return;
      }

      this.previewPoints = [...this.previewPoints, worldPointer.x, worldPointer.y];
      this.previewLine.points(this.previewPoints);
      this.overlayLayer.batchDraw();
    });

    this.stage.on('pointerup pointerleave', () => {
      if (!this.isDrawing) {
        return;
      }

      const points = [...this.previewPoints];
      this.isDrawing = false;
      this.previewPoints = [];
      this.previewLine?.destroy();
      this.previewLine = null;
      this.overlayLayer.batchDraw();

      if (points.length >= 4) {
        this.options.onStrokeCreated(points);
      }
    });

    this.stage.on('click tap', event => {
      const tokenId = this.findAttrValue(event.target, 'boardTokenId');
      const strokeId = this.findAttrValue(event.target, 'boardStrokeId');
      const tool = this.options.getTool();

      if (tool === 'erase' && this.isEditable) {
        if (tokenId) {
          this.options.onTokenRemoved(tokenId);
          return;
        }

        if (strokeId) {
          this.options.onStrokeRemoved(strokeId);
          return;
        }
      }

      if (tool === 'select') {
        this.options.onTokenSelected(tokenId);
      }
    });

    this.stage.on('wheel', event => {
      event.evt.preventDefault();
      const pointer = this.stage.getPointerPosition();
      if (!pointer) {
        return;
      }

      const oldScale = this.stage.scaleX();
      const direction = event.evt.deltaY > 0 ? -1 : 1;
      const proposedScale = direction > 0 ? oldScale * ZOOM_FACTOR : oldScale / ZOOM_FACTOR;
      const newScale = Math.max(MIN_SCALE, Math.min(MAX_SCALE, proposedScale));

      const worldPoint = {
        x: (pointer.x - this.stage.x()) / oldScale,
        y: (pointer.y - this.stage.y()) / oldScale,
      };

      this.stage.scale({ x: newScale, y: newScale });
      this.stage.position({
        x: pointer.x - worldPoint.x * newScale,
        y: pointer.y - worldPoint.y * newScale,
      });
      this.renderGrid();
      this.stage.batchDraw();
    });
  }

  private createTokenNode(token: BoardToken, isSelected: boolean): Konva.Group {
    const group = new Konva.Group({
      x: token.x,
      y: token.y,
      draggable: this.isEditable && !token.locked,
      name: 'board-token',
    });
    group.setAttr('boardTokenId', token.id);

    const body = new Konva.Rect({
      x: -token.width / 2,
      y: -token.height / 2,
      width: token.width,
      height: token.height,
      fill: token.color,
      cornerRadius: 12,
      stroke: isSelected ? '#facc15' : '#0f172a',
      strokeWidth: isSelected ? 4 : 2,
      shadowColor: '#020617',
      shadowBlur: 10,
      shadowOpacity: 0.18,
      shadowOffsetY: 3,
    });

    const label = new Konva.Text({
      x: -token.width / 2 + 6,
      y: -token.height / 2 + 12,
      width: token.width - 12,
      height: token.height - 24,
      text: token.label,
      fontSize: 11,
      fontStyle: 'bold',
      fill: '#f8fafc',
      align: 'center',
      verticalAlign: 'middle',
      lineHeight: 0.95,
      listening: false,
    });

    group.add(body);
    group.add(label);
    group.dragBoundFunc(position => this.snapTokenPosition(position));

    group.on('dragstart', () => {
      this.options.onTokenSelected(token.id);
    });
    group.on('dragend', () => {
      this.options.onTokenMoved(token.id, {
        x: group.x(),
        y: group.y(),
      });
    });

    return group;
  }

  private renderGrid(): void {
    this.backgroundLayer.destroyChildren();

    const bounds = this.gridWorldBounds();
    const background = new Konva.Rect({
      x: bounds.minX,
      y: bounds.minY,
      width: bounds.maxX - bounds.minX,
      height: bounds.maxY - bounds.minY,
      fill: BOARD_SURFACE_COLOR,
    });
    background.setAttr('boardBackground', true);
    this.backgroundLayer.add(background);

    for (let x = bounds.minX; x <= bounds.maxX; x += BOARD_GRID_SIZE) {
      this.backgroundLayer.add(new Konva.Line({
        points: [x, bounds.minY, x, bounds.maxY],
        stroke: BOARD_GRID_COLOR,
        strokeWidth: 1,
        listening: false,
      }));
    }

    for (let y = bounds.minY; y <= bounds.maxY; y += BOARD_GRID_SIZE) {
      this.backgroundLayer.add(new Konva.Line({
        points: [bounds.minX, y, bounds.maxX, y],
        stroke: BOARD_GRID_COLOR,
        strokeWidth: 1,
        listening: false,
      }));
    }

    this.backgroundLayer.batchDraw();
  }

  private isBackgroundTarget(target: Konva.Node | null): boolean {
    return target === this.stage || Boolean(target?.getAttr('boardBackground'));
  }

  private findAttrValue(node: Konva.Node | null, attrName: string): string | null {
    let currentNode: Konva.Node | null = node;
    while (currentNode) {
      const attrValue = currentNode.getAttr(attrName);
      if (typeof attrValue === 'string' && attrValue.length > 0) {
        return attrValue;
      }
      currentNode = currentNode.getParent();
    }
    return null;
  }

  private pointerWorldPosition(): BoardPoint | null {
    const pointer = this.stage.getRelativePointerPosition();
    if (!pointer) {
      return null;
    }

    return {
      x: pointer.x,
      y: pointer.y,
    };
  }

  private snapTokenPosition(position: BoardPoint): BoardPoint {
    return snapPointToGrid(position);
  }

  private gridWorldBounds(): { minX: number; minY: number; maxX: number; maxY: number } {
    const scale = this.stage.scaleX() || 1;
    const viewportWidth = this.stage.width() / scale;
    const viewportHeight = this.stage.height() / scale;
    const viewportLeft = -this.stage.x() / scale;
    const viewportTop = -this.stage.y() / scale;
    const viewportRight = viewportLeft + viewportWidth;
    const viewportBottom = viewportTop + viewportHeight;
    const paddingX = viewportWidth;
    const paddingY = viewportHeight;

    return {
      minX: this.alignToGrid(viewportLeft - paddingX, 'floor'),
      minY: this.alignToGrid(viewportTop - paddingY, 'floor'),
      maxX: this.alignToGrid(viewportRight + paddingX, 'ceil'),
      maxY: this.alignToGrid(viewportBottom + paddingY, 'ceil'),
    };
  }

  private alignToGrid(value: number, direction: 'floor' | 'ceil'): number {
    const alignedValue = value / BOARD_GRID_SIZE;
    return (direction === 'floor' ? Math.floor(alignedValue) : Math.ceil(alignedValue)) * BOARD_GRID_SIZE;
  }
}
