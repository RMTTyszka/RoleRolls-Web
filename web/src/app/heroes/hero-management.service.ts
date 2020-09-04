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

@Injectable({
  providedIn: 'root'
})
export class HeroManagementService {
  hero: Hero;
  heroChanged = new Subject<Hero>();
  addItemToinventory = new Subject<ItemInstance>();
  updateFunds = new Subject<number>();
  action: EditorAction;
  constructor(
    private heroService: HeroesService,
    private heroShopService: HeroShopService,
  ) {
    this.heroChanged.subscribe(hero => this.hero = hero);
  }

  buyItems(shop: Shop, items: ShopItem[]) {
    const itemsToAdd: ItemInstance[] = [];
    for (const item of items) {
      this.heroShopService.buy(this.hero.id, shop.id, item.id, item.quantityToBuy)
        .subscribe(buyOutput => {
          itemsToAdd.push(buyOutput.itemInstance);
          this.addItemToinventory.next(buyOutput.itemInstance);
          this.updateFunds.next(item.value * item.quantityToBuy);
          this.hero.inventory.cash1 -= item.value * item.quantityToBuy;
        });

    }
  }
}
