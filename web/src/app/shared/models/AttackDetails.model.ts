import {AttackResult} from './AttackResult.model';

export class AttackDetails {
  mainWeaponAttackResult = new AttackResult();
  offWeaponAttackResult = new AttackResult();
  evasion = 0;
  defense = 0;
}
