import {Component, OnInit} from '@angular/core';
import {EncountersService} from '../encounters.service';
import {MatDialog} from '@angular/material/dialog';
import {EncounterCreateEditComponent} from '../encounter-create-edit/encounter-create-edit.component';
import {Encounter} from '../../shared/models/Encounter.model';
import {EncounterConfig} from '../encounter-config';

@Component({
  selector: 'rr-encounters',
  templateUrl: './encounters.component.html',
  styleUrls: ['./encounters.component.css']
})
export class EncountersComponent implements OnInit {

  public config = new EncounterConfig();
  constructor(
    public service: EncountersService,
    public dialog: MatDialog
    ) {
  }
  ngOnInit() {
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
