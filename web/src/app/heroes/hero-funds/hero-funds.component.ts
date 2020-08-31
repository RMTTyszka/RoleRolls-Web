import {Component, Input, OnInit} from '@angular/core';
import {Hero} from '../../shared/models/NewHero.model';

@Component({
  selector: 'loh-hero-funds',
  templateUrl: './hero-funds.component.html',
  styleUrls: ['./hero-funds.component.css']
})
export class HeroFundsComponent implements OnInit {

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
  constructor() { }

  ngOnInit(): void {
  }

}
