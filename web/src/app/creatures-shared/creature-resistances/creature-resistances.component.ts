import {Component, Input, OnInit} from '@angular/core';
import {FormGroup, FormGroupDirective} from '@angular/forms';

@Component({
  selector: 'loh-creature-resistances',
  templateUrl: './creature-resistances.component.html',
  styleUrls: ['./creature-resistances.component.css']
})
export class CreatureResistancesComponent implements OnInit {

  resistances: string[] = ['fear', 'health', 'magic', 'physical', 'reflex'];
  form: FormGroup;
  @Input() resistancesFormName = 'resistances'
  constructor(
    private formDirective: FormGroupDirective
  ) {

  }

  ngOnInit() {
    this.form = this.formDirective.form;
  }

}
