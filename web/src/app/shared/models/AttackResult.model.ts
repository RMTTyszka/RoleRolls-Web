import {DamageDetails} from './DamageDetails.model';

export class AttackResult {
  numberOfAttacks = 0;
  rolls: number[] = [];
  hitBonus = 0;
  hits = 0;
  criticalHits = 0;
  criticalMisses = 0;
  damageDetails: DamageDetails;
  totalDamage = 0;
}
