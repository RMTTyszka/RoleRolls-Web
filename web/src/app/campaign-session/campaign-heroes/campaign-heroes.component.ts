import { Component, OnInit } from '@angular/core';
import {CampaignSessionService} from '../campaign-session.service';
import {Hero} from '../../shared/models/NewHero.model';
import {Campaign} from '../../shared/models/campaign/Campaign.model';

@Component({
  selector: 'loh-campaign-heroes',
  templateUrl: './campaign-heroes.component.html',
  styleUrls: ['./campaign-heroes.component.css']
})
export class CampaignHeroesComponent implements OnInit {
  public heroes: Hero[];
  selectedHero: Hero;
  public get isMaster(): boolean {
    return this.campaignsService.isMaster;
  }
  public get campaign(): Campaign {
    return this.campaignsService.campaign;
  }
  constructor(
    private campaignsService: CampaignSessionService
  ) {
    this.campaignsService.heroesChanged.subscribe((heroes: Hero[]) => {
      this.heroes = heroes;
    });
  }

  ngOnInit(): void {
  }

  heroSelected(hero: Hero) {
    this.selectedHero = hero;
  }
}
