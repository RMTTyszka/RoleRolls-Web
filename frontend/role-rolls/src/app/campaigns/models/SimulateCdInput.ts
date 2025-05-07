import { Property } from '@app/models/bonuses/bonus';

export interface SimulateCdInput {
  property: Property;
  expectedChance: number;
  creatureId: string;
}
