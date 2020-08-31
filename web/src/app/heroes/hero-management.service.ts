import {Injectable} from '@angular/core';
import {Hero} from '../shared/models/NewHero.model';
import {HeroesService} from './heroes.service';
import {Subject} from 'rxjs';
import {EditorAction} from '../shared/dtos/ModalEntityData';
import {ShopItem} from '../shared/models/shop/ShopItem.model';
import {ItemInstance} from '../shared/models/ItemInstance.model';

@Injectable({
  providedIn: 'root'
})
export class HeroManagementService {
  hero: Hero;
  heroChanged = new Subject<Hero>();
  addItemToinventory = new Subject<ItemInstance>();
  action: EditorAction;
  constructor(
    private heroService: HeroesService
  ) {
    this.heroChanged.subscribe(hero => this.hero = hero);
  }

  buyItems(items: ShopItem[]) {
    if (this.action === EditorAction.create) {
      for (const item of items) {
        this.addItemToinventory.next(item.item);
      }
    }
  }
}
