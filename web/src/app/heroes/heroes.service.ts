import { Injectable, Injector } from '@angular/core';
import { BaseEntityService } from '../shared/base-entity-service';
import { Hero } from '../shared/models/Hero.model';
import {NewHero} from '../shared/models/NewHero.model';

@Injectable({
  providedIn: 'root'
})
export class HeroesService extends BaseEntityService<NewHero> {
  path = 'hero';
  constructor(
    injector: Injector
  ) {
    super(injector, NewHero);
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

}
