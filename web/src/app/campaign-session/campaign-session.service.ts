import { Injectable } from '@angular/core';
import {CampaignsService} from '../campaign/campaigns.service';
import {BehaviorSubject, of} from 'rxjs';
import {Hero} from '../shared/models/NewHero.model';
import {Campaign} from '../shared/models/campaign/Campaign.model';
import {tap} from 'rxjs/operators';
import {CreatureRollResult} from '../shared/models/rolls/CreatureRollResult';
import {Encounter} from '../shared/models/Encounter.model';

@Injectable()
export class CampaignSessionService {
  public campaign: Campaign;
  public heroesChanged = new BehaviorSubject<Hero[]>([]);
  public heroChanged = new BehaviorSubject<Hero>(new Hero());
  public campaignChanged = new BehaviorSubject<Campaign>(new Campaign());
  public encountersChanged = new BehaviorSubject<Encounter[]>([]);
  public isMaster = false;
  constructor(
    private campaignsService: CampaignsService
  ) {
  }

  getCampaign(campaignId: string) {
    return this.campaignsService.get(campaignId)
      .pipe(tap((campaign: Campaign) => {
        this.campaign = campaign;
        this.isMaster = campaign.master;
        this.heroesChanged.next(this.campaign.heroes);
        this.campaignChanged.next(this.campaign);
        this.encountersChanged.next(this.campaign.encounters);
      }));
  }

  getHeroesFromCampaign() {
    return of(this.campaign.heroes);
  }
  getEncountersFromCampaign() {
    return of(this.campaign.encounters);
  }
  saveRoll(campaignId: string, roll: CreatureRollResult) {
    return this.campaignsService.saveRoll(campaignId, roll);
  }
  getRolls(campaignId: string) {
    return this.campaignsService.getRolls(campaignId);
  }
}
