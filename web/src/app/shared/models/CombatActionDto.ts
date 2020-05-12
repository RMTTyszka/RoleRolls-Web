import {Entity} from './Entity.model';
import {AttackDetails} from './AttackDetails.model';
import {Combat} from './Combat.model';

export class CombatActionDto extends Entity {
  attackDetails: AttackDetails;
  combat: Combat;
}
