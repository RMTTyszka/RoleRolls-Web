import {Component, Input} from '@angular/core';
import {UntypedFormGroup} from '@angular/forms';
import {CreatureEquipmentSlotComponent} from './creature-equipment-slot/creature-equipment-slot.component';
import {EquipableSlot} from '../../../shared/models/pocket/itens/equipable-slot';
import {NgForOf} from '@angular/common';

@Component({
  selector: 'rr-creature-equipment',
  standalone: true,
  imports: [
    CreatureEquipmentSlotComponent,
    NgForOf
  ],
  templateUrl: './creature-equipment.component.html',
  styleUrl: './creature-equipment.component.scss'
})
export class CreatureEquipmentComponent {
  @Input() public form: UntypedFormGroup;
  public get slots() {
    return Object.keys(EquipableSlot).filter(key => isNaN(Number(key)));
  }
}
