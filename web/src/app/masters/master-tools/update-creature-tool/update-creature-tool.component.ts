import {Component, OnDestroy, OnInit} from '@angular/core';
import {UpdateCreatureToolService} from './update-creature-tool.service';
import {Creature} from '../../../shared/models/creatures/Creature.model';
import {DynamicDialogConfig, DynamicDialogRef, MenuItem} from 'primeng/api';
import {FormBuilder, FormGroup} from '@angular/forms';
import {BaseEntityService} from '../../../shared/base-entity-service';
import {TieredMenu} from 'primeng/primeng';
import {EffectInstance} from '../../../shared/models/effects/EffectInstance.model';
import {Subject} from 'rxjs';
import {takeUntil} from 'rxjs/operators';

@Component({
  selector: 'loh-update-creature-tool',
  templateUrl: './update-creature-tool.component.html',
  styleUrls: ['./update-creature-tool.component.css']
})
export class UpdateCreatureToolComponent<T extends Creature> implements OnInit, OnDestroy {

  creature: T;
  form: FormGroup;
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
    this.entityService = config.data.service;
    this.form = this.fb.group({
      life: [this.creature.currentLife]
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
    this.updateCreatureToolService.updateLife({creatureId: this.creature.id, life: this.life})
      .subscribe((updatedCreature: Creature) => {
        this.form.get('life').setValue(updatedCreature.currentLife);
        this.entityService.onEntityChange.next(updatedCreature as T);
      });
  }
get life() {
    return this.form.get('life').value;
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
    this.updateCreatureToolService.updateLife({creatureId: this.creature.id, life: 999999})
      .subscribe((updatedCreature: Creature) => {
        this.form.get('life').setValue(updatedCreature.currentLife);
        this.entityService.onEntityChange.next(updatedCreature as T);
      });
  }
}
