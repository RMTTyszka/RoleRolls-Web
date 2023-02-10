import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { CampaignScene } from 'src/app/shared/models/pocket/campaigns/campaign-scene-model';

@Injectable()
export class PocketCampaignDetailsService {

  public sceneChanged = new Subject<CampaignScene>();
  constructor() { }
}
