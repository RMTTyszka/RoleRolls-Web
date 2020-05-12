import {Component, OnDestroy, OnInit} from '@angular/core';
import {CombatService} from './combat.service';
import {Hero} from '../shared/models/NewHero.model';
import {AttackDetails} from '../shared/models/AttackDetails.model';
import {Combat} from '../shared/models/Combat.model';
import {Monster} from '../shared/models/Monster.model';
import {Creature} from '../shared/models/creatures/Creature.model';
import {isNullOrUndefined} from 'util';
import {ActivatedRoute} from '@angular/router';
import {Initiative} from '../shared/models/Iniciative.model';
import {AddOrRemoveCreatureToCombatInput} from '../shared/models/combat/AddOrRemoveCreatureToCombatInput';
import {EndTurnInput} from '../shared/models/combat/EndTurnInput';
import {DialogService} from 'primeng/api';
import {CreatureType} from '../shared/models/creatures/CreatureType';
import {EffectType} from '../shared/models/effects/EffectType.model';
import {BehaviorSubject, interval, Observable, pipe, Subject} from 'rxjs';
import {switchMap, takeUntil} from 'rxjs/operators';
import {CombatManagementService} from './combat-management.service';

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
export class CombatComponent implements OnInit, OnDestroy {

  combat: Combat = new Combat();
  attackDetails: AttackDetails;
  hasLoaded = false;
  actionModalOpened = false;
  creatureType = CreatureType;
  combatActionData: CombatActionData = new CombatActionData();
  effectType = EffectType;
  usubscriber = new Subject<void>();
  constructor(
    private _combatService: CombatService,
    private _combatManagement: CombatManagementService,
    private dialog: DialogService,
    private routeSnapshot: ActivatedRoute
  ) { }
  ngOnDestroy(): void {
    this.usubscriber.next();
    this.usubscriber.complete();
  }
  ngOnInit() {
    this._combatManagement.combatUpdated
      .pipe(takeUntil(this.usubscriber)).subscribe(combat => {
      this.combat = combat;
    })
     interval(3000)
       .pipe(takeUntil(this.usubscriber)).subscribe(() => {
       this._combatService.get(this.combat.id)
         .subscribe(combat => {
          if (this.combat.lastUpdateTime < combat.lastUpdateTime) {
            this._combatManagement.combatUpdated.next(combat);
          }
         }
         );
     })
    if (this.routeSnapshot.snapshot.params['id']) {
      this._combatService.get(this.routeSnapshot.snapshot.params['id']).subscribe((combat) => {
        this._combatManagement.combatUpdated.next(combat);
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
  removeHero(i: number) {
    const hero = this.heroes[i];
    this._combatService.removeHero(new AddOrRemoveCreatureToCombatInput<Hero>(this.combat.id, hero))
      .subscribe(combat => {
        this.heroes.splice(i, 1);
        this._combatManagement.combatUpdated.next(combat);
      });
  }
  heroSelected(hero: Hero, i: number) {
    if (this.isSaved) {
      if (this.heroes[i].id) {
        const heroToRemove = this.heroes[i];
        this._combatService.removeHero(new AddOrRemoveCreatureToCombatInput<Hero>(this.combat.id, heroToRemove))
          .pipe(
            switchMap((combat) => {
              return this._combatService.addHero(new AddOrRemoveCreatureToCombatInput<Hero>(this.combat.id, hero));
            })
          )
          .subscribe();
      } else {
        this._combatService.addHero(new AddOrRemoveCreatureToCombatInput<Hero>(this.combat.id, hero))
          .subscribe((combat: Combat) => {
            this._combatManagement.combatUpdated.next(combat);
          });
      }
    } else {
      this.heroes[i] = hero;
    }

  }

  monsterSelected(monster: Monster, i: number) {
    if (this.isSaved) {
      this._combatService.addMonster(new AddOrRemoveCreatureToCombatInput<Monster>(this.combat.id, monster))
        .subscribe((combat: Combat) => {
          this.monsters[i] = monster;
          this._combatManagement.combatUpdated.next(combat);
        });
    } else {
      this.monsters[i] = monster;
    }
  }
  addHeroPlace() {
    this.heroes.push(new Monster());
  }
  addMonsterPlace() {
    this.monsters.push(new Monster());
  }

  saveCombat() {
    this._combatService.create(this.combat).subscribe((combat) => this.combat = combat.entity);
  }

  endTurn() {
    this._combatService.endTurn(new EndTurnInput(this.combat.id, this.combat.currentInitiative.creature.id))
      .subscribe((combat: Combat) => {
        this._combatManagement.combatUpdated.next(combat);
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

  removeMonster(i: number) {
    const monster = this.monsters[i];
    this._combatService.removeMonster(new AddOrRemoveCreatureToCombatInput<Monster>(this.combat.id, monster))
      .subscribe(combat => {
        this._combatManagement.combatUpdated.next(combat);
      });
  }
  heroFullAttack(attackInput: AttackInput) {
    this._combatService.fullAttack({ combatId: this.combat.id, attackerId: attackInput.attackerId, targetId: attackInput.targetId}).subscribe((val) => {
      this.attackDetails = val.attackDetails;
      this._combatManagement.combatUpdated.next(val.combat);
    });
  }

}

