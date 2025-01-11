import { Entity } from "../../models/Entity.model";
import { ItemConfigurationModel } from './item-configuration-model';
import {CampaignTemplateModel} from './campaign-template.model';

export interface Campaign extends Entity {
  name: string;
  masterId: string;
  campaignTemplateId: string | null;
  copy: boolean;
  campaignTemplate: CampaignTemplateModel;
  itemConfiguration: ItemConfigurationModel;
}
