import { GetListInput } from '@app/tokens/get-list-input';
import { CreatureCategory } from '@app/campaigns/models/CreatureCategory';

export class GetAllCampaignCreaturesInput extends GetListInput {
  public creatureCategory: CreatureCategory
}
