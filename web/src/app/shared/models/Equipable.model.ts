import {Bonus} from './Bonus.model';
import {Power} from './Power.model';
import {Item} from './Item.model';

export class EquipableSlot {

}

export class ItemMaterial {
}

export class Equipable extends Item {
  specialName: string;
  slot: EquipableSlot;
  bonuses: Bonus[];
  material: ItemMaterial;
  power: Power;
}
