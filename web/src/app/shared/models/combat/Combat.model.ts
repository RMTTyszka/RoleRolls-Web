import {Entity} from '../Entity.model';
import {Monster} from '../creatures/monsters/Monster.model';
import {Hero} from '../NewHero.model';
import {Initiative} from '../Iniciative.model';
import {CombatLog} from './CombatLog';
import {Creature} from '../creatures/Creature.model';

export class Combat extends Entity {
  monsters: Array<Monster | Creature> = new Array<Monster>();
  heroes: Array<Hero | Creature> = new Array<Hero>();
  initiatives: Array<Initiative> = new Array<Initiative>();
  hasStarted: boolean;
  currentInitiative: Initiative = new Initiative();
  lastUpdateTime: Date;
  combatLog: CombatLog[];
}
