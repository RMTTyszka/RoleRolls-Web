import {Component, EventEmitter, Injector, OnInit, Output} from '@angular/core';
import {LegacyBaseListComponent} from '../../shared/base-list/legacy-base-list.component';
import {Race} from '../../shared/models/Race.model';
import {Combat} from '../../shared/models/combat/Combat.model';
import {RacesService} from '../../races/races.service';
import {CombatService} from '../combat.service';
import {Router} from '@angular/router';
import {RRColumns} from '../../shared/components/cm-grid/cm-grid.component';

@Component({
  selector: 'rr-combat-list',
  templateUrl: './combat-list.component.html',
  styleUrls: ['./combat-list.component.css']
})
export class CombatListComponent extends LegacyBaseListComponent<Combat> implements OnInit {
  columns: RRColumns[] = [
    {
      header: 'Name',
      property: 'name'
    },
  ];
  useRoute = false;
  @Output() combatSelected = new EventEmitter<Combat>();
  constructor(
    injector: Injector,
    protected service: CombatService,
  ) {
    super(injector, service);
  }

  ngOnInit() {
  }
  add() {
    this.combatSelected.next(new Combat());
  }

}
