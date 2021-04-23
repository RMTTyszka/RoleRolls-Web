import {Component, OnInit} from '@angular/core';
import {FormArray, FormBuilder, FormGroup} from '@angular/forms';
import {MakeTestService} from './make-test.service';
import {TestResult} from '../../shared/models/RollResult.model';

@Component({
  selector: 'rr-make-test',
  templateUrl: './make-test.component.html',
  styleUrls: ['./make-test.component.css']
})
export class MakeTestComponent implements OnInit {
  form: FormGroup;
  difficulty: RegExp = /^\d{1,2}\/\d$/;
  hasRollResult = false;
  constructor(
    private makeTestService: MakeTestService,
    private fb: FormBuilder
  ) {
    this.form = this.fb.group(
      {
        level: [14],
        bonus: [1],
        difficulty: ['12/2'],
        rolls: this.fb.array([]),
        criticalSuccesses: [],
        criticalFailures: [],
        success: [],
        bonusDice: [],
        successes: [],
        rollSuccesses: [],
        successesMessage: []
      });
  }

  ngOnInit() {
  }
  get success(): boolean {
    return this.form.get('success').value;
  }
  get rolls(): FormArray {
    return this.form.get('rolls') as FormArray;
  }
  makeTest() {
    this.makeTestService.makeTest(
      this.form.get('level').value,
      this.form.get('bonus').value || 0,
      (this.form.get('difficulty').value as String).split('/')[0],
      (this.form.get('difficulty').value as String).split('/')[1]).subscribe((rollResult: TestResult) => {
        this.form.get('successesMessage').setValue(rollResult.success ? 'Success' : 'Failure');
        this.form.get('success').setValue(rollResult.success);
        this.form.get('criticalSuccesses').setValue(rollResult.criticalSuccesses);
        this.form.get('criticalFailures').setValue(rollResult.criticalFailures);
        this.form.get('rollSuccesses').setValue(rollResult.successes);
        this.form.get('successes').setValue(rollResult.successes);
        this.form.get('bonusDice').setValue(rollResult.bonusDice);
        this.rolls.clear();
        rollResult.rolls.forEach(roll => this.rolls.push(this.fb.control(roll)));
        this.hasRollResult = true;
    });
  }

  getRollColor(value: number) {
    switch (value) {
      case 1: return 'criticalFailureResult';
      case 20: return 'criticalSuccessResult';
      default: return 'normalResult';
    }
  }
}
