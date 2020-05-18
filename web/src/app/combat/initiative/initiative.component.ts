import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Initiative} from '../../shared/models/Iniciative.model';
import {Creature} from '../../shared/models/creatures/Creature.model';
import {CombatManagementService} from '../combat-management.service';

@Component({
  selector: 'loh-initiative',
  templateUrl: './initiative.component.html',
  styleUrls: ['./initiative.component.css']
})
export class InitiativeComponent implements OnInit {

   initiatives: Initiative[] = [];
  currentCreatureId: string;
  constructor(
    private _combatManagement: CombatManagementService,
  ) { }

  ngOnInit() {
    this._combatManagement.combatUpdated.subscribe(combat => {
      this.initiatives = combat.initiatives;
      this.currentCreatureId = combat.currentInitiative ? combat.currentInitiative.creature.id : null;
    });
  }

  isCurrentCreatureTurn(creatureId: string) {
    return creatureId === this.currentCreatureId;
  }

}
