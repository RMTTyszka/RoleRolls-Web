import {Injectable, Injector} from '@angular/core';
import {BaseEntityService} from '../shared/base-entity-service';
import {Campaign} from '../shared/models/campaign/Campaign.model';
import {InvitedPlayer} from '../shared/models/campaign/InvitedPlayer.model';
import {InvitedPlayerOutput} from '../shared/models/campaign/dtos/InvitedPlayerOutput';
import {HttpParams} from '@angular/common/http';
import {Player} from '../shared/models/Player.model';

@Injectable({
  providedIn: 'root'
})
export class CampaignsService extends BaseEntityService<Campaign> {

  path = 'campaigns';

  constructor(
    injector: Injector) {
    super(injector, Campaign);
  }

  public getInvitations() {
    return this.http.get<InvitedPlayerOutput>(this.serverUrl + this.path + '/player/invite/get');
  }
  public invitePlayer(campaignId: string, playerId: string) {
    return this.http.post(this.serverUrl + this.path + '/player/invite', {
      campaignId: campaignId,
      playerId: playerId
    });
  }
  public acceptInvitation(campaignId: string) {
    return this.http.post(this.serverUrl + this.path + `/player/add/${campaignId}`, {
    });
  }
  public denyInvitation(campaignId: string) {
    return this.http.post(this.serverUrl + this.path + `/player/invite/deny/${campaignId}`, {});
  }
  public getPlayer(campaignId: string, skipCount: number, maxResultCount: number) {
    const params = new HttpParams()
      .set('campaignId', campaignId)
      .set('skipCount', skipCount.toString())
      .set('maxResultCount', maxResultCount.toString())
    return this.http.get<Player[]>(this.serverUrl + this.path + `/player/select/`, { params: params});
  }

}
