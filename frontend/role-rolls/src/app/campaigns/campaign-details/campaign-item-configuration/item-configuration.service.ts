import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Campaign} from 'src/app/shared/models/pocket/campaigns/pocket.campaign.model';
import {
  ItemConfigurationModel
} from 'src/app/pocket-role-rolls/campaigns/CampaignEditor/item-configuration/models/item-configuration-model';
import {RR_API} from 'src/app/loh.api';

@Injectable({
  providedIn: 'root'
})
export class ItemConfigurationService {
  private path = 'campaigns';
  private serverUrl = RR_API.myPocketBackUrl;
  constructor(
    private httpClient: HttpClient
  ) { }

  public get(campaignId: string): Observable<ItemConfigurationModel> {
    return this.httpClient.get<ItemConfigurationModel>(`${this.serverUrl + this.path}/${campaignId}/item-configurations`);
  }
  public update(campaignId: string, configuration: ItemConfigurationModel): Observable<never> {
    return this.httpClient.put<never>(`${this.serverUrl + this.path}/${campaignId}/item-configurations`, configuration);
  }
}
