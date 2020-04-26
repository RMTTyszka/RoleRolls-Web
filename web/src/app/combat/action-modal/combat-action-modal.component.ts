import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormBuilder, FormGroup} from '@angular/forms';
import {DynamicDialogConfig, DynamicDialogRef, SelectItem} from 'primeng/api';
import {Creature} from '../../shared/models/Creature.model';
import {AttackInput, CombatActionData} from '../combat.component';
import {CombatService} from '../combat.service';

@Component({
  selector: 'loh-combat-action-modal',
  templateUrl: './combat-action-modal.component.html',
  styleUrls: ['./combat-action-modal.component.css']
})
export class CombatActionModalComponent implements OnInit {
  form: FormGroup;
  @Input() combatData: CombatActionData;
  @Output() attackAction = new EventEmitter<AttackInput>()
  target: Creature;
  combatAction: string;
  actions: SelectItem[];
  constructor(
    private readonly fb: FormBuilder,
    private _combatService: CombatService,
  ) {
    this.form = this.fb.group({
      combatAction: ['attack'],
      target: []
    });
    this.actions = this.actionOptions;
  }

  ngOnInit() {
  }

  get action() {
    return this.form.get('combatAction').value;
  }
  get actionOptions() {
    return [
      {
        label: 'attack', value: 'attack'
      },
      {
        label: 'other', value: 'other'
      }
      ] as SelectItem[];
  }

  attack() {
    this.attackAction.emit({attackerId: this.combatData.currentCreatureActing.id, targetId: this.target.id} as AttackInput);
  }

  targetSelected(target: Creature) {
    this.target = target;
  }
}
