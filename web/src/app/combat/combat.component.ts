import {Component, OnInit} from '@angular/core';
import {CombatService} from './combat.service';
import {HeroesService} from '../heroes/heroes.service';
import {Hero} from '../shared/models/NewHero.model';
import {AttackDetails} from '../shared/models/AttackDetails.model';
import {Combat} from '../shared/models/Combat.model';
import {Monster} from '../shared/models/Monster.model';
import {Creature} from '../shared/models/Creature.model';

@Component({
  selector: 'loh-combat',
  templateUrl: './combat.component.html',
  styleUrls: ['./combat.component.css']
})
export class CombatComponent implements OnInit {
  combat: Combat = new Combat();
  attackDetails: AttackDetails;
  attackTargetsByIds: Map<string, string> = new Map<string, string>();
  constructor(
    private heroService: HeroesService,
    private _combatService: CombatService
  ) { }

  ngOnInit() {
  }

  get heroesTargets() {
    return this.monsters.concat(this.heroes);
  }
  get monsterTargets() {
    return this.heroes.concat(this.monsters);
  }
  get heroes() {
    return this.combat.heroes;
  }
  get monsters() {
    return this.combat.monsters;
  }
  heroSelected(hero: Hero, i: number) {
    this.heroes[i] = hero;
  }
  heroTargetSelected(hero: Hero, target: Creature) {
    this.attackTargetsByIds[hero.id] = target.id;
  }
  monsterTargetSelected(monster: Monster, target: Creature) {
    this.attackTargetsByIds[monster.id] = target.id;
  }
  monsterSelected(monster: Monster, i: number) {
    this.monsters[i] = monster;
  }

  heroFullAttack(attacker: Creature) {
    this._combatService.fullAttack(attacker.id,  this.attackTargetsByIds[attacker.id]).subscribe((val) => {
      console.log(val);
      this.attackDetails = val.attackDetails;
    });
  }

  addHero() {
    this.heroes.push(new Hero());
  }
  addMonster() {
    this.monsters.push(new Monster());
  }

  y() {
    return this.heroes.map(e => e.name);
  }
}

