import {Component, OnInit} from '@angular/core';
import {EncountersService} from '../encounters.service';
import {MatDialog} from '@angular/material/dialog';
import {EncounterCreateEditComponent} from '../encounter-create-edit/encounter-create-edit.component';
import {Encounter} from '../../shared/models/Encounter.model';

@Component({
  selector: 'loh-encounters',
  templateUrl: './encounters.component.html',
  styleUrls: ['./encounters.component.css']
})
export class EncountersComponent implements OnInit {

  encounters: Encounter[];

  constructor(
    private _encounterService: EncountersService,
    public dialog: MatDialog
    ) {
  }
  ngOnInit() {
    this._encounterService.getAll().subscribe(resp => console.log(resp));
  }

  createNewEncounter() {
    const encounter = new Encounter();
    this.dialog.open(EncounterCreateEditComponent,
      {
        width: '280px',
        data: encounter
      }).afterClosed().subscribe();
  }

}
