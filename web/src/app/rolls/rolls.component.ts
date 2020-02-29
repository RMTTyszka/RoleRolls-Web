import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'loh-rolls',
  templateUrl: './rolls.component.html',
  styleUrls: ['./rolls.component.css']
})
export class RollsComponent implements OnInit {
  @Input() prop: string;
  @Input() size = 20;
  @Input() points = 18;
  @Input() level = 4;
  @Input() bonusDice = 8;
  @Input() hitBonus = 0;
  @Input() cd = 10;
  @Input() totalHits = 0;
  @Input() weapDamage = 10;
  @Input() attribute = 4;
  @Input() damageBonus = 1;
  @Input() bonusMod = 2;
  @Input() attrMod = 2;
  @Input() hits: number[] = [];
  @Input() rolls: number[] = [];
  @Input() damages: number[] = [];


  @Input() isAttack: boolean;



  constructor(
  ) { }

  ngOnInit() {
  }

  getLevel() {
    this.level = Math.floor((this.points + 4) / 5);
    this.bonusDice = Math.floor((this.points + 4) % 5) * 4;
  }
  getRoll() {
    this.rolls = [];
    this.hits = [];
    for ( let i = 0; i < this.level; i++) {
      const val: number = Math.floor(Math.random() * this.size + 1);
      this.rolls.push(val);
      console.log(this.rolls[i]);
    }
    if (this.bonusDice === 0) {
      this.getHits();
    }

  }

  addBDice(i: number) {
    this.rolls[i] += Math.floor(Math.random() * this.bonusDice + 1);
    this.bonusDice = 0;
    this.getHits();
  }
  getHits() {
    this.hits = [];
    this.totalHits = 0;
    this.rolls.forEach(result => {
      if (result >= this.cd) {
        this.hits.push(1);
        this.totalHits++;
      } else {
        this.hits.push(0);
      }
    });
  }
  getDamages() {
    this.damages = [];
    for (let i = 0; i < this.totalHits; i++) {
      this.damages.push(this.getDamage());

    }
  }
  getDamage(): number {
    return Math.floor(Math.random() * this.weapDamage + 1 )
            + (this.attribute * this. attrMod)
            + (this.damageBonus * this.bonusMod);
  }

  clickTest() {
    console.log(this.isAttack);
  }


}
