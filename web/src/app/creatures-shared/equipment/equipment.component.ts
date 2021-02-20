import {Component, Input, OnInit} from '@angular/core';
import {FormArray, FormControl, FormGroup} from '@angular/forms';
import {createForm} from '../../shared/EditorExtension';
import {Equipment} from '../../shared/models/Equipment.model';
import {Inventory} from '../../shared/models/Inventory.model';
import {ArmorInstance} from '../../shared/models/items/ArmorInstance.model';
import {WeaponInstance} from '../../shared/models/WeaponInstance.model';
import {GloveInstance} from '../../shared/models/GloveInstance.model';
import {BeltInstance} from '../../shared/models/BeltInstance.model';
import {HeadpieceInstance} from '../../shared/models/HeadpieceInstance.model';
import {NeckAccessoryInstance} from '../../shared/models/NeckAccessoryInstance.model';
import {RingHand, SelectedRing} from '../inventory/inventory-ring-select/inventory-ring-select.component';
import {RingInstance} from '../../shared/models/RingInstance.model';
import {CreatureEquipmentService} from './creature-equipment.service';
import {Creature} from '../../shared/models/creatures/Creature.model';
import {CreatureEditorService} from '../creature-editor/creature-editor.service';
import {createMetadataReaderCache} from '@angular/compiler-cli/src/transformers/metadata_reader';

@Component({
  selector: 'loh-equipment',
  templateUrl: './equipment.component.html',
  styleUrls: ['./equipment.component.css']
})
export class EquipmentComponent implements OnInit {

  @Input() form: FormGroup = this.createForm();
  @Input() controlName = 'equipment';
  @Input() inventoryControlName = 'inventory';
  @Input() isCreating = false;
  ringHand = RingHand;
  entity: Equipment;
  get inventory(): Inventory {
    return this.form.get(this.inventoryControlName).value;
  }
  set inventory(value) {
    const items = (this.form.get(this.inventoryControlName).get('items') as FormArray);
    items.clear();
    value.items.forEach(i => {
      const newForm = new FormGroup({});
      createForm(newForm, i);
      items.push(newForm);
    });
    value.items = [];
    this.form.get(this.inventoryControlName).patchValue(value);
  }
  get equipment(): FormGroup {
    return this.form.get(this.controlName) as FormGroup;
  }
  constructor(
    private creatureEquipmentService: CreatureEquipmentService,
    private creatureEditorService: CreatureEditorService
  ) { }

  ngOnInit() {
    this.entity = this.getEntity();
  }

  createForm() {
    const form = new FormGroup({});
    const entity = new Equipment();
    createForm(form, entity);
    return form;
  }

  getEntity() {
    const entity = this.form.get(this.controlName).value;
    return entity ? entity : new Equipment();
  }


  findItem(id: string) {
    return this.inventory.items.find(r => r.id === id);
  }

  armorSelected(armor: ArmorInstance) {
    this.creatureEquipmentService.equipArmor(this.form.get('id').value, armor.id).subscribe((creature: Creature) => {
      this.inventory = creature.inventory;
      this.equipment.patchValue(creature.equipment);
      this.creatureEditorService.publishCreatureUpdated(creature);
    });
  }

  mainWeaponSelected(weapon: WeaponInstance) {
    this.creatureEquipmentService.equipMainWeapon(this.form.get('id').value, weapon.id).subscribe((creature: Creature) => {
      // this.equipment.get('mainWeapon').patchValue(weapon);
      this.inventory = creature.inventory;
      this.equipment.patchValue(creature.equipment);
      this.creatureEditorService.publishCreatureUpdated(creature);
    });
  }
  offWeaponSelected(weapon: WeaponInstance) {
    this.creatureEquipmentService.equipOffWeapon(this.form.get('id').value, weapon.id).subscribe((creature: Creature) => {
      // this.equipment.get('mainWeapon').patchValue(weapon);
      this.inventory = creature.inventory;
      this.equipment.patchValue(creature.equipment);
      this.creatureEditorService.publishCreatureUpdated(creature);
    });
  }
  glovesSelected(gloves: GloveInstance) {
    const selectedGloves = this.findItem(gloves.id);
    this.equipment.get('gloves').patchValue(selectedGloves);
    this.inventory.items.splice(this.inventory.items.indexOf(selectedGloves), 1);
  }
  beltSelected(belt: BeltInstance) {
    const selectedBelt = this.findItem(belt.id);
    this.equipment.get('belt').patchValue(selectedBelt);
    this.inventory.items.splice(this.inventory.items.indexOf(selectedBelt), 1);
  }
  headpieceSelected(belt: HeadpieceInstance) {
    const selectedHeadpiece = this.findItem(belt.id);
    this.equipment.get('headpiece').patchValue(selectedHeadpiece);
    this.inventory.items.splice(this.inventory.items.indexOf(selectedHeadpiece), 1);
  }
  neckAccessorySelected(belt: NeckAccessoryInstance) {
    const selectedItem = this.findItem(belt.id);
    this.equipment.get('neckAccessory').patchValue(selectedItem);
    this.inventory.items.splice(this.inventory.items.indexOf(selectedItem), 1);
  }
  ringSelected(selectedRing: SelectedRing) {
    const selectedItem = this.findItem(selectedRing.ring.id);
    let removedItem: RingInstance;
    if (selectedRing.isRight) {
      removedItem = this.equipment.get('ringRight').value;
    } else if (selectedRing.isLeft) {
      removedItem = this.equipment.get('ringLeft').value;
    }
    if (selectedRing.isRight) {
      this.equipment.get('ringRight').patchValue(selectedItem);
    } else if (selectedRing.isLeft) {
      this.equipment.get('ringLeft').patchValue(selectedItem);
    }
    this.inventory.items.splice(this.inventory.items.indexOf(selectedItem), 1);
  }

}
