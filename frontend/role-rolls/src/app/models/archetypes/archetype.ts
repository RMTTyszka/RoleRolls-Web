import {Bonus} from '@app/models/bonuses/bonus';
import { Spell } from '@app/models/spells/spell';

export interface Archetype {
  id: string;
  name: string;
  description: string;
  bonuses: Bonus[];
  powerDescriptions: ArchetypePowerDescription[];
  spells?: Spell[];
}
export interface ArchetypePowerDescription {
  id: string;
  description: string;
  name: string;
  gameDescription: string;
  level: number;
}
