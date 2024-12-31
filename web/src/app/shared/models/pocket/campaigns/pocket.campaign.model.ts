import { Entity } from "../../Entity.model";
import { CreatureTemplateModel } from "../creature-templates/creature-template.model";
import {
  ItemConfigurationModel
} from 'src/app/pocket-role-rolls/campaigns/CampaignEditor/item-configuration/models/item-configuration-model';

export class PocketCampaignModel extends Entity {
  public masterId: string;
  public campaignTemplateId: string;
  public copy: boolean;
  public creatureTemplate: CreatureTemplateModel;
  public itemConfiguration: ItemConfigurationModel;
  constructor() {
    super();
    this.creatureTemplate = new CreatureTemplateModel();
    this.itemConfiguration = new ItemConfigurationModel();
  }
}
export enum PropertyType {
  All = 0,
  Attribute = 1,
  Skill = 2,
  MinorSkill = 3,
  Defense = 4,
  Life = 5,
}
