import { Injectable } from '@angular/core';
import {ItemTemplateModel, ItemType} from 'src/app/shared/models/pocket/itens/ItemTemplateModel';
import {Observable} from 'rxjs';
import {HttpClient} from '@angular/common/http';
import {LOH_API} from 'src/app/loh.api';
import {InstantiateItemInput} from 'src/app/pocket-role-rolls/items/item-instantiator/models/instantiate-item-input';
import {ItemModel} from 'src/app/shared/models/pocket/creatures/item-model';

@Injectable({
  providedIn: 'root'
})
export class ItemInstantiatorService {
  public serverUrl = LOH_API.myPocketBackUrl;
  constructor(
    private httpClient: HttpClient,
  ) { }

  public addItem(campaignId: string, creatureId: string, item: InstantiateItemInput): Observable<ItemModel> {
    return this.httpClient.post<ItemModel>(`${this.serverUrl}campaigns/${campaignId}/creatures/${creatureId}/itens`, item);
  }
}
