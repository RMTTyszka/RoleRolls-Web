import {Injectable} from '@angular/core';
import {Hero} from '../shared/models/NewHero.model';
import {HeroesService} from './heroes.service';
import {Subject} from 'rxjs';
import {EditorAction} from '../shared/dtos/ModalEntityData';
import {ShopItem} from '../shared/models/shop/ShopItem.model';
import {ItemInstance} from '../shared/models/ItemInstance.model';
import {ItemInstanceService} from '../items/item-instance.service';
import {HeroShopService} from './hero-shop/hero-shop.service';
import {Shop} from '../shared/models/shop/Shop.model';
import {CreatureManagementService} from '../creatures-shared/interfaces/creature-management-service';

@Injectable({
  providedIn: 'root'
})
export class HeroManagementService implements CreatureManagementService {
  entity: Hero;
  heroChanged = new Subject<Hero>();
  addItemToInventory = new Subject<ItemInstance>();
  updateFunds = new Subject<number>();
  constructor(
    private heroesService: HeroesService,
    private shopService: HeroShopService,
  ) {
    this.heroChanged.subscribe(hero => this.entity = hero);
  }

  buyItems(shop: Shop, items: ShopItem[]) {
    const itemsToAdd: ItemInstance[] = [];
    for (const item of items) {
      this.shopService.buy(this.entity.id, shop.id, item.id, item.quantityToBuy)
        .subscribe(buyOutput => {
          itemsToAdd.push(buyOutput.itemInstance);
          this.addItemToInventory.next(buyOutput.itemInstance);
          this.updateFunds.next(item.value * item.quantityToBuy);
          this.entity.inventory.cash1 -= item.value * item.quantityToBuy;
        });

    }
  }
}
