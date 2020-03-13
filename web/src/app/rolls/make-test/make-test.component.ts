import { Component, OnInit } from '@angular/core';
import {FormArray, FormBuilder, FormGroup} from '@angular/forms';
import {MakeTestService} from './make-test.service';
import {RollResult} from '../../shared/models/RollResult.model';

@Component({
  selector: 'loh-make-test',
  templateUrl: './make-test.component.html',
  styleUrls: ['./make-test.component.css']
})
export class MakeTestComponent implements OnInit {
  form: FormGroup;
  difficulty: RegExp = /^\d{1,2}\/\d$/;

  constructor(
    private makeTestService: MakeTestService,
    private fb: FormBuilder
  ) {
    this.form = this.fb.group(
      {
        level: [],
        bonus: [],
        difficulty: [],
        rolls: this.fb.array([this.fb.control('')]),
        criticalSuccesses: [],
        criticalFailures: [],
        success: [],
        bonusDice: [],
        successes: []
      });
  }

  ngOnInit() {
  }

  get rolls(): FormArray {
    return this.form.get('rolls') as FormArray;
  }
  makeTest() {
    this.makeTestService.makeTest(
      this.form.get('level').value,
      this.form.get('bonus').value,
      (this.form.get('difficulty').value as String).split('/')[0],
      (this.form.get('difficulty').value as String).split('/')[1]).subscribe((rollResult: RollResult) => {
        this.form.get('success').setValue(rollResult.success);
        this.form.get('criticalSuccesses').setValue(rollResult.criticalSuccesses);
        this.form.get('criticalFailures').setValue(rollResult.criticalFailures);
        this.form.get('successes').setValue(rollResult.successes);
        this.form.get('bonusDice').setValue(rollResult.bonusDice);
        this.rolls.clear();
        rollResult.rolls.forEach(roll => this.rolls.push(this.fb.control(roll)));
    });
  }

}
