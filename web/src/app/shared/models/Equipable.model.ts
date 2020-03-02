import {Bonus} from './Bonus.model';
import {Power} from './Power.model';
import {Item} from './Item.model';

export class EquipableSlot {

}

export class ItemMaterial {
}

export class Equipable extends Item {
  specialName = '';
  slot: EquipableSlot;
  bonuses: Bonus[] = [];
  material: ItemMaterial = new ItemMaterial();
  power: Power = new Power();
}
