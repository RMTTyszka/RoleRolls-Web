import {Hero} from '../NewHero.model';

export class AddHeroToCombatInput {
  public combatId: string;
  public hero: Hero;


  constructor(combatId: string, hero: Hero) {
    this.combatId = combatId;
    this.hero = hero;
  }
}
