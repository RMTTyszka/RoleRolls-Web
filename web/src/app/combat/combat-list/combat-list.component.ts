import {Component, ElementRef, EventEmitter, Injector, OnInit, Output, ViewChild} from '@angular/core';
import {LegacyBaseListComponent} from '../../shared/base-list/legacy-base-list.component';
import {Race} from '../../shared/models/Race.model';
import {Combat} from '../../shared/models/combat/Combat.model';
import {RacesService} from '../../races/races.service';
import {CombatService} from '../combat.service';
import {Router} from '@angular/router';
import {RRColumns} from '../../shared/components/cm-grid/cm-grid.component';
import {DialogService} from 'primeng/dynamicdialog';
import {RrSelectModalComponent} from '../../shared/components/rr-select-modal/rr-select-modal.component';
import {Encounter} from '../../shared/models/Encounter.model';
import {ModalEntityData} from '../../shared/dtos/ModalEntityData';
import {RRSelectModalInjector} from '../../shared/components/rr-select-field/rr-select-field.component';
import {EncountersService} from '../../encounters/encounters.service';

@Component({
  selector: 'rr-combat-list',
  templateUrl: './combat-list.component.html',
  styleUrls: ['./combat-list.component.css'],
  providers: [DialogService]
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
  @Output() encounterSelected = new EventEmitter<Encounter>();
  @ViewChild('aa') aa: ElementRef;
  constructor(
    injector: Injector,
    public service: CombatService,
    public dialogService: DialogService,
    public encountersService: EncountersService,
  ) {
    super(injector, service);
  }

  ngOnInit() {
  }
  add() {
    this.combatSelected.next(new Combat());
  }

  addEncounter() {
    this.dialogService.open(RrSelectModalComponent, {
      data: <RRSelectModalInjector<Encounter>> {
        service: this.encountersService
      }
    }).onClose.subscribe((encounter: Encounter) => {
      if (encounter) {
        this.encounterSelected.next(encounter);
      }
    });
  }
}
