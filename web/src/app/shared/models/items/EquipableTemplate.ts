import {ItemTemplate} from './ItemTemplate';
import {Power} from '../Power.model';
import {Bonus} from '../Bonus.model';
import {EquipableSlotOld} from './EquipableSlotOld';
import {ItemMaterial} from './ItemMaterial';

export class EquipableTemplate extends ItemTemplate {
  public specialName: string;
  public slot: EquipableSlotOld;
  public bonuses: Bonus[] = [];
  public material: ItemMaterial;
  public power: Power;
}
