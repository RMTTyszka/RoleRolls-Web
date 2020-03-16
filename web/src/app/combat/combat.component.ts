import { Component, OnInit } from '@angular/core';
import { MonstersService } from '../monsters/monsters.service';
import { Monster } from '../shared/models/Monster.model';
import { CombatService } from './combat.service';
import {HeroesService} from '../heroes/heroes.service';
import {Hero} from '../shared/models/Hero.model';
import {NewHero} from '../shared/models/NewHero.model';

@Component({
  selector: 'loh-combat',
  templateUrl: './combat.component.html',
  styleUrls: ['./combat.component.css']
})
export class CombatComponent implements OnInit {
  x;
  heroes: NewHero[] = [];
  constructor(
    private heroService: HeroesService,
    private _combatService: CombatService
  ) { }

  ngOnInit() {
    this.heroService.getAllDummies().subscribe(data => this.heroes = data);
  }

  simulateAttack() {
    this._combatService.fullAttackSimulated(this.heroes[0].id, this.heroes[0].id).subscribe((val) => {
      console.log(val);
      this.x = val;
    });
  }

}
