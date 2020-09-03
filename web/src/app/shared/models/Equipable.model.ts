import {Bonus} from './Bonus.model';
import {Power} from './Power.model';
import {ItemTemplate} from './Item.model';

export class EquipableSlot {

}

export class ItemMaterial {
}

export class EquipableTemplate extends ItemTemplate {
  specialName = '';
  slot: EquipableSlot;
  bonuses: Bonus[] = [];
  material: ItemMaterial = new ItemMaterial();
  power: Power = new Power();
}
