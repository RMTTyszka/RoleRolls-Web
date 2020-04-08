import {Entity} from './Entity.model';
import {Monster} from './Monster.model';
import {NewHero} from './NewHero.model';
import {Iniciative} from './Iniciative.model';

export class Combat extends Entity{
  monsters: Array<Monster> = new Array<Monster>();
  heroes: Array<NewHero> = new Array<NewHero>();
  iniciatives: Array<Iniciative> = new Array<Iniciative>();
}
