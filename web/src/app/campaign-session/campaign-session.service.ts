import { Injectable } from '@angular/core';
import {CampaignsService} from '../campaign/campaigns.service';
import {BehaviorSubject} from 'rxjs';
import {Hero} from '../shared/models/NewHero.model';
import {Campaign} from '../shared/models/campaign/Campaign.model';
import {tap} from 'rxjs/operators';

@Injectable()
export class CampaignSessionService {
  public campaign: Campaign;
  public heroesChanged = new BehaviorSubject<Hero[]>([]);
  public heroChanged = new BehaviorSubject<Hero>(new Hero());
  constructor(
    private campaignsService: CampaignsService
  ) { }

  getCampaign(campaignId: string) {
    return this.campaignsService.get(campaignId)
      .pipe(tap((campaign: Campaign) => {
        this.campaign = campaign;
        this.heroesChanged.next(this.campaign.heroes);
      }));
  }
}
