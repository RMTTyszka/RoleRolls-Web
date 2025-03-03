import {Injectable} from '@angular/core';
import {Observable, of} from 'rxjs';
import {HttpClient, HttpParams} from '@angular/common/http';
import { v4 as uuidv4 } from 'uuid';
import { RRColumns } from '@app/components/grid/grid.component';
import { RR_API } from '@app/tokens/loh.api';
import { PagedOutput } from '@app/models/PagedOutput';
import {
  ArmorTemplateModel,
  ItemTemplateModel,
  ItemType,
  WeaponTemplateModel
} from '@app/models/itens/ItemTemplateModel';

@Injectable({
  providedIn: 'root'
})
export class CampaignItemTemplatesService {
  public path = 'item-templates';
  public selectPlaceholder: string;
  public fieldName: string;
  public selectModalTitle: string;
  public selectModalColumns: RRColumns[];
  public entityListColumns: RRColumns[];
  public serverUrl = RR_API.backendUrl;
  private completePath(path: string): string {
    return this.serverUrl + path;
  }
  constructor(
    private httpClient: HttpClient,
  ) {
  }

  public getNew(): Observable<ItemTemplateModel> {
    const campaignModel = {
      id: uuidv4(),
      name: 'New Campaign',
    } as ItemTemplateModel;
    return of<ItemTemplateModel>(campaignModel);
  }
   get(itemId: string): Observable<ItemTemplateModel> {
    return this.httpClient.get<ItemTemplateModel>(`${this.completePath(this.path)}/${itemId}`);
  }

  public addItem(item: ItemTemplateModel): Observable<never> {
    return this.httpClient.post<never>(`${this.completePath(this.resolvePath(ItemType.Consumable))}`, item);
  }
  public updateItem(itemId: string, item: ItemTemplateModel): Observable<never> {
    return this.httpClient.put<never>(`${this.completePath(this.resolvePath(ItemType.Consumable))}/${itemId}`, item);
  }
  public removeItem(itemId: string): Observable<never> {
    return this.httpClient.delete<never>(`${this.completePath(this.resolvePath(ItemType.Consumable))}/${itemId}`);
  }

    list(campaingId: string, itemType: ItemType, filter: string, skipCount: number, maxResultCount: number) {
    const params = new HttpParams()
      .set('filter', filter)
      .set('skipCount', skipCount)
      .set('maxResultCount', maxResultCount)
      .set('campaignId', campaingId);
    return this.httpClient.get<PagedOutput<ItemTemplateModel>>(`${this.completePath(this.resolvePath(itemType))}`, {
      params
    });
  }

  addWeapon(weaponTemplate: WeaponTemplateModel) {
    return this.httpClient.post<never>(`${this.serverUrl}weapon-templates`, weaponTemplate);
  }

  updateWeapon(id: string, weaponTemplate: WeaponTemplateModel) {
    return this.httpClient.put<never>(`${this.serverUrl}weapon-templates/${id}`, weaponTemplate);
  }

  addArmor(template: ArmorTemplateModel) {
    return this.httpClient.post<never>(`${this.serverUrl}armor-templates`, template);
  }
  updateArmor(id: string, template: ArmorTemplateModel) {
    return this.httpClient.put<never>(`${this.serverUrl}armor-templates/${id}`, template);
  }

  private resolvePath(itemType: ItemType) {
    switch (itemType) {
      case null:
        return this.path;
      case ItemType.Consumable:
        return 'consumable-templates';
      case ItemType.Weapon:
        return 'weapon-templates';
      case ItemType.Armor:
        return 'armor-templates';

    }
  }
}
