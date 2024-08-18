import {Injectable} from '@angular/core';
import * as signalR from '@microsoft/signalr';
import {LogLevel} from '@microsoft/signalr';
import {LOH_API} from "../../loh.api";
import {Subject} from "rxjs";
import {AuthenticationService} from "../../authentication/authentication.service";

@Injectable({
  providedIn: 'root'
})
export class SceneNotificationService {

  public historyUpdated = new Subject<string>();
  public connection: signalR.HubConnection;

  constructor(
    private readonly service: AuthenticationService
  ) {
    const token = this.service.getToken()
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl(LOH_API.myPocketBackUrl + 'sceneHub', {
        accessTokenFactory: () => token // Use o token capturado do interceptor
      })
      .configureLogging(LogLevel.Information)
      .build()
    this.start();
    this.connection.on("UpdateHistory", (message: string) => {
      this.historyUpdated.next(message)
    });
  }

  public async start() {
    try {
      await this.connection.start();
      console.log("Connection is established");
    } catch (error) {
      console.error("Error during connection startup:", error);
    }
  }
  public async joinScene(sceneId: string) {
    return this.connection.invoke("JoinGroup", 'SceneGroup_'+sceneId);
  }
}
