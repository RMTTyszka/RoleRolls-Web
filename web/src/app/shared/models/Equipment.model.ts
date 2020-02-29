import {Entity} from './Entity.model';
import {ArmorInstance} from './ArmorInstance.model';

export class Equipment extends Entity {
  armor: ArmorInstance = null;
}
