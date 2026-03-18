import { Injectable, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';
import * as signalR from '@microsoft/signalr';

export interface StrokePoint {
  x: number;
  y: number;
}

export interface DrawingEvent {
  userId: string;
  points: StrokePoint[];
  color: string;
  strokeWidth: number;
}

export interface TokenEvent {
  tokenId: string;
  userId: string;
  x: number;
  y: number;
}

@Injectable({ providedIn: 'root' })
export class DrawingHubService implements OnDestroy {

  private hubConnection!: signalR.HubConnection;

  // Subjects para os outros componentes escutarem
  readonly remoteStroke$ = new Subject<DrawingEvent>();
  readonly remoteTokenMoved$ = new Subject<TokenEvent>();
  readonly remoteTokenAdded$ = new Subject<TokenEvent>();

  async connect(roomId: string): Promise<void> {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`/hubs/drawing?roomId=${roomId}`)
      .withAutomaticReconnect()
      .build();

    // Escuta traços de desenho dos outros usuários
    this.hubConnection.on('ReceiveStroke', (event: DrawingEvent) => {
      this.remoteStroke$.next(event);
    });

    // Escuta movimentação de tokens
    this.hubConnection.on('ReceiveTokenMoved', (event: TokenEvent) => {
      this.remoteTokenMoved$.next(event);
    });

    // Escuta criação de novos tokens
    this.hubConnection.on('ReceiveTokenAdded', (event: TokenEvent) => {
      this.remoteTokenAdded$.next(event);
    });

    await this.hubConnection.start();
    await this.hubConnection.invoke('JoinRoom', roomId);
  }

  // Envia traço de desenho para os outros
  sendStroke(event: DrawingEvent): void {
    this.hubConnection.invoke('SendStroke', event).catch(console.error);
  }

  // Envia movimentação de token
  sendTokenMoved(event: TokenEvent): void {
    this.hubConnection.invoke('SendTokenMoved', event).catch(console.error);
  }

  // Envia token novo
  sendTokenAdded(event: TokenEvent): void {
    this.hubConnection.invoke('SendTokenAdded', event).catch(console.error);
  }

  disconnect(): void {
    this.hubConnection?.stop();
  }

  ngOnDestroy(): void {
    this.disconnect();
  }
}
