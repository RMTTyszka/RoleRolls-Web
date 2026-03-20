import { Subject } from 'rxjs';
import { AuthenticationService } from '@app/authentication/services/authentication.service';
import { HistoryDto } from '@app/campaigns/models/history-dto';
import { Injectable } from '@angular/core';
import { RR_API } from '@app/tokens/loh.api';
import * as signalR from '@microsoft/signalr';
import {HubConnectionState, LogLevel} from '@microsoft/signalr';
import { BoardOperationEnvelope } from '@app/campaign-session/scene-board/scene-board.models';

@Injectable({
  providedIn: 'root'
})
export class SceneNotificationService {

  public historyUpdated = new Subject<HistoryDto>();
  public boardOperationReceived = new Subject<BoardOperationEnvelope>();
  public connection: signalR.HubConnection;
  private groupsToJoin = new Set<string>();
  private joinedGroups = new Set<string>();

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
    this.registerHandlers();
    this.start();
  }

  public async start() {
    try {
      await this.connection.start();
      console.log("Connection is established");
      await this.joinPendingGroups();
    } catch (error) {
      console.error("Error during connection startup:", error);
    }

  }
  public async joinScene(sceneId: string) {
    const groupName = 'SceneGroup_'+sceneId;
    this.joinedGroups.add(groupName);
    if (this.connection.state === HubConnectionState.Connected) {
      console.log('JoinGroup');
      return this.connection.invoke("JoinGroup", groupName);
    } else {
      this.groupsToJoin.add(groupName);
    }
  }

  private registerHandlers() {
    this.connection.on("UpdateHistory", (message: HistoryDto) => {
      this.historyUpdated.next(message)
    });
    this.connection.on("BoardOperationApplied", (message: BoardOperationEnvelope) => {
      this.boardOperationReceived.next(message);
    });
    this.connection.onclose(async error => {
      console.error(error);
      await this.start();
    })
    this.connection.onreconnected(async () => {
      console.log("Connection reconnected");
      await this.rejoinKnownGroups();
    })
  }

  private async joinPendingGroups() {
    const groups = [...this.groupsToJoin];
    this.groupsToJoin.clear();
    for (const groupName of groups) {
      console.log('JoinGroup');
      await this.connection.invoke("JoinGroup", groupName);
    }
  }

  private async rejoinKnownGroups() {
    for (const groupName of this.joinedGroups) {
      await this.connection.invoke("JoinGroup", groupName);
    }
  }
}
