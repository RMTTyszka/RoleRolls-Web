import { Injectable } from '@angular/core';
import { Subject, ReplaySubject, BehaviorSubject } from 'rxjs';
import { CampaignScene } from 'src/app/shared/models/pocket/campaigns/campaign-scene-model';
import { PocketCampaignModel } from 'src/app/shared/models/pocket/campaigns/pocket.campaign.model';
import { v4 as uuidv4 } from 'uuid';

@Injectable({
  providedIn: 'root'
})
export class PocketCampaignDetailsService {

  public sceneChanged = new BehaviorSubject<CampaignScene>(null);
  public campaignLoaded = new BehaviorSubject<PocketCampaignModel>(null);
  public heroAddedToScene = new Subject<void>();
  public heroRemovedToScene = new Subject<void>();
  public monsterAddedToScene = new Subject<void>();
  public monsterRemovedToScene = new Subject<void>();
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
