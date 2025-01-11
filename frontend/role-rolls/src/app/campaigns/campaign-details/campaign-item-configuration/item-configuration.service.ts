import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import { RR_API } from '../../../tokens/loh.api';
import { ItemConfigurationModel } from '../../models/item-configuration-model';

@Injectable({
  providedIn: 'root'
})
export class ItemConfigurationService {
  private path = 'campaigns';
  private serverUrl = RR_API.backendUrl;
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
