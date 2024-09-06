import { Injectable } from '@angular/core';
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {LOH_API} from "src/app/loh.api";
import {HistoryDto} from "src/app/pocket-role-rolls/campaigns/models/history-dto";

@Injectable({
  providedIn: 'root'
})
export class CampaignSceneHistoryService {

  public path = 'campaigns';
  public serverUrl = LOH_API.myPocketBackUrl;
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
