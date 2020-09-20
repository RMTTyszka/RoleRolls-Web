import {Component, EventEmitter, Injector, OnInit, Output} from '@angular/core';
import {BaseListComponent} from '../../shared/base-list/base-list.component';
import {Race} from '../../shared/models/Race.model';
import {Combat} from '../../shared/models/combat/Combat.model';
import {RacesService} from '../../races/races.service';
import {CombatService} from '../combat.service';
import {Router} from '@angular/router';
import {CmColumns} from '../../shared/components/cm-grid/cm-grid.component';

@Component({
  selector: 'loh-combat-list',
  templateUrl: './combat-list.component.html',
  styleUrls: ['./combat-list.component.css']
})
export class CombatListComponent extends BaseListComponent<Combat> implements OnInit {
  columns: CmColumns[] = [
    {
      header: 'Name',
      property: 'name'
    },
  ];

  @Output() combatSelected = new EventEmitter<Combat>();
  constructor(
    injector: Injector,
    protected service: CombatService,
  ) {
    super(injector, service);
  }

  ngOnInit() {
  }

}
