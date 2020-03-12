import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup} from '@angular/forms';
import {MakeTestService} from './make-test.service';
import {RollResult} from '../../shared/models/RollResult.model';

@Component({
  selector: 'loh-make-test',
  templateUrl: './make-test.component.html',
  styleUrls: ['./make-test.component.css']
})
export class MakeTestComponent implements OnInit {
  form: FormGroup;

  constructor(
    private makeTestService: MakeTestService,
    private fb: FormBuilder
  ) {
    this.form = this.fb.group(
      {
        level: [],
        bonus: [],
        difficulty: [],
        complexity: []
      });
  }

  ngOnInit() {
  }

  makeTest() {
    this.makeTestService.makeTest(
      this.form.get('level').value,
      this.form.get('bonus').value,
      this.form.get('difficulty').value,
      this.form.get('complexity').value).subscribe((rollResult: RollResult) => console.log(rollResult));
  }

}
