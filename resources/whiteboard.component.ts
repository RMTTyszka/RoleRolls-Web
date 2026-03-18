import {
  Component,
  OnInit,
  OnDestroy,
  AfterViewInit,
  ElementRef,
  ViewChild,
  signal,
  computed
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import Konva from 'konva';
import { Subject, throttleTime } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import {
  DrawingHubService,
  DrawingEvent,
  TokenEvent,
  StrokePoint
} from '../services/drawing-hub.service';

type Tool = 'pen' | 'token';

interface Token {
  id: string;
  x: number;
  y: number;
  label: string;
  color: string;
}

@Component({
  selector: 'app-whiteboard',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <div class="whiteboard-wrapper">

      <!-- Toolbar -->
      <div class="toolbar">
        <button
          [class.active]="activeTool() === 'pen'"
          (click)="setTool('pen')"
          title="Caneta">
          ✏️ Caneta
        </button>
        <button
          [class.active]="activeTool() === 'token'"
          (click)="setTool('token')"
          title="Adicionar token">
          🪙 Token
        </button>

        <div class="separator"></div>

        <label>Cor:
          <input type="color" [(ngModel)]="strokeColor" />
        </label>

        <label>Espessura:
          <input type="range" min="1" max="20" [(ngModel)]="strokeWidth" />
        </label>

        <div class="separator"></div>

        <button (click)="clearCanvas()" class="danger">🗑 Limpar</button>

        <span class="status" [class.connected]="isConnected()">
          {{ isConnected() ? '🟢 Conectado' : '🔴 Desconectado' }}
        </span>
      </div>

      <!-- Canvas container -->
      <div #canvasContainer class="canvas-container"></div>

    </div>
  `,
  styles: [`
    .whiteboard-wrapper {
      display: flex;
      flex-direction: column;
      height: 100vh;
      background: #1a1a2e;
      font-family: 'Segoe UI', sans-serif;
    }

    .toolbar {
      display: flex;
      align-items: center;
      gap: 12px;
      padding: 10px 16px;
      background: #16213e;
      border-bottom: 1px solid #0f3460;
      flex-shrink: 0;
    }

    .toolbar button {
      padding: 6px 14px;
      border: 1px solid #0f3460;
      background: #0f3460;
      color: #e2e2e2;
      border-radius: 6px;
      cursor: pointer;
      font-size: 13px;
      transition: all 0.2s;
    }

    .toolbar button:hover { background: #1a4a8a; }

    .toolbar button.active {
      background: #e94560;
      border-color: #e94560;
    }

    .toolbar button.danger {
      background: #7a1c2e;
      border-color: #e94560;
    }

    .toolbar label {
      color: #a0a0b0;
      font-size: 13px;
      display: flex;
      align-items: center;
      gap: 6px;
    }

    .separator {
      width: 1px;
      height: 24px;
      background: #0f3460;
    }

    .status {
      margin-left: auto;
      font-size: 12px;
      color: #666;
    }

    .status.connected { color: #4caf50; }

    .canvas-container {
      flex: 1;
      cursor: crosshair;
      overflow: hidden;
    }
  `]
})
export class WhiteboardComponent implements OnInit, AfterViewInit, OnDestroy {

  @ViewChild('canvasContainer') containerRef!: ElementRef<HTMLDivElement>;

  activeTool = signal<Tool>('pen');
  isConnected = signal(false);

  strokeColor = '#e94560';
  strokeWidth = 4;

  private stage!: Konva.Stage;
  private drawingLayer!: Konva.Layer;   // Layer para traços
  private tokenLayer!: Konva.Layer;     // Layer para tokens

  private isDrawing = false;
  private currentLine!: Konva.Line;
  private strokeBuffer: StrokePoint[] = [];

  private strokeThrottle$ = new Subject<DrawingEvent>();
  private destroy$ = new Subject<void>();

  private localUserId = crypto.randomUUID();
  private tokenCounter = 0;

  constructor(private hub: DrawingHubService) {}

  async ngOnInit(): Promise<void> {
    await this.hub.connect('sala-1');
    this.isConnected.set(true);
    this.listenToHub();
  }

  ngAfterViewInit(): void {
    this.initKonva();
  }

  private initKonva(): void {
    const container = this.containerRef.nativeElement;

    this.stage = new Konva.Stage({
      container,
      width: container.clientWidth,
      height: container.clientHeight,
    });

    // Layer 1: desenho livre (fundo)
    this.drawingLayer = new Konva.Layer();
    // Layer 2: tokens (frente, recebem eventos de drag)
    this.tokenLayer = new Konva.Layer();

    this.stage.add(this.drawingLayer);
    this.stage.add(this.tokenLayer);

    this.setupDrawingEvents();
    this.setupThrottle();
    this.setupResize();
  }

  // ─── Configuração dos eventos de desenho ─────────────────────────────────────

  private setupDrawingEvents(): void {
    this.stage.on('mousedown touchstart', (e) => {
      if (this.activeTool() === 'token') {
        const pos = this.stage.getPointerPosition()!;
        this.addToken(pos.x, pos.y);
        return;
      }

      // Ignora se clicou num token
      if (e.target !== this.stage && !(e.target instanceof Konva.Rect)) {
        if (this.tokenLayer.getChildren().some(c => c === e.target || c.hasChildren?.() && (c as Konva.Group).getChildren().includes(e.target as any))) {
          return;
        }
      }

      this.isDrawing = true;
      this.strokeBuffer = [];
      const pos = this.stage.getPointerPosition()!;

      this.currentLine = new Konva.Line({
        points: [pos.x, pos.y],
        stroke: this.strokeColor,
        strokeWidth: this.strokeWidth,
        lineCap: 'round',
        lineJoin: 'round',
        tension: 0.5,
      });

      this.drawingLayer.add(this.currentLine);
    });

    this.stage.on('mousemove touchmove', () => {
      if (!this.isDrawing) return;

      const pos = this.stage.getPointerPosition()!;
      const points = this.currentLine.points();
      this.currentLine.points([...points, pos.x, pos.y]);
      this.drawingLayer.batchDraw();

      this.strokeBuffer.push({ x: pos.x, y: pos.y });

      // Throttle o envio pro SignalR
      this.strokeThrottle$.next({
        userId: this.localUserId,
        points: [...this.strokeBuffer],
        color: this.strokeColor,
        strokeWidth: this.strokeWidth,
      });
    });

    this.stage.on('mouseup touchend', () => {
      if (!this.isDrawing) return;
      this.isDrawing = false;

      // Envia stroke final para salvar no backend e broadcast
      const finalStroke: DrawingEvent = {
        userId: this.localUserId,
        points: this.strokeBuffer,
        color: this.strokeColor,
        strokeWidth: this.strokeWidth,
      };

      this.saveStrokeToBackend(finalStroke);
      this.strokeBuffer = [];
    });
  }

  // ─── Tokens arrastáveis ───────────────────────────────────────────────────────

  private addToken(x: number, y: number, fromRemote?: Token): void {
    const token: Token = fromRemote ?? {
      id: crypto.randomUUID(),
      x,
      y,
      label: `T${++this.tokenCounter}`,
      color: this.strokeColor,
    };

    const group = new Konva.Group({
      x: token.x,
      y: token.y,
      draggable: !fromRemote, // remoto não é arrastável localmente
      id: token.id,
    });

    const circle = new Konva.Circle({
      radius: 24,
      fill: token.color,
      stroke: '#ffffff',
      strokeWidth: 2,
      shadowColor: '#000',
      shadowBlur: 8,
      shadowOpacity: 0.4,
    });

    const label = new Konva.Text({
      text: token.label,
      fontSize: 13,
      fontFamily: 'Segoe UI, sans-serif',
      fill: '#ffffff',
      fontStyle: 'bold',
      offsetX: 0,
      offsetY: 7,
    });

    // Centraliza o label no círculo
    label.offsetX(label.width() / 2);

    group.add(circle, label);
    this.tokenLayer.add(group);

    if (!fromRemote) {
      // Notifica os outros da criação
      const event: TokenEvent = {
        tokenId: token.id,
        userId: this.localUserId,
        x: token.x,
        y: token.y,
      };
      this.hub.sendTokenAdded(event);
      this.saveTokenToBackend(event, token);
    }

    // Drag: throttle de 50ms para não inundar o SignalR
    const dragThrottle$ = new Subject<TokenEvent>();
    dragThrottle$.pipe(
      throttleTime(50),
      takeUntil(this.destroy$)
    ).subscribe(evt => this.hub.sendTokenMoved(evt));

    group.on('dragmove', () => {
      const pos = group.position();
      dragThrottle$.next({
        tokenId: token.id,
        userId: this.localUserId,
        x: pos.x,
        y: pos.y,
      });
    });

    group.on('dragend', () => {
      const pos = group.position();
      const evt: TokenEvent = {
        tokenId: token.id,
        userId: this.localUserId,
        x: pos.x,
        y: pos.y,
      };
      this.saveTokenToBackend(evt, token);
    });

    this.tokenLayer.batchDraw();
  }

  // ─── Escuta eventos remotos do Hub ──────────────────────────────────────────

  private listenToHub(): void {
    // Recebe traços dos outros usuários
    this.hub.remoteStroke$
      .pipe(takeUntil(this.destroy$))
      .subscribe(event => {
        if (event.userId === this.localUserId) return;
        this.renderRemoteStroke(event);
      });

    // Recebe movimentação de token
    this.hub.remoteTokenMoved$
      .pipe(takeUntil(this.destroy$))
      .subscribe(event => {
        if (event.userId === this.localUserId) return;
        const group = this.tokenLayer.findOne(`#${event.tokenId}`) as Konva.Group;
        if (group) {
          group.position({ x: event.x, y: event.y });
          this.tokenLayer.batchDraw();
        }
      });

    // Recebe token adicionado por outro
    this.hub.remoteTokenAdded$
      .pipe(takeUntil(this.destroy$))
      .subscribe(event => {
        if (event.userId === this.localUserId) return;
        this.addToken(event.x, event.y, {
          id: event.tokenId,
          x: event.x,
          y: event.y,
          label: `T${++this.tokenCounter}`,
          color: '#4a9eff',
        });
      });
  }

  private renderRemoteStroke(event: DrawingEvent): void {
    if (event.points.length < 2) return;
    const flatPoints = event.points.flatMap(p => [p.x, p.y]);

    const line = new Konva.Line({
      points: flatPoints,
      stroke: event.color,
      strokeWidth: event.strokeWidth,
      lineCap: 'round',
      lineJoin: 'round',
      tension: 0.5,
    });

    this.drawingLayer.add(line);
    this.drawingLayer.batchDraw();
  }

  // ─── Throttle de envio durante o drag ────────────────────────────────────────

  private setupThrottle(): void {
    this.strokeThrottle$.pipe(
      throttleTime(50),         // máximo 20 eventos/segundo
      takeUntil(this.destroy$)
    ).subscribe(event => {
      this.hub.sendStroke(event);
    });
  }

  // ─── Backend (HTTP) ──────────────────────────────────────────────────────────

  private saveStrokeToBackend(event: DrawingEvent): void {
    // Substitua pela sua chamada HTTP
    // this.http.post('/api/drawing/stroke', event).subscribe();
    console.log('[Backend] Salvando stroke:', event);
  }

  private saveTokenToBackend(event: TokenEvent, token: Token): void {
    // Substitua pela sua chamada HTTP
    // this.http.put(`/api/drawing/token/${token.id}`, event).subscribe();
    console.log('[Backend] Salvando token:', event);
  }

  // ─── Ações da toolbar ────────────────────────────────────────────────────────

  setTool(tool: Tool): void {
    this.activeTool.set(tool);
    // Muda o cursor conforme a ferramenta
    const container = this.containerRef.nativeElement;
    container.style.cursor = tool === 'token' ? 'copy' : 'crosshair';
  }

  clearCanvas(): void {
    this.drawingLayer.destroyChildren();
    this.drawingLayer.draw();
  }

  // ─── Resize responsivo ────────────────────────────────────────────────────────

  private setupResize(): void {
    const ro = new ResizeObserver(() => {
      const el = this.containerRef.nativeElement;
      this.stage.width(el.clientWidth);
      this.stage.height(el.clientHeight);
    });
    ro.observe(this.containerRef.nativeElement);
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
    this.hub.disconnect();
    this.stage?.destroy();
  }
}
