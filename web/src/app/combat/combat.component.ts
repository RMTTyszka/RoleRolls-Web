import {Component, OnInit} from '@angular/core';
import {CombatService} from './combat.service';
import {HeroesService} from '../heroes/heroes.service';
import {Hero} from '../shared/models/NewHero.model';
import {AttackDetails} from '../shared/models/AttackDetails.model';
import {Combat} from '../shared/models/Combat.model';
import {Monster} from '../shared/models/Monster.model';
import {Creature} from '../shared/models/creatures/Creature.model';
import {isNullOrUndefined} from 'util';
import {ActivatedRoute} from '@angular/router';
import {AddHeroToCombatInput} from '../shared/models/combat/AddHeroToCombatInput';
import {Initiative} from '../shared/models/Iniciative.model';
import {AddMonsterToCombatInput} from '../shared/models/combat/AddMonsterToCombatInput';
import {EndTurnInput} from '../shared/models/combat/EndTurnInput';
import {DialogService} from 'primeng/api';
import {CreatureType} from '../shared/models/creatures/CreatureType';
import {EffectType} from '../shared/models/effects/EffectType.model';

export class CombatActionData {
  currentTargets: Creature[] = [];
  currentCreatureActing: Creature;
}
export class AttackInput {
  attackerId: string;
  targetId: string;
}

@Component({
  selector: 'loh-combat',
  templateUrl: './combat.component.html',
  styleUrls: ['./combat.component.css'],
  providers: [DialogService]
})
export class CombatComponent implements OnInit {
  combat: Combat = new Combat();
  attackDetails: AttackDetails;
  attackTargetsByIds: Map<string, string> = new Map<string, string>();
  hasLoaded = false;
  actionModalOpened = false;
  creatureType = CreatureType;
  combatActionData: CombatActionData = new CombatActionData();
  effectType = EffectType;
  constructor(
    private heroService: HeroesService,
    private _combatService: CombatService,
    private dialog: DialogService,
    private routeSnapshot: ActivatedRoute
  ) { }

  ngOnInit() {
    if (this.routeSnapshot.snapshot.params['id']) {
      this._combatService.get(this.routeSnapshot.snapshot.params['id']).subscribe((combat) => {
        this.combat = combat;
        this.hasLoaded = true;
      });
    } else {
      this.hasLoaded = true;
    }
  }

  get isSaved(): boolean {
    return  !isNullOrUndefined(this.combat.id);
  }
  get hasStarted() {
    return this.combat.hasStarted;
  }

  get heroesTargets() {
    return this._combatService.getHeroesTargets(this.combat);
  }
  get monsterTargets() {
    return this._combatService.getMonsterTargets(this.combat);
  }
  get heroes() {
    return this.combat.heroes;
  }
  get monsters() {
    return this.combat.monsters;
  }
  heroSelected(hero: Hero, i: number) {
    if (this.isSaved) {
      this._combatService.addHero(new AddHeroToCombatInput(this.combat.id, hero))
        .subscribe((initiative: Initiative) => {
          this.heroes[i] = hero;
          this.combat.initiatives.push(initiative);
        });
    } else {
      this.heroes[i] = hero;
    }

  }
  heroTargetSelected(hero: Hero, target: Creature) {
    this.attackTargetsByIds[hero.id] = target.id;
  }
  monsterTargetSelected(monster: Monster, target: Creature) {
    this.attackTargetsByIds[monster.id] = target.id;
  }
  monsterSelected(monster: Monster, i: number) {
    if (this.isSaved) {
      this._combatService.addMonster(new AddMonsterToCombatInput(this.combat.id, monster))
        .subscribe((initiative: Initiative) => {
          this.monsters[i] = monster;
          this.updateInitiative(initiative);
        });
    } else {
      this.monsters[i] = monster;
    }
  }

  private updateInitiative(initiative: Initiative) {
    this.combat.initiatives.push(initiative);
    this.combat.initiatives = this.combat.initiatives.sort((a, b) => b.value - a.value);
  }

  heroFullAttack(attackInput: AttackInput) {
    this._combatService.fullAttack(attackInput.attackerId, attackInput.targetId).subscribe((val) => {
      this.attackDetails = val.attackDetails;
      const heroAsAttackerIndex = this.heroes.findIndex(hero => hero.id === this.attackDetails.attacker.id);
      if (heroAsAttackerIndex >= 0) {
        this.heroes[heroAsAttackerIndex] = this.attackDetails.attacker;
      }
      const heroAsTargetIndex = this.heroes.findIndex(hero => hero.id === this.attackDetails.target.id);
      if (heroAsTargetIndex >= 0) {
        this.heroes[heroAsTargetIndex] = this.attackDetails.target;
      }
      const monsterAsAttackerIndex = this.monsters.findIndex(monster => monster.id === this.attackDetails.attacker.id);
      if (monsterAsAttackerIndex >= 0) {
        this.monsters[monsterAsAttackerIndex] = this.attackDetails.attacker;
      }
      const monsterAsTargetIndex = this.monsters.findIndex(monster => monster.id === this.attackDetails.target.id);
      if (monsterAsTargetIndex >= 0) {
        this.monsters[monsterAsTargetIndex] = this.attackDetails.target;
      }
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

  saveCombat() {
    this._combatService.create(this.combat).subscribe((combat) => this.combat = combat.entity);
  }

  endTurn() {
    this._combatService.endTurn(new EndTurnInput(this.combat.id, this.combat.currentInitiative.creature.id))
      .subscribe((nextInitiative: Initiative) => {
        this.combat.currentInitiative.creature = nextInitiative.creature;
        this.combatActionData.currentCreatureActing = nextInitiative.creature;
      });
  }

  openActionBar(creature: Creature, creatureType: CreatureType) {
    this.combatActionData.currentTargets = creatureType === this.creatureType.Hero ? this.heroesTargets : this.monsterTargets;
    this.combatActionData.currentCreatureActing = creature;
    this.actionModalOpened = true;
  }

  getEffect(creature: Creature, effectType: EffectType) {
    return creature.effects.find(effect => effect.effectType === effectType) || null;
  }

}

