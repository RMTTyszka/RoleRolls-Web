import { Subject } from 'rxjs';
import { SubscriptionManager } from '@app/tokens/subscription-manager';
import { AuthenticationService } from '@app/authentication/services/authentication.service';
import { HistoryDto } from '@app/campaigns/models/history-dto';
import { Injectable } from '@angular/core';
import { RR_API } from '@app/tokens/loh.api';
import * as signalR from '@microsoft/signalr';
import {HubConnectionState, LogLevel} from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class SceneNotificationService {

  public historyUpdated = new Subject<HistoryDto>();
  public connection: signalR.HubConnection;
  private subscriptionManager: SubscriptionManager = new SubscriptionManager();
  private groupsToJoin: string[] = [];

  constructor(
    private readonly service: AuthenticationService
  ) {
    const token = this.service.getToken()
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl(RR_API.backendUrl + 'sceneHub', {
        accessTokenFactory: () => token // Use o token capturado do interceptor
      })
      .configureLogging(LogLevel.Information)
      .withAutomaticReconnect()
      .build()
    this.start();
  }

  public async start() {
    try {
      await this.connection.start();
      console.log("Connection is established");
      this.connection.on("UpdateHistory", (message: HistoryDto) => {
        this.historyUpdated.next(message)
      });
      for (const string of this.groupsToJoin) {
        console.log('JoinGroup');
        this.connection.invoke("JoinGroup", string);
      }
      this.groupsToJoin = [];
      this.connection.onclose(async error => {
        console.error(error);
        await this.start();
      })
      this.connection.onreconnected(() => {
        console.error("Reconectederror");
      })
    } catch (error) {
      console.error("Error during connection startup:", error);
    }

  }
  public async joinScene(sceneId: string) {
    if (this.connection.state === HubConnectionState.Connected) {
      console.log('JoinGroup');
      return this.connection.invoke("JoinGroup", 'SceneGroup_'+sceneId);
    } else {
      this.groupsToJoin.push('SceneGroup_'+sceneId);
    }
  }
}
