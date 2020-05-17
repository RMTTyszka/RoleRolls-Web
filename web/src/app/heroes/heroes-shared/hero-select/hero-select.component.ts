import {Component, EventEmitter, Input, OnDestroy, OnInit, Output} from '@angular/core';
import {NewHeroService} from '../../new-hero.service';
import {Hero} from '../../../shared/models/NewHero.model';
import {Combat} from '../../../shared/models/combat/Combat.model';

@Component({
  selector: 'loh-hero-select',
  templateUrl: './hero-select.component.html',
  styleUrls: ['./hero-select.component.css']
})
export class HeroSelectComponent implements OnInit {
  @Output() heroSelected = new EventEmitter<Hero>();
  @Input() hero: Hero;
  @Input() combat: Combat;
  constructor(
    private service: NewHeroService,
  ) {
  }

  ngOnInit() {
  }

  creatureSelect(hero: Hero) {
    this.heroSelected.next(hero);
  }
}
