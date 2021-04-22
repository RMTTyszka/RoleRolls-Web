import {Component, Inject, OnInit} from '@angular/core';
import {EncountersService} from '../encounters.service';
import {Encounter} from '../../shared/models/Encounter.model';
import {FormArray, FormBuilder, FormControl, FormGroup} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';

@Component({
  selector: 'rr-encounter-create-edit',
  templateUrl: './encounter-create-edit.component.html',
  styleUrls: ['./encounter-create-edit.component.css']
})
export class EncounterCreateEditComponent implements OnInit {

  encounter: Encounter;

  form: FormGroup;
  monsters: FormArray;

  constructor(
    private dialogRef: MatDialogRef<EncounterCreateEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private _encounterService: EncountersService,
    private fb: FormBuilder
    ) { }

  ngOnInit() {
    this.encounter = this.data;
    this.createForm();
  }

  createForm() {
    this.form = this.fb.group({
      name: [this.encounter.name],
      level: [this.encounter.level],
      monsters: this.fb.array(this.encounter.monsters)}
    );
    this.monsters = <FormArray>this.form.get('monsters');
  }
  addMonster() {
    this.monsters.push(new FormControl());
    console.log(JSON.stringify(this.form.value));
  }
}
