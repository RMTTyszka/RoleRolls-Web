import {Component, Input, OnInit} from '@angular/core';
import {Hero} from '../../shared/models/NewHero.model';
import {HeroManagementService} from '../../heroes/hero-management.service';
import {FormGroup, FormGroupDirective} from '@angular/forms';

@Component({
  selector: 'loh-hero-funds',
  templateUrl: './hero-funds.component.html',
  styleUrls: ['./hero-funds.component.css']
})
export class HeroFundsComponent implements OnInit {
  form: FormGroup;
  @Input() formGroupName = 'inventory'
  @Input() hero: Hero;
  public get cash1() {
    return this.hero.inventory.cash1;
  }
  public get cash2() {
    return this.hero.inventory.cash2;
  }
  public get cash3() {
    return this.hero.inventory.cash3;
  }
  constructor(
    private heroManagementService: HeroManagementService,
    private formGroupDirective: FormGroupDirective,
  ) {
  }

  ngOnInit(): void {
    this.form = this.formGroupDirective.form.get(this.formGroupName) as FormGroup;
    this.heroManagementService.updateFunds.subscribe(value => {
      let funds = this.form.get('cash1').value;
      funds -= value;
      this.form.get('cash1').setValue(funds);
    });
  }


}
