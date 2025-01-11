import { Entity } from "../../models/Entity.model";
import { CreatureTemplateModel } from './creature-template.model';
import { ItemConfigurationModel } from './item-configuration-model';

export interface Campaign extends Entity {
  name: string;
  masterId: string;
  campaignTemplateId: string | null;
  copy: boolean;
  creatureTemplate: CreatureTemplateModel;
  itemConfiguration: ItemConfigurationModel;
}
