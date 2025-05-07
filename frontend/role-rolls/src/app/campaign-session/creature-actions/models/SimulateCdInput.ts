import {PropertyType} from "../CampaignInstance/campaign-heroes/PropertyType";

export class SimulateCdInput {
  public propertyId: string;
  public propertyType: PropertyType;
  public expectedChance: number;
  public creatureId: string;
}
