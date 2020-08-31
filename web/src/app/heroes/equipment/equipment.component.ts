import {Component, Input, OnInit} from '@angular/core';
import {FormGroup} from '@angular/forms';
import {createForm} from '../../shared/EditorExtension';
import {Equipment} from '../../shared/models/Equipment.model';
import {Inventory} from '../../shared/models/Inventory.model';
import {ArmorInstance} from '../../shared/models/ArmorInstance.model';
import {WeaponInstance} from '../../shared/models/WeaponInstance.model';
import {GloveInstance} from '../../shared/models/GloveInstance.model';
import {BeltInstance} from '../../shared/models/BeltInstance.model';
import {HeadpieceInstance} from '../../shared/models/HeadpieceInstance.model';
import {NeckAccessoryInstance} from '../../shared/models/NeckAccessoryInstance.model';
import {RingHand, SelectedRing} from '../../creatures-shared/inventory/inventory-ring-select/inventory-ring-select.component';
import {RingInstance} from '../../shared/models/RingInstance.model';
import {Message, MessageService} from 'primeng/api';
import {HeroFundsService} from '../hero-funds/hero-funds.service';
import {ShopArmor} from '../../shared/models/shop/ShopArmor.model';

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
  get equipment(): FormGroup {
    return this.form.get(this.controlName) as FormGroup;
  }
  constructor(
    private messageService: MessageService,
    private fundsService: HeroFundsService
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
    const selectedArmor = this.findItem(armor.id);
    const removedArmor = this.equipment.get('armor');
    const armorForm = new FormGroup({});
    createForm(armorForm , selectedArmor);
    this.equipment.get('armor').patchValue(armorForm);
    this.inventory.items.splice(this.inventory.items.indexOf(selectedArmor), 1);
  }
  armorBought(armor: ShopArmor, autoEquip: boolean) {
    if (this.inventory.cash1 - armor.value > 0) {
      const removedArmor = this.equipment.get('armor');
      const armorForm = new FormGroup({});
      createForm(armorForm , armor.armor);
      if (autoEquip) {
        this.equipment.get('armor').setValue(armorForm);
      } else {
        this.inventory.items.push(armor.armor);
      }
      this.inventory.cash1 -= armor.value;
    } else {
      this.messageService.add(<Message> {
        summary: 'Insuficiente gold',
        detail: 'You don`t have enough gold'
      });
    }
  }
  mainWeaponSelected(weapon: WeaponInstance) {
    const selectedWeapon = this.findItem(weapon.id);
    const removedWeapon = this.equipment.get('mainWeapon');
    const weaponForm = new FormGroup({});
    createForm(weaponForm , selectedWeapon);
    this.equipment.get('mainWeapon').patchValue(weaponForm);
    this.inventory.items.splice(this.inventory.items.indexOf(selectedWeapon), 1);
  }
  glovesSelected(gloves: GloveInstance) {
    const selectedGloves = this.findItem(gloves.id);
    const removedGloves = this.equipment.get('gloves');
    const glovesForm = new FormGroup({});
    createForm(glovesForm , selectedGloves);
    this.equipment.get('gloves').patchValue(glovesForm);
    this.inventory.items.splice(this.inventory.items.indexOf(selectedGloves), 1);
  }
  beltSelected(belt: BeltInstance) {
    const selectedBelt = this.findItem(belt.id);
    const removedBelt = this.equipment.get('belt');
    const beltForm = new FormGroup({});
    createForm(beltForm , selectedBelt);
    this.equipment.get('belt').patchValue(beltForm);
    this.inventory.items.splice(this.inventory.items.indexOf(selectedBelt), 1);
  }
  headpieceSelected(belt: HeadpieceInstance) {
    const selectedHeadpiece = this.findItem(belt.id);
    const removedHeadpiece = this.equipment.get('headpiece');
    const headpieceForm = new FormGroup({});
    createForm(headpieceForm , selectedHeadpiece);
    this.equipment.get('headpiece').patchValue(headpieceForm);
    this.inventory.items.splice(this.inventory.items.indexOf(selectedHeadpiece), 1);
  }
  neckAccessorySelected(belt: NeckAccessoryInstance) {
    const selectedItem = this.findItem(belt.id);
    const removedItem = this.equipment.get('neckAccessory');
    const formGroup = new FormGroup({});
    createForm(formGroup , selectedItem);
    this.equipment.get('neckAccessory').patchValue(formGroup);
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
    const formGroup = new FormGroup({});
    createForm(formGroup , selectedItem);
    if (selectedRing.isRight) {
      this.equipment.get('ringRight').patchValue(formGroup);
    } else if (selectedRing.isLeft) {
      this.equipment.get('ringLeft').patchValue(formGroup);
    }
    this.inventory.items.splice(this.inventory.items.indexOf(selectedItem), 1);
  }

}
