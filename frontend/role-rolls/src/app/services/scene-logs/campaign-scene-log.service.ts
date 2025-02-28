import { Injectable } from '@angular/core';
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import { RR_API } from '@app/tokens/loh.api';
import { HistoryDto } from '@app/campaigns/models/history-dto';

@Injectable({
  providedIn: 'root'
})
export class CampaignSceneLogService {

  public path = 'campaigns';
  public serverUrl = RR_API.backendUrl;
  constructor(
    private http: HttpClient,
  ) { }

  public getHistory(campaignId: string, sceneId: string): Observable<HistoryDto[]> {
    return this.http.get<HistoryDto[]>(`${this.completePath}/${campaignId}/scenes/${sceneId}/history`);
  }

  private get completePath(): string {
    return this.serverUrl + this.path;
  }

}
