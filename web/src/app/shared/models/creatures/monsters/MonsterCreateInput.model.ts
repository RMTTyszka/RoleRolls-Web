import {MonsterModel} from './MonsterModel.model';
import {Entity} from '../../Entity.model';

export class MonsterCreateInput extends Entity {
  level: number;
  monsterModel: MonsterModel;
}
