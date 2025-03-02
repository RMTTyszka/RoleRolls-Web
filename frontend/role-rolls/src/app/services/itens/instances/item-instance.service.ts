import { Injectable } from '@angular/core';
import {RR_API} from '@app/tokens/loh.api';
import {HttpClient} from '@angular/common/http';
import {ItemModel} from '@app/campaigns/models/item-model';
import { Observable } from 'rxjs';
import {InstantiateItemInput} from '@app/models/itens/instances/instantiate-item-input';
import {EquipInput} from '@app/models/creatures/equip-input';
import {EquipableSlot} from '@app/models/itens/equipable-slot';

@Injectable({
  providedIn: 'root'
})
export class ItemInstanceService {
  public serverUrl = RR_API.backendUrl;
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
