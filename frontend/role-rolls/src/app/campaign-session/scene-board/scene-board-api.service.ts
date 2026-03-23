import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  BoardCommand,
  BoardDeleteCommand,
  BoardOperationEnvelope,
  BoardStroke,
  BoardTokenLockChangedPayload,
  BoardTokenLockInput,
  BoardTokenMoveInput,
  BoardTokenMovedPayload,
  BoardTokenRenameInput,
  BoardTokenRenamedPayload,
  BoardToken,
  SceneBoardDocument
} from '@app/campaign-session/scene-board/scene-board.models';
import { RR_API } from '@app/tokens/loh.api';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SceneBoardApiService {
  constructor(
    private readonly http: HttpClient,
  ) {
  }

  public getBoard(campaignId: string, sceneId: string): Observable<SceneBoardDocument> {
    return this.http.get<SceneBoardDocument>(this.boardPath(campaignId, sceneId));
  }

  public addStroke(
    campaignId: string,
    sceneId: string,
    command: BoardCommand<BoardStroke>
  ): Observable<BoardOperationEnvelope<BoardStroke>> {
    return this.http.post<BoardOperationEnvelope<BoardStroke>>(
      `${this.boardPath(campaignId, sceneId)}/strokes`,
      command
    );
  }

  public removeStroke(
    campaignId: string,
    sceneId: string,
    strokeId: string,
    command: BoardDeleteCommand
  ): Observable<BoardOperationEnvelope<{ strokeId: string }>> {
    return this.http.request<BoardOperationEnvelope<{ strokeId: string }>>(
      'delete',
      `${this.boardPath(campaignId, sceneId)}/strokes/${strokeId}`,
      {
        body: command
      }
    );
  }

  public upsertToken(
    campaignId: string,
    sceneId: string,
    command: BoardCommand<BoardToken>
  ): Observable<BoardOperationEnvelope<BoardToken>> {
    return this.http.put<BoardOperationEnvelope<BoardToken>>(
      `${this.boardPath(campaignId, sceneId)}/tokens/${command.payload.id}`,
      command
    );
  }

  public moveToken(
    campaignId: string,
    sceneId: string,
    tokenId: string,
    command: BoardCommand<BoardTokenMoveInput>
  ): Observable<BoardOperationEnvelope<BoardTokenMovedPayload>> {
    return this.http.put<BoardOperationEnvelope<BoardTokenMovedPayload>>(
      `${this.boardPath(campaignId, sceneId)}/tokens/${tokenId}/position`,
      command
    );
  }

  public renameToken(
    campaignId: string,
    sceneId: string,
    tokenId: string,
    command: BoardCommand<BoardTokenRenameInput>
  ): Observable<BoardOperationEnvelope<BoardTokenRenamedPayload>> {
    return this.http.put<BoardOperationEnvelope<BoardTokenRenamedPayload>>(
      `${this.boardPath(campaignId, sceneId)}/tokens/${tokenId}/label`,
      command
    );
  }

  public setTokenLocked(
    campaignId: string,
    sceneId: string,
    tokenId: string,
    command: BoardCommand<BoardTokenLockInput>
  ): Observable<BoardOperationEnvelope<BoardTokenLockChangedPayload>> {
    return this.http.put<BoardOperationEnvelope<BoardTokenLockChangedPayload>>(
      `${this.boardPath(campaignId, sceneId)}/tokens/${tokenId}/lock`,
      command
    );
  }

  public removeToken(
    campaignId: string,
    sceneId: string,
    tokenId: string,
    command: BoardDeleteCommand
  ): Observable<BoardOperationEnvelope<{ tokenId: string }>> {
    return this.http.request<BoardOperationEnvelope<{ tokenId: string }>>(
      'delete',
      `${this.boardPath(campaignId, sceneId)}/tokens/${tokenId}`,
      {
        body: command
      }
    );
  }

  public clearBoard(
    campaignId: string,
    sceneId: string,
    command: BoardDeleteCommand
  ): Observable<BoardOperationEnvelope<null>> {
    return this.http.post<BoardOperationEnvelope<null>>(
      `${this.boardPath(campaignId, sceneId)}/clear`,
      command
    );
  }

  private boardPath(campaignId: string, sceneId: string): string {
    return `${RR_API.backendUrl}campaigns/${campaignId}/scenes/${sceneId}/board`;
  }
}
