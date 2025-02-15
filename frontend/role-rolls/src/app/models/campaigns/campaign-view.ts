import { Entity } from '@app/models/Entity.model';

export interface CampaignView extends Entity {
  id: string;
  name: string;
  description: string;
  templateName: string;
  masterId: string;
}
