import {InvitationStatus} from '../InvitationStatus';
import {Player} from '../../Player.model';

export class CampaignInvitationsOutput {
  public player: Player;
  public status: InvitationStatus;
}
