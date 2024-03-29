import {Component, Input, OnDestroy, OnInit} from '@angular/core';
import {CombatService} from './combat.service';
import {Hero} from '../shared/models/NewHero.model';
import {AttackDetails} from '../shared/models/AttackDetails.model';
import {Combat} from '../shared/models/combat/Combat.model';
import {Monster} from '../shared/models/creatures/monsters/Monster.model';
import {Creature} from '../shared/models/creatures/Creature.model';
import {ActivatedRoute} from '@angular/router';
import {AddOrRemoveCreatureToCombatInput} from '../shared/models/combat/AddOrRemoveCreatureToCombatInput';
import {EndTurnInput} from '../shared/models/combat/EndTurnInput';
import {MenuItem} from 'primeng/api';
import {CreatureType} from '../shared/models/creatures/CreatureType';
import {EffectType} from '../shared/models/effects/EffectType.model';
import {interval, Subject} from 'rxjs';
import {switchMap, takeUntil} from 'rxjs/operators';
import {CombatManagementService} from './combat-management.service';
import {Menu} from 'primeng/menu';
import {UpdateCreatureToolComponent} from '../masters/master-tools/update-creature-tool/update-creature-tool.component';
import {HeroesService} from '../heroes/heroes.service';
import {MonsterService} from '../monsters/monster/monster.service';
import {MasterToolAction} from '../masters/master-tools/MasterToolAction';
import {DialogService} from 'primeng/dynamicdialog';
import {isNotNullOrUndefined} from 'codelyzer/util/isNotNullOrUndefined';

export class CombatActionData {
  currentTargets: Creature[] = [];
  currentCreatureActing: Creature;
}
export class AttackInput {
  attackerId: string;
  targetId: string;
}

@Component({
  selector: 'rr-combat',
  templateUrl: './combat.component.html',
  styleUrls: ['./combat.component.css'],
  providers: [DialogService]
})
export class CombatComponent implements OnInit, OnDestroy {
  combat: Combat
  isMaster = true;
  attackDetails: AttackDetails;
  hasLoaded = false;
  actionModalOpened = false;
  creatureType = CreatureType;
  combatActionData: CombatActionData = new CombatActionData();
  effectType = EffectType;
  usubscriber = new Subject<void>();
  creatureActions: MenuItem[] = [];
  selectedCreature: Creature;
  selectedCreatureType: CreatureType;
  constructor(
    private _combatService: CombatService,
    private heroesService: HeroesService,
    private monsterService: MonsterService,
    private _combatManagement: CombatManagementService,
    private dialog: DialogService,
    private routeSnapshot: ActivatedRoute
  ) { }
  ngOnDestroy(): void {
    this.usubscriber.next();
    this.usubscriber.complete();
  }
  ngOnInit() {
    this.creatureActions = [
      {
        label: 'Act',
        icon: 'fas fa-street-view',
        command: () => {
          this.openActionBar(this.selectedCreature, this.selectedCreatureType);
        }
      },
      {
        label: 'Remove',
        icon: 'fas fa-times',
        command: () => {
          this.removeCreature(this.selectedCreature, this.selectedCreatureType);
        }
      },
    ];
    if (this.isMaster) {
      this.creatureActions.push({
        label: 'Master Tools',
        icon: 'fas fa-user-secret',
        items: [
          {
            label: 'Status',
            icon: 'fas fa-diagnoses',
            command: () => {
              this.openMasterTools(MasterToolAction.Status);
            },
          },
          {
            label: 'Take Damage',
            icon: 'fas fa-gavel',
            command: () => {
              this.openMasterTools(MasterToolAction.TakeDamage);
            },
          },
          {
            label: 'Bonus',
            icon: 'fas fa-rainbow',
            command: () => {
              this.openMasterTools(MasterToolAction.Bonus);
            },
          },
          {
            label: 'Inventory',
            icon: 'fas fa-hammer',
            command: () => {
              this.openMasterTools(MasterToolAction.Inventory);
            },
          },
        ]
      });
    }
    this._combatManagement.combatUpdated
      .pipe(takeUntil(this.usubscriber)).subscribe(combat => {
      this.combat = combat;
      this.hasLoaded = true;
    });
    interval(300000000)
      .pipe(takeUntil(this.usubscriber)).subscribe(() => {
      if (this.combat.id) {
        this._combatService.get(this.combat.id)
          .subscribe(combat => {
              if (this.combat.lastUpdateTime < combat.lastUpdateTime) {
                this._combatManagement.combatUpdated.next(combat);
              }
            }
          );
      }
    });
  }

