import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {map} from 'rxjs/operators';
import {NewHeroService} from '../../new-hero.service';
import {Hero} from '../../../shared/models/NewHero.model';
import {EffectType} from '../../../shared/models/effects/EffectType.model';

@Component({
  selector: 'loh-hero-select',
  templateUrl: './hero-select.component.html',
  styleUrls: ['./hero-select.component.css']
})
export class HeroSelectComponent implements OnInit {
  @Output() heroSelected = new EventEmitter<Hero>();
  result: Hero[] = [];
  @Input() hero: Hero;
  effectType = EffectType;
  constructor(
    private service: NewHeroService,
  ) {
  }

  ngOnInit() {
  }

  search(event) {
    this.service.getAllFiltered(event).pipe(
      map(resp => resp.map(hero => hero))
    ).subscribe(response => this.result = response);
  }
  selected(hero: Hero) {
    this.hero = hero;
    this.heroSelected.emit(hero);
  }

  getEffect(effectType: EffectType) {
    return this.hero.effects.find(effect => effect.effectType === effectType) || null;
  }

}
