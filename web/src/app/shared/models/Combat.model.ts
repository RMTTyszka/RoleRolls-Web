import {Entity} from './Entity.model';
import {Monster} from './Monster.model';
import {Hero} from './NewHero.model';
import {Iniciative} from './Iniciative.model';

export class Combat extends Entity{
  monsters: Array<Monster> = new Array<Monster>();
  heroes: Array<Hero> = new Array<Hero>();
  initiatives: Array<Iniciative> = new Array<Iniciative>();
  currentInitiative: number;
}
