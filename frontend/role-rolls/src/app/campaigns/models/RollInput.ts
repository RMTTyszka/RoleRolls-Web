import { RollOrigin } from '../../models/RollOrigin';

export interface RollInput {
  propertyName: string;
  propertyValue: number;
  propertyId: string;
  propertyType: RollOrigin;
  complexity: number;
  difficulty: number;
  propertyBonus: number;
  rollBonus: number;
  hidden: boolean;
  creatureId: string;
  rollsAsString: string;
  rolls: number[];
  description: string;
}
