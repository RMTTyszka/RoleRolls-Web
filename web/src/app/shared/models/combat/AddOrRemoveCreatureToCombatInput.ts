import {Hero} from '../NewHero.model';
import {Monster} from '../Monster.model';
import {Creature} from '../creatures/Creature.model';

export class AddOrRemoveCreatureToCombatInput<T extends Creature> {
  public combatId: string;
  public creature: T;


  constructor(combatId: string, creature: T) {
    this.combatId = combatId;
    this.creature = creature;
  }
}
