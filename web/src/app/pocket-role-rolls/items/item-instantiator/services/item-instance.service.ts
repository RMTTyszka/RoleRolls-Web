import { Injectable } from '@angular/core';
import {ItemTemplateModel, ItemType} from 'src/app/shared/models/pocket/itens/ItemTemplateModel';
import {Observable} from 'rxjs';
import {HttpClient} from '@angular/common/http';
import {LOH_API} from 'src/app/loh.api';
import {InstantiateItemInput} from 'src/app/pocket-role-rolls/items/item-instantiator/models/instantiate-item-input';
import {ItemModel} from 'src/app/shared/models/pocket/creatures/item-model';
import {EquipInput} from '../../../pocket-creature-editor/tokens/equip-input';
import {EquipableSlot} from '../../../../shared/models/pocket/itens/equipable-slot';

@Injectable({
  providedIn: 'root'
})
export class ItemInstanceService {
  public serverUrl = LOH_API.myPocketBackUrl;
  constructor(
    private httpClient: HttpClient,
  ) { }

  public addItem(campaignId: string, creatureId: string, item: InstantiateItemInput): Observable<ItemModel> {
    return this.httpClient.post<ItemModel>(`${this.serverUrl}campaigns/${campaignId}/creatures/${creatureId}/itens`, item);
  }

  removeItem(campaignId: string, creatureId: string, id: string) {
    return this.httpClient.delete<never>(`${this.serverUrl}campaigns/${campaignId}/creatures/${creatureId}/itens/${id}`);
  }

  equip(campaignId: string, creatureId: string, item: EquipInput) {
    return this.httpClient.post<never>(`${this.serverUrl}campaigns/${campaignId}/creatures/${creatureId}/equipments`, item);
  }
  unequip(campaignId: string, creatureId: string, slot: EquipableSlot) {
    return this.httpClient.delete<never>(`${this.serverUrl}campaigns/${campaignId}/creatures/${creatureId}/equipments/slots/${slot}`);
  }
}
