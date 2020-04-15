import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {map} from 'rxjs/operators';
import {NewHeroService} from '../../new-hero.service';
import {Hero} from '../../../shared/models/NewHero.model';

@Component({
  selector: 'loh-hero-select',
  templateUrl: './hero-select.component.html',
  styleUrls: ['./hero-select.component.css']
})
export class HeroSelectComponent implements OnInit {
  @Output() heroSelected = new EventEmitter<Hero>();
  result: Hero[] = [];
  @Input() hero: Hero;
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

}
