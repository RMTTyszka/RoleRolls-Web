import {Component, EventEmitter, Input, OnDestroy, OnInit, Output} from '@angular/core';
import {EffectType} from '../../shared/models/effects/EffectType.model';
import {Subject} from 'rxjs';
import {map, takeUntil} from 'rxjs/operators';
import {UpdateCreatureToolComponent} from '../../masters/master-tools/update-creature-tool/update-creature-tool.component';
import {Creature} from '../../shared/models/creatures/Creature.model';
import {BaseEntityService} from '../../shared/base-entity-service';
import {CombatManagementService} from '../../combat/combat-management.service';
import {Combat} from '../../shared/models/combat/Combat.model';
import {BaseCrudServiceComponent} from '../../shared/base-service/base-crud-service.component';
import {BaseCombatCreatureService} from '../interfaces/baseCombatCreatureService';

@Component({
  selector: 'loh-creature-base-select',
  templateUrl: './creature-base-select.component.html',
  styleUrls: ['./creature-base-select.component.css']
})
export class CreatureBaseSelectComponent implements OnInit, OnDestroy {
  @Input() creature: Creature;
  @Input() combat: Combat;
  @Input() private service: BaseCombatCreatureService<Creature>;
  @Output() creatureSelected = new EventEmitter<Creature>();
  result: Creature[] = [];
  effectType = EffectType;
  unsubscriber = new Subject<void>();
  currentCreatureOnInitiativeId = '';
  @Input() placeholder = 'Creature';
  constructor(
    private _combatManagement: CombatManagementService,
  ) {
  }

  ngOnInit() {
    this._combatManagement.combatUpdated
      .pipe(takeUntil(this.unsubscriber)).subscribe(combat => {
      this.currentCreatureOnInitiativeId = combat.currentInitiative.creature.id;
    });
    this.service.onEntityChange
      .pipe(takeUntil(this.unsubscriber))
      .subscribe((creature: Creature) => {
        if (creature.id === this.creature.id){
          this.creature = creature;
        }
      });
  }
  ngOnDestroy(): void {
    this.unsubscriber.next();
    this.unsubscriber.complete();
  }
  search(event) {
    this.service.getEntitiesForSelect(event).subscribe(response => this.result = response);
  }
  selected(creature: Creature) {
    this.creature = creature;
    this.creatureSelected.emit(creature);
  }

  getEffect(effectType: EffectType) {
    return this.creature ? this.creature.effects.find(effect => effect.effectType === effectType) || null : null;
  }

  get isCurrentOnInitiative() {
    return this.currentCreatureOnInitiativeId === this.creature.id;
  }
}
