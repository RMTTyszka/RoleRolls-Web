import { Injectable } from '@angular/core';
import {LOH_API} from "../../loh.api";
import {Observable} from "rxjs";
import {CampaignScene} from "../../shared/models/pocket/campaigns/campaign-scene-model";
import {HttpClient} from "@angular/common/http";
import {HistoryDto} from "./models/history-dto";

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
