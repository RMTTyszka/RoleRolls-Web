import {Component, OnInit} from '@angular/core';
import {CombatService} from './combat.service';
import {HeroesService} from '../heroes/heroes.service';
import {NewHero} from '../shared/models/NewHero.model';
import {AttackDetails} from '../shared/models/AttackDetails.model';
import {Combat} from '../shared/models/Combat.model';

@Component({
  selector: 'loh-combat',
  templateUrl: './combat.component.html',
  styleUrls: ['./combat.component.css']
})
export class CombatComponent implements OnInit {
  combat: Combat = new Combat();
  attackDetails: AttackDetails;
  constructor(
    private heroService: HeroesService,
    private _combatService: CombatService
  ) { }

  ngOnInit() {
  }

  simulateAttack() {
    this._combatService.fullAttackSimulated(this.heroes[0].id, this.heroes[0].id).subscribe((val) => {
      console.log(val);
      this.attackDetails = val.attackDetails;
    });
  }

  get heroesTargets() {
    return this.heroes.concat(this.monsters);
  }
  get heroes() {
    return this.combat.heroes;
  }
  get monsters() {
    return this.combat.monsters;
  }
  heroSelected(hero: NewHero, i: number) {
    this.heroes.splice(i, 1, hero);
  }
  heroTargetSelected(hero: NewHero, i: number) {
    this.heroesTargets.splice(i, 1, hero);
  }
  monsterSelected(hero: NewHero, i: number) {
    this.monsters.splice(i, 1, hero);
  }

  heroFullAttack(index: number) {
    this._combatService.fullAttack(this.heroes[index].id, this.heroesTargets[index].id).subscribe((val) => {
      console.log(val);
      this.attackDetails = val.attackDetails;
    });
  }

  addHero() {
    this.heroes.push(new NewHero());
  }

  y() {
    return this.heroes.map(e => e.name);
  }
}

