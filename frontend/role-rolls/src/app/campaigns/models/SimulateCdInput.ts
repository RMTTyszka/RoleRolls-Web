import {RollOrigin} from "../CampaignInstance/campaign-heroes/RollOrigin";

export class SimulateCdInput {
  public propertyId: string;
  public propertyType: RollOrigin;
  public expectedChance: number;
  public creatureId: string;
}
