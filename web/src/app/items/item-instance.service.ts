import { Injectable } from '@angular/core';
import {ItemTemplate} from '../shared/models/Item.model';
import {Observable, of} from 'rxjs';
import {ItemInstance} from '../shared/models/ItemInstance.model';
import {HttpClient} from '@angular/common/http';
import {LOH_API} from '../loh.api';
import {ArmorTemplateService} from './equipment/armor/armors/armor-template.service';
import {ItemTemplateType} from '../shared/models/ItemTemplateType.enum';
import {ArmorModel} from '../shared/models/items/ArmorModel.model';
import {ArmorInstanceService} from './equipment/armor/armor-instance.service';
import {map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ItemInstanceService {
  serverUrl = LOH_API.myBackUrl;
  constructor(
    private http: HttpClient,
    private armorService: ArmorInstanceService,
  ) { }

  async instantiateItem(itemTemplate: ItemTemplate): Promise<ItemInstance> {
    switch (itemTemplate.itemTemplateType) {
      case ItemTemplateType.Weapon:
        break;
      case ItemTemplateType.Armor:
        return this.armorService.instantiate(itemTemplate as ArmorModel, 1).pipe(
          map(response => response.entity)
        ).toPromise();
      case ItemTemplateType.Glove:
        break;
      case ItemTemplateType.Arms:
        break;
      case ItemTemplateType.Ring:
        break;
      case ItemTemplateType.Neck:
        break;
      case ItemTemplateType.Boot:
        break;
      case ItemTemplateType.Belt:
        break;
      case ItemTemplateType.Head:
        break;

    }
  }
}
