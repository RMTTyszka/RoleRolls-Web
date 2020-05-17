import {Entity} from '../Entity.model';
import {Monster} from '../Monster.model';
import {Hero} from '../NewHero.model';
import {Initiative} from '../Iniciative.model';
import {CombatLog} from './CombatLog';

export class Combat extends Entity {
  monsters: Array<Monster> = new Array<Monster>();
  heroes: Array<Hero> = new Array<Hero>();
  initiatives: Array<Initiative> = new Array<Initiative>();
  hasStarted: boolean;
  currentInitiative: Initiative;
  lastUpdateTime: Date;
  combatLog: CombatLog[];
}
