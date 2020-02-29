import { Injectable, Injector } from '@angular/core';
import { BaseEntityService } from '../shared/base-entity-service';
import { Hero } from '../shared/models/Hero.model';

@Injectable({
  providedIn: 'root'
})
export class HeroesService extends BaseEntityService<Hero> {
  path = 'heroes';
  constructor(
    injector: Injector
  ) {
    super(injector, Hero);
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
}
