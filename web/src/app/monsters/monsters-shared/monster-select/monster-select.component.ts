import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {map} from 'rxjs/operators';
import {MonsterService} from '../../monster/monster.service';
import {Monster} from '../../../shared/models/Monster.model';
import {Hero} from '../../../shared/models/NewHero.model';
import {Combat} from '../../../shared/models/combat/Combat.model';

@Component({
  selector: 'loh-monster-select',
  templateUrl: './monster-select.component.html',
  styleUrls: ['./monster-select.component.css']
})
export class MonsterSelectComponent implements OnInit {
  @Output() monsterSelected = new EventEmitter<Monster>();
  @Input() monster: Monster;
  @Input() combat: Combat;
  constructor(
    private service: MonsterService,
  ) {
  }

  ngOnInit() {
  }

  creatureSelect(monster: Monster) {
    this.monsterSelected.next(monster);
  }
}
