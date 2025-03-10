import {RollOrigin} from "../CampaignInstance/campaign-heroes/RollOrigin";

export class RollInput {
  public propertyName: string;
  public propertyValue: number;
  public propertyId: string;
  public propertyType: RollOrigin;
  public complexity: number;
  public difficulty: number;
  public propertyBonus: number;
  public rollBonus: number;
  public hidden: boolean;
  public creatureId: string;
  public rollsAsString = '';
  public rolls: number[];
  public description: string;
}