  get isSaved(): boolean {
    return  isNotNullOrUndefined(this.combat.id);
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
    if (hero.id === undefined) {
      this.heroes.splice(i, 1);
      return;
    }
    this._combatService.removeHero(this.combat.id, hero.id)
      .subscribe(combat => {
        this._combatManagement.combatUpdated.next(combat);
      });
  }
  heroSelected(hero: Hero, i: number) {
    if (this.isSaved) {
      if (this.heroes[i].id) {
        const heroToRemove = this.heroes[i];
        this._combatService.removeHero(this.combat.id, heroToRemove.id)
          .pipe(
            switchMap((combat) => {
              return this._combatService.addHero(this.combat.id, new AddOrRemoveCreatureToCombatInput<Hero>(hero));
            })
          )
          .subscribe();
      } else {
        this._combatService.addHero(this.combat.id, new AddOrRemoveCreatureToCombatInput<Hero>(hero))
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
      this._combatService.addMonster(this.combat.id, new AddOrRemoveCreatureToCombatInput<Monster>(monster))
        .subscribe((combat: Combat) => {
          this.monsters[i] = monster;
          this._combatManagement.combatUpdated.next(combat);
        });
    } else {
      this.monsters[i] = monster;
    }
  }
  addHeroPlace() {
    this.heroes.push(new Hero());
  }
  addMonsterPlace() {
    this.monsters.push(new Monster());
  }

  saveCombat() {
    if (this.isSaved) {
      this._combatService.update(this.combat).subscribe((combat) => this.combat = combat.entity);
    } else {
      this._combatService.create(this.combat).subscribe((combat) => this.combat = combat.entity);
    }
  }

  endTurn() {
    this._combatService.endTurn(this.combat.id, new EndTurnInput(this.combat.currentInitiative.creature.id))
      .subscribe((combat: Combat) => {
        this._combatManagement.combatUpdated.next(combat);
      });
  }
  openMasterTools(action: MasterToolAction) {
    this.dialog.open(UpdateCreatureToolComponent, {
      data: {
        creature: this.selectedCreature,
        service: this.selectedCreatureType === CreatureType.Hero ? this.heroesService : this.monsterService,
        combat: this.combat,
        action: action
      },
      header: this.selectedCreature.name,
      width: '40vw'
    }).onClose.subscribe((creature: Creature) => {
      this.selectedCreature = creature ? creature : this.selectedCreature;
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
    const monster = this.monsters[i] as Monster;
    if (monster.id === undefined) {
      this.monsters.splice(i, 1);
      return;
    }
    this._combatService.removeMonster(this.combat.id, monster.id)
      .subscribe(combat => {
        this._combatManagement.combatUpdated.next(combat);
      });
  }
  heroFullAttack(attackInput: AttackInput) {
    this.actionModalOpened = false;
    this._combatService.fullAttack(this.combat.id, {attackerId: attackInput.attackerId, targetId: attackInput.targetId})
      .subscribe((val) => {
        this.attackDetails = val.attackDetails;
        this._combatManagement.combatUpdated.next(val.combat);
      });
  }

  isCurrentOnInitiative(id: string) {
    return this.combat.currentInitiative ? id === this.combat.currentInitiative.creature.id : false;
  }

  setSelectedCreature(creature: Creature, creatureType: CreatureType, actionMenu: Menu, $event: MouseEvent) {
    this.selectedCreature = creature;
    this.selectedCreatureType = creatureType;
    actionMenu.toggle($event);
  }

  private removeCreature(selectedCreature: Creature, selectedCreatureType: CreatureType) {
    const observable = selectedCreatureType === CreatureType.Hero ?
      this._combatService.removeHero(this.combat.id, selectedCreature.id) :
      this._combatService.removeMonster(this.combat.id, selectedCreature.id);
    observable.subscribe((combat) => this._combatManagement.combatUpdated.next(combat));
  }

  deleteCombat() {

  }

  public closeCombat(): void {
    this._combatManagement.combatUpdated.next(null);
  }
}

