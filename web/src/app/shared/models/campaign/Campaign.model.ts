import {Player} from '../Player.model';
import {Hero} from '../NewHero.model';
import {Entity} from '../Entity.model';

export class Campaign extends Entity{
  public id: string;
  public description: string;
  public master: boolean;
  public masterId: string;
  public players: Player[] = [];
  public heroes: Hero[] = [];

}
