import {Component, EventEmitter, Input, OnDestroy, OnInit, Output} from '@angular/core';
import {Hero} from '../../../shared/models/NewHero.model';
import {Combat} from '../../../shared/models/combat/Combat.model';
import {HeroesService} from '../../heroes.service';
import {CampaignCombatHeroService} from '../../../creatures-shared/creature-base-select/campaign-combat-hero.service';

@Component({
  selector: 'rr-hero-select',
  templateUrl: './hero-select.component.html',
  styleUrls: ['./hero-select.component.css']
})
export class HeroSelectComponent implements OnInit {
  @Output() heroSelected = new EventEmitter<Hero>();
  @Input() hero: Hero;
  @Input() combat: Combat;
  constructor(
    private service: CampaignCombatHeroService,
  ) {
  }

  ngOnInit() {
  }

  creatureSelect(hero: Hero) {
    this.heroSelected.next(hero);
  }
}
