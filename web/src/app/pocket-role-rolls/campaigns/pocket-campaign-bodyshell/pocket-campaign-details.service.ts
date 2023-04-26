import { Injectable } from '@angular/core';
import { Subject, ReplaySubject } from 'rxjs';
import { CampaignScene } from '../../../shared/models/pocket/campaigns/campaign-scene-model';
import { PocketCampaignModel } from '../../../shared/models/pocket/campaigns/pocket.campaign.model';
import { StorageService } from 'ngx-webstorage-service';
import { tap } from 'rxjs/operators';
import { RollInput } from '../models/RollInput';
import { TakeDamageInput } from '../models/TakeDamangeInput';

@Injectable()
export class PocketCampaignDetailsService {

  public sceneChanged = new Subject<CampaignScene>();
  public campaignLoaded = new Subject<PocketCampaignModel>();
  public heroAddedToScene = new Subject<void>();
  public monsterAddedToScene = new Subject<void>();
  public heroTookDamage = new Subject<void>();
  public campaign: PocketCampaignModel;
  public rollInputEmitter = new Subject<RollInput>();
  public rollResultEmitter = new Subject<boolean>();
  public takeDamageInputEmitter = new Subject<TakeDamageInput>();
  
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
