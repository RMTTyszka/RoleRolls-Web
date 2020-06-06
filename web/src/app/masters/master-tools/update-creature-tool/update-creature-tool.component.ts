import {Component, OnDestroy, OnInit} from '@angular/core';
import {UpdateCreatureToolService} from './update-creature-tool.service';
import {Creature} from '../../../shared/models/creatures/Creature.model';
import {MenuItem} from 'primeng/api';
import {FormBuilder, FormGroup} from '@angular/forms';
import {BaseEntityService} from '../../../shared/base-entity-service';
import {TieredMenu} from 'primeng/primeng';
import {EffectInstance} from '../../../shared/models/effects/EffectInstance.model';
import {Subject} from 'rxjs';
import {takeUntil} from 'rxjs/operators';
import {TakeDamageInput} from '../../../shared/models/inputs/TakeDamageInput';
import {Combat} from '../../../shared/models/combat/Combat.model';
import {DynamicDialogConfig, DynamicDialogRef} from 'primeng/dynamicdialog';
import {MasterToolAction} from '../MasterToolAction';

@Component({
  selector: 'loh-update-creature-tool',
  templateUrl: './update-creature-tool.component.html',
  styleUrls: ['./update-creature-tool.component.css']
})
export class UpdateCreatureToolComponent<T extends Creature> implements OnInit, OnDestroy {

  creature: T;
  form: FormGroup;
  combat: Combat;
  action: MasterToolAction;
  MasterToolAction =  MasterToolAction;
  entityService: BaseEntityService<T>;
  effectOptions: MenuItem[] = [];
  public currentEffect: EffectInstance;
  public unsubscriber = new Subject<void>();
  constructor(
    private readonly updateCreatureToolService: UpdateCreatureToolService,
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    public fb: FormBuilder,
  ) {
    this.creature = config.data.creature;
    this.combat = config.data.combat;
    this.action = config.data.action;
    this.entityService = config.data.service;
    this.form = this.fb.group({
      life: [this.creature.currentLife],
      moral: [this.creature.currentMoral],
      healing: [0],
      isTakeDamage: [false],
      physicalDamages: [[]],
      arcaneDamages: [[]],
      fireDamages: [[]],
      iceDamages: [[]],
      lightningDamages: [[]],
      poisonDamages: [[]],
      holyDamages: [[]],
      necroticDamages: [[]],
      sonicDamages: [[]],
    });
  }
  ngOnDestroy(): void {
    this.unsubscriber.next();
    this.unsubscriber.complete();
  }
  ngOnInit() {
    this.entityService.onEntityChange
      .pipe(
        takeUntil(this.unsubscriber)
      ).subscribe(entity => {
      this.creature = entity;
    })
    this.effectOptions = [
      {
        label: 'Remove',
        command: () => this.removeEffect()
      },
      {
        label: '+ Level',
        items: [
          {
            label: '+1',
            command: () => this.updateEffect(1)
          },          {
            label: '+2',
            command: () => this.updateEffect(2)
          },          {
            label: '+3',
            command: () => this.updateEffect(3)
          },          {
            label: '+4',
            command: () => this.updateEffect(4)
          },
        ]
      },
      {
        label: '- Level',
        items: [
          {
            label: '-1',
            command: () => this.updateEffect(-1)
          },          {
            label: '-2',
            command: () => this.updateEffect(-2)
          },          {
            label: '-3',
            command: () => this.updateEffect(-3)
          },          {
            label: '-4',
            command: () => this.updateEffect(-4)
          },
        ]
      },
    ];
  }

  removeEffect() {
    this.updateCreatureToolService.removeEffect({creatureId: this.creature.id, effect: this.currentEffect })
      .subscribe((creature: Creature) => {
        this.entityService.onEntityChange.next(creature as T);
      });
  }
  updateLife() {
    this.updateCreatureToolService.updateLife({creatureId: this.creature.id, value: this.life})
      .subscribe((updatedCreature: Creature) => {
        this.form.get('life').setValue(updatedCreature.currentLife);
        this.entityService.onEntityChange.next(updatedCreature as T);
      });
  }
  updateMoral() {
    this.updateCreatureToolService.updateMoral({creatureId: this.creature.id, value: this.moral})
      .subscribe((updatedCreature: Creature) => {
        this.form.get('moral').setValue(updatedCreature.currentMoral);
        this.entityService.onEntityChange.next(updatedCreature as T);
      });
  }
  heal() {
    this.updateCreatureToolService.heal({creatureId: this.creature.id, value: this.healing})
      .subscribe((updatedCreature: Creature) => {
        this.form.get('life').setValue(updatedCreature.currentLife);
        this.form.get('moral').setValue(updatedCreature.currentMoral);
        this.entityService.onEntityChange.next(updatedCreature as T);
      });
  }
get life() {
    return this.form.get('life').value;
}
get moral() {
    return this.form.get('moral').value;
}
get healing() {
    return this.form.get('healing').value;
}

  test() {
    console.log('dsadsa');
  }

  effectClicked(event: any, effectMenu: TieredMenu, effect: EffectInstance) {
    effectMenu.show(event);
    this.currentEffect = effect;
  }

  private updateEffect(level: number) {
    this.currentEffect.level = level;
    this.updateCreatureToolService.updateEffect({creatureId: this.creature.id, effect: this.currentEffect})
      .subscribe((creature: T) => {
        this.entityService.onEntityChange.next(creature);
      });
  }

  updateFullLife() {
    this.updateCreatureToolService.updateLife({creatureId: this.creature.id, value: 999999})
      .subscribe((updatedCreature: Creature) => {
        this.form.get('life').setValue(updatedCreature.currentLife);
        this.entityService.onEntityChange.next(updatedCreature as T);
      });
  }
  updateFullMoral() {
    this.updateCreatureToolService.updateMoral({creatureId: this.creature.id, value: 999999})
      .subscribe((updatedCreature: Creature) => {
        this.form.get('moral').setValue(updatedCreature.currentMoral);
        this.entityService.onEntityChange.next(updatedCreature as T);
      });
  }
  fullyHeal() {
    this.updateCreatureToolService.heal({creatureId: this.creature.id, value: 999999})
      .subscribe((updatedCreature: Creature) => {
        this.form.get('life').setValue(updatedCreature.currentLife);
        this.form.get('moral').setValue(updatedCreature.currentMoral);
        this.entityService.onEntityChange.next(updatedCreature as T);
      });
  }

  takeDamage() {
    const input = this.form.getRawValue() as TakeDamageInput;
    input.creatureId = this.creature.id;
    input.combatId = this.combat.id;
    this.updateCreatureToolService.takeDamage(input)
      .subscribe((updatedCreature: Creature) => {
        this.form.get('life').setValue(updatedCreature.currentLife);
        this.form.get('moral').setValue(updatedCreature.currentMoral);
        this.entityService.onEntityChange.next(updatedCreature as T);
      });
  }
  get isTakeDamage() {
    return this.form.get('isTakeDamage').value;
  }

}
