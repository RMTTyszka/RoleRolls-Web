import {Injectable, Injector} from '@angular/core';
import {BaseEntityService} from '../shared/base-entity-service';
import {Hero} from '../shared/models/NewHero.model';
import {ItemInstance} from '../shared/models/ItemInstance.model';

@Injectable({
  providedIn: 'root'
})
export class HeroesService extends BaseEntityService<Hero> {
  path = 'hero';
  constructor(
    injector: Injector
  ) {
    super(injector, Hero);
   }

   getAllDummies() {
    return this.getAllFiltered('Dummy');
   }


   static getTotalAttributeBonusPoints(level: number) {
     return (level - 1) * 2;
   }

   static getMaximumAttributeBonusPoints(level: number) {
     return level - 1;
   }


   static getTotalSkillsBonusPoints(level: number) {
     return level * 6 + 12;
   }

   static getMaximumSkillsBonusPoints(level: number) {
     return Number(level) + 2;
   }

   public addItemsToInventory(heroId: string, items: ItemInstance[]) {
    return this.http.put(this.serverUrl + this.path + '/addItems', {
      items: items,
      heroId: heroId
    }).toPromise();
  }

}
