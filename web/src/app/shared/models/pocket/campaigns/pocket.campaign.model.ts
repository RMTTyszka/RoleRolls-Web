import { Entity } from "../../Entity.model";
import { CreatureTemplateModel } from "../creature-templates/creature-template.model";

export class PocketCampaignModel extends Entity {
  public masterId: string;
  public creatureTemplateId: string;
  public creatureTemplate: CreatureTemplateModel;
  constructor() {
    super();
    this.creatureTemplate = new CreatureTemplateModel();
  }
}
