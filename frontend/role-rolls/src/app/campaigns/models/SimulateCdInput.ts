import { RollOrigin } from '../../models/RollOrigin';

export interface SimulateCdInput {
  propertyId: string;
  propertyType: RollOrigin;
  expectedChance: number;
  creatureId: string;
}
