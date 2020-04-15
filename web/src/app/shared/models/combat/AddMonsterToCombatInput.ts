import {Hero} from '../NewHero.model';
import {Monster} from '../Monster.model';

export class AddMonsterToCombatInput {
  public combatId: string;
  public monster: Monster;


  constructor(combatId: string, monster: Monster) {
    this.combatId = combatId;
    this.monster = monster;
  }
}
