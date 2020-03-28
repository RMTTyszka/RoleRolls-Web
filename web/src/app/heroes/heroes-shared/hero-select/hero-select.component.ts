import {Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild, ViewChildren} from '@angular/core';
import {FormGroup, FormGroupDirective} from '@angular/forms';
import {Race} from '../../../shared/models/Race.model';
import {RaceService} from '../../../races/race-editor/race.service';
import {map, tap} from 'rxjs/operators';
import {createForm} from '../../../shared/EditorExtension';
import {Hero} from '../../../shared/models/Hero.model';
import {NewHeroService} from '../../new-hero.service';
import {NewHero} from '../../../shared/models/NewHero.model';
import {OverlayPanel} from 'primeng/overlaypanel';

@Component({
  selector: 'loh-hero-select',
  templateUrl: './hero-select.component.html',
  styleUrls: ['./hero-select.component.css']
})
export class HeroSelectComponent implements OnInit {
  @Output() heroSelected = new EventEmitter<NewHero>();
  result: NewHero[] = [];
  hero: NewHero;
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
  selected(hero: NewHero) {
    this.hero = hero;
    this.heroSelected.emit(hero);
  }

}
