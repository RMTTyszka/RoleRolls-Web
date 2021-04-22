import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {map} from 'rxjs/operators';
import {MonsterService} from '../../monster/monster.service';
import {Monster} from '../../../shared/models/creatures/monsters/Monster.model';
import {Hero} from '../../../shared/models/NewHero.model';
import {Combat} from '../../../shared/models/combat/Combat.model';
import {CampaignCombatMonsterService} from '../../../creatures-shared/creature-base-select/campaign-combat-monster.service';

@Component({
  selector: 'rr-monster-select',
  templateUrl: './monster-select.component.html',
  styleUrls: ['./monster-select.component.css']
})
export class MonsterSelectComponent implements OnInit {
  @Output() monsterSelected = new EventEmitter<Monster>();
  @Input() monster: Monster;
  @Input() combat: Combat;
  constructor(
    private service: CampaignCombatMonsterService,
  ) {
  }

  ngOnInit() {
  }

  creatureSelect(monster: Monster) {
    this.monsterSelected.next(monster);
  }
}
