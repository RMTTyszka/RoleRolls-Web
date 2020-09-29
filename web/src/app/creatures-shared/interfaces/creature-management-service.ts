import {Shop} from '../../shared/models/shop/Shop.model';
import {ShopItem} from '../../shared/models/shop/ShopItem.model';
import {Subject} from 'rxjs';
import {ItemInstance} from '../../shared/models/ItemInstance.model';
import {Creature} from '../../shared/models/creatures/Creature.model';

export interface CreatureManagementService {
  entity: Creature;
  addItemToInventory: Subject<ItemInstance>;
  buyItems(shop: Shop, items: ShopItem[]);
}
