import {Hero} from '../NewHero.model';
import {Monster} from '../creatures/monsters/Monster.model';
import {Creature} from '../creatures/Creature.model';

export class AddOrRemoveCreatureToCombatInput<T extends Creature> {
  public creatureId: string;


  constructor(creature: T) {
    this.creatureId = creature.id;
  }
}
