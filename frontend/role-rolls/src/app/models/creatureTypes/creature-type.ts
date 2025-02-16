import {Bonus} from '@app/models/bonuses/bonus';
import {Entity} from '@app/models/Entity.model';

export interface CreatureType extends Entity {
  id: string;
  name: string;
  description: string;
  bonuses: Bonus[];
}
