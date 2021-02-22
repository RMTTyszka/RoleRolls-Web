import {DamageDetails} from './DamageDetails.model';
import {Roll} from './rolls/Roll';

export class AttackResult {
  numberOfAttacks = 0;
  rolls: Roll[] = [];
  hitBonus = 0;
  hits = 0;
  criticalHits = 0;
  criticalMisses = 0;
  damageDetails: DamageDetails;
  totalDamage = 0;
}
