import { Component, OnInit } from '@angular/core';
import {FormGroup, FormGroupDirective} from '@angular/forms';

@Component({
  selector: 'loh-hero-stats',
  templateUrl: './hero-stats.component.html',
  styleUrls: ['./hero-stats.component.css']
})
export class HeroStatsComponent implements OnInit {

  stats: string[] = ['defense', 'evasion', 'life', 'moral', 'dodge', 'specialAttack', 'magicDefense'];
  form: FormGroup;
  constructor(
    private formDirective: FormGroupDirective
  ) {

  }

  ngOnInit() {
    this.form = this.formDirective.form;
  }

}
