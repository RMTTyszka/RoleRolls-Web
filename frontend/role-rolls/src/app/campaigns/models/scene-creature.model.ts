import { CreatureCategory } from 'app/campaigns/models/CreatureCategory';

export class SceneCreature {
  public creatureId!: string;
  public creatureType!: CreatureCategory;
  public hidden!: boolean;
}
