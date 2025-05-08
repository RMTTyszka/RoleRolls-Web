import { Property } from '@app/models/bonuses/bonus';

export interface RollInput {
  propertyName: string;
  propertyValue: number;
  property: Property;
  complexity: number;
  difficulty: number;
  advantage: number;
  bonus: number;
  hidden: boolean;
  creatureId: string;
  rollsAsString: string;
  rolls: number[];
  description: string;
}
