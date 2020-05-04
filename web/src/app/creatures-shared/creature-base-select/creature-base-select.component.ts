import {Component, EventEmitter, Input, OnDestroy, OnInit, Output} from '@angular/core';
import {EffectType} from '../../shared/models/effects/EffectType.model';
import {Subject} from 'rxjs';
import {DialogService} from 'primeng/api';
import {map, takeUntil} from 'rxjs/operators';
import {UpdateCreatureToolComponent} from '../../masters/master-tools/update-creature-tool/update-creature-tool.component';
import {Creature} from '../../shared/models/creatures/Creature.model';
import {BaseEntityService} from '../../shared/base-entity-service';

@Component({
  selector: 'loh-creature-base-select',
  templateUrl: './creature-base-select.component.html',
  styleUrls: ['./creature-base-select.component.css']
})
export class CreatureBaseSelectComponent<T extends Creature> implements OnInit, OnDestroy {
  @Input() creature: T;
  @Input() private service: BaseEntityService<T>;
  @Output() creatureSelected = new EventEmitter<T>();
  result: T[] = [];
  effectType = EffectType;
  unsubscriber = new Subject<void>();
  @Input() placeholder = 'Creature';
  constructor(
    private dialogService: DialogService,
  ) {
  }

  ngOnInit() {
    this.service.onEntityChange
      .pipe(takeUntil(this.unsubscriber))
      .subscribe((creature: T) => {
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
    this.service.getAllFiltered(event).pipe(
      map(resp => resp.map(creature => creature))
    ).subscribe(response => this.result = response);
  }
  selected(creature: T) {
    this.creature = creature;
    this.creatureSelected.emit(creature);
  }

  getEffect(effectType: EffectType) {
    return this.creature.effects.find(effect => effect.effectType === effectType) || null;
  }

  openMasterTools() {
    this.dialogService.open(UpdateCreatureToolComponent, {
      data: {
        creature: this.creature,
        service: this.service
      },
      header: this.creature.name,
    }).onClose.subscribe((creature: T) => {
      this.creature = creature ? creature : this.creature;
    });
  }
}