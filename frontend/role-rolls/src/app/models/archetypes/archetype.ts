import {Bonus} from '@app/models/bonuses/bonus';

export interface Archetype {
  id: string;
  name: string;
  description: string;
  bonuses: Bonus[];
}
