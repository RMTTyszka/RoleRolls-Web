import {Bonus} from '@app/models/bonuses/bonus';

export interface CreatureType {
  id: string;
  name: string;
  description: string;
  bonuses: Bonus[];
}
