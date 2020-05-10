import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {map} from 'rxjs/operators';
import {MonsterService} from '../../monster/monster.service';
import {Monster} from '../../../shared/models/Monster.model';
import {Hero} from '../../../shared/models/NewHero.model';
import {NewHeroService} from '../../../heroes/new-hero.service';

@Component({
  selector: 'loh-monster-select',
  templateUrl: './monster-select.component.html',
  styleUrls: ['./monster-select.component.css']
})
export class MonsterSelectComponent implements OnInit {
  @Output() monsterSelected = new EventEmitter<Hero>();
  @Input() monster: Monster;
  constructor(
    private service: NewHeroService,
  ) {
  }

  ngOnInit() {
  }

  creatureSelect(monster: Monster) {
    this.monsterSelected.next(monster);
  }
}
