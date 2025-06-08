import { Injectable, signal } from '@angular/core';
import { BehaviorSubject, Subject } from 'rxjs';
import { CampaignScene } from '@app/campaigns/models/campaign-scene-model';
import { Campaign } from '@app/campaigns/models/campaign';
import { Creature } from '@app/campaigns/models/creature';

@Injectable({
  providedIn: 'root'
})
export class CampaignSessionService {

  public sceneChanged = new BehaviorSubject<CampaignScene>(null);
  public campaignLoaded = new BehaviorSubject<Campaign>(null);
  public heroAddedToScene = new Subject<void>();
  public heroRemovedToScene = new Subject<void>();
  public monsterAddedToScene = new Subject<void>();
  public monsterRemovedToScene = new Subject<void>();
  public heroTookDamage = new Subject<void>();
  public campaign: Campaign;
  public heroes  = signal<Creature[]>([]);
  public monsters  = signal<Creature[]>([]);

  public get currentScene(): CampaignScene {
    if (this.campaign) {
      return JSON.parse(localStorage.getItem(this.campaign.id + 'CurrentScene')) as CampaignScene;
    }
    return null;
  }
  constructor(
  ) {
    this.sceneChanged.subscribe((scene: CampaignScene) => {
      if (scene) {
        localStorage.setItem(scene.campaignId + 'CurrentScene', JSON.stringify(scene))
      }
    });
    this.campaignLoaded.subscribe((campaign: Campaign) => {
      this.campaign = campaign;
    });
  }

  public clear() {
    this.sceneChanged.next(null);
    this.campaignLoaded.next(null);
  }
}
