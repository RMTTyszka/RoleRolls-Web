import { Component, OnInit } from '@angular/core';
import { MonstersService } from '../monsters/monsters.service';
import { Monster } from '../shared/models/Monster.model';
import { CombatService } from './combat.service';

@Component({
  selector: 'loh-combat',
  templateUrl: './combat.component.html',
  styleUrls: ['./combat.component.css']
})
export class CombatComponent implements OnInit {

  monsters: Monster[] = [];
  constructor(
    private _monsterService: MonstersService,
    private _combatService: CombatService
  ) { }

  ngOnInit() {
    this._monsterService.getAll().subscribe(data => this.monsters = data);
  }

  simulateAttack() {
    this._combatService.fullAttackSimulated(this.monsters[0].id, this.monsters[0].id).subscribe(() => {
    });
  }

}
