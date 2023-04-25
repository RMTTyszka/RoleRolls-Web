import { Injectable } from '@angular/core';
import { Subject, ReplaySubject } from 'rxjs';
import { CampaignScene } from 'src/app/shared/models/pocket/campaigns/campaign-scene-model';
import { PocketCampaignModel } from 'src/app/shared/models/pocket/campaigns/pocket.campaign.model';
import { StorageService } from '../../../../../node_modules/ngx-webstorage-service';
import { tap } from 'rxjs/operators';

@Injectable()
export class PocketCampaignDetailsService {

  public sceneChanged = new Subject<CampaignScene>();
  public campaignLoaded = new Subject<PocketCampaignModel>();
  public heroAddedToScene = new Subject<void>();
  public heroTookDamage = new Subject<void>();
  public campaign: PocketCampaignModel;

  public get currentScene(): CampaignScene {
    if (this.campaign) {
      return JSON.parse(localStorage.getItem(this.campaign.id + 'CurrentScene')) as CampaignScene;
    }
  }
  constructor(
  ) {
    this.sceneChanged.subscribe((scene: CampaignScene) => {
      if (scene) {
        localStorage.setItem(scene.campaignId + 'CurrentScene', JSON.stringify(scene))
      }
    });
    this.campaignLoaded.subscribe((campaign: PocketCampaignModel) => {
      this.campaign = campaign;
    });
   }
}
