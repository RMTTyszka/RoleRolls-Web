import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { CampaignScene } from 'src/app/shared/models/pocket/campaigns/campaign-scene-model';
import { PocketCampaignModel } from 'src/app/shared/models/pocket/campaigns/pocket.campaign.model';

@Injectable()
export class PocketCampaignDetailsService {

  public sceneChanged = new Subject<CampaignScene>();
  public campaignLoaded = new Subject<PocketCampaignModel>();
  constructor() { }
}
