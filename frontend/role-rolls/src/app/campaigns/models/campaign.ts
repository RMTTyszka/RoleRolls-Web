import { Entity } from "../../models/Entity.model";
import { ItemConfigurationModel } from './item-configuration-model';
import {CampaignTemplate} from 'app/campaigns/models/campaign.template';

export interface Campaign extends Entity {
  name: string;
  masterId: string;
  campaignTemplateId: string | null;
  copy: boolean;
  campaignTemplate: CampaignTemplate;
}
