import {AttackResult} from './AttackResult.model';
import {Creature} from './Creature.model';

export class AttackDetails {
  mainWeaponAttackResult = new AttackResult();
  offWeaponAttackResult = new AttackResult();
  evasion = 0;
  defense = 0;
  attacker: Creature;
  target: Creature;
}
