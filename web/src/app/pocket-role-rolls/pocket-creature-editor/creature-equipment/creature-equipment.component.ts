import {Component, Input} from '@angular/core';
import {UntypedFormGroup} from '@angular/forms';

@Component({
  selector: 'rr-creature-equipment',
  standalone: true,
  imports: [],
  templateUrl: './creature-equipment.component.html',
  styleUrl: './creature-equipment.component.scss'
})
export class CreatureEquipmentComponent {
  @Input() public form: UntypedFormGroup;
}
