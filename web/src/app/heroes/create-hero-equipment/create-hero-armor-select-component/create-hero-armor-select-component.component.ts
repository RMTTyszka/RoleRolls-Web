import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Shop} from '../../../shared/models/shop/Shop.model';
import {HeroCreateShopService} from '../hero-create-shop.service';
import {ShopArmor} from '../../../shared/models/shop/ShopArmor.model';
import {ArmorInstance} from '../../../shared/models/ArmorInstance.model';
import {FormGroup, FormGroupDirective} from '@angular/forms';
import {createForm} from '../../../shared/EditorExtension';

@Component({
  selector: 'loh-create-hero-armor-select-component',
  templateUrl: './create-hero-armor-select-component.component.html',
  styleUrls: ['./create-hero-armor-select-component.component.css']
})
export class CreateHeroArmorSelectComponentComponent implements OnInit {
  placeholder = 'Armor';
  armors: ShopArmor[];
  result: ShopArmor[];
  inventoryForm: FormGroup;
  itemForm: FormGroup;
  equipmentForm: FormGroup;
  @Input() inventoryFormName = 'inventory';
  @Input() equipmentFormName = 'equipment';
  itemFormName = 'armor';
  @Output() armorSelected = new EventEmitter<ShopArmor>();
  constructor(
    private shopService: HeroCreateShopService,
    protected formGroupDirective: FormGroupDirective
  ) {
    this.inventoryForm = this.formGroupDirective.form.get(this.inventoryFormName) as FormGroup;
    this.equipmentForm = this.formGroupDirective.form.get(this.equipmentFormName) as FormGroup;
    this.itemForm = this.equipmentForm.get(this.itemFormName) as FormGroup;
  }

  ngOnInit(): void {
    this.shopService.getShop().subscribe((shop: Shop) => {
      this.armors = shop.armors;
    });
  }
  itemSelected(selectedArmor: ShopArmor) {
    this.armorSelected.next(selectedArmor);
  }
  public get(filter: string) {
    this.result = this.search(filter, this.armors);
  }

  search = (filter: string, items: Array<ShopArmor>) => items
    .filter((item: ShopArmor) => item.armor.name.includes(filter)
      || item.armor.armorModel.baseArmor.name.includes(filter))
}

