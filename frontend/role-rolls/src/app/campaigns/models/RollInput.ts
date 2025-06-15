import { Property } from '@app/models/bonuses/bonus';
import {Creature} from '@app/campaigns/models/creature';

export interface RollInput {
  propertyName: string;
  propertyValue: number;
  property: Property;
  attribute?: Property | null;
  numberOfDices: number
  complexity: number;
  difficulty: number;
  advantage: number;
  luck: number;
  bonus: number;
  hidden: boolean;
  creature: Creature;
  rollsAsString: string[];
  rolls: number[];
  description: string;
}
