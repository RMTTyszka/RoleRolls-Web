import {Component, Input, OnInit} from '@angular/core';
import {FormArray, FormBuilder, FormGroup, FormGroupDirective} from '@angular/forms';
import {Inventory} from '../../../shared/models/Inventory.model';
import {HeroManagementService} from '../../../heroes/hero-management.service';
import {createForm} from '../../../shared/EditorExtension';
import {ItemInstance} from '../../../shared/models/ItemInstance.model';
import {EquipableInstance} from '../../../shared/models/EquipableInstance.model';
import {CreatureManagementService} from '../../interfaces/creature-management-service';

@Component({
  selector: 'loh-inventory',
  templateUrl: './inventory.component.html',
  styleUrls: ['./inventory.component.css']
})
export class InventoryComponent implements OnInit {

  @Input() formGroupName = 'inventory'
  @Input() private creatureManagementService: CreatureManagementService

  form: FormGroup;
  constructor(
    private formGroupDirective: FormGroupDirective,
  ) {
  }

  ngOnInit() {
    this.creatureManagementService.addItemToInventory.subscribe(item => {
      const itemForm = new FormGroup({});
      createForm(itemForm, item);
      (this.form.get('items') as FormArray).push(itemForm);
    });
    this.form = this.formGroupDirective.form.get(this.formGroupName) as FormGroup;
  }
  get inventory(): Inventory {
    return this.form.value;
  }

  showItem(item: ItemInstance) {
    return item.equipable && (item as EquipableInstance).removable || !item.equipable;
  }
}
