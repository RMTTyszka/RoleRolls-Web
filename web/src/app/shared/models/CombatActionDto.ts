import { Entity } from './Entity.model';
import {AttackDetails} from './AttackDetails.model';

export class CombatActionDto extends Entity {
  attackDetails: AttackDetails;
}
