import { Injectable } from '@angular/core';
import {CreatureManagementService} from '../../creatures-shared/interfaces/creature-management-service';
import {ItemInstance} from '../../shared/models/ItemInstance.model';
import {Subject} from 'rxjs';
import {Creature} from '../../shared/models/creatures/Creature.model';
import {Shop} from '../../shared/models/shop/Shop.model';
import {ShopItem} from '../../shared/models/shop/ShopItem.model';

@Injectable({
  providedIn: 'root'
})
export class MonsterManagementService implements CreatureManagementService {
  addItemToInventory: Subject<ItemInstance>;
  entity: Creature;
  buyItems(shop: Shop, items: ShopItem[]) {
  }

  constructor() { }
}
