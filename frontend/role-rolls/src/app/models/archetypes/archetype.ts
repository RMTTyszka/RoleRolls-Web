import {Bonus} from '@app/models/bonuses/bonus';

export interface Archetype {
  id: string;
  name: string;
  description: string;
  bonuses: Bonus[];
  powerDescriptions: ArchetypePowerDescription[];
}
export interface ArchetypePowerDescription {
  id: string;
  description: string;
  name: string;
  gameDescription: string;
  level: number;
}
