import {Component, Input} from '@angular/core';
import {EquipableSlot} from '@app/models/itens/equipable-slot';
import {UntypedFormGroup} from '@angular/forms';
import {
  CreatureEquipmentSlotComponent
} from '@app/creatures/creature-editor/creature-equipment/creature-equipment-slot/creature-equipment-slot.component';
import {NgForOf} from '@angular/common';

@Component({
  selector: 'rr-creature-equipment',
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
