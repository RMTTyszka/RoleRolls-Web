import {ItemTemplate} from './ItemTemplate';
import {Power} from '../Power.model';
import {Bonus} from '../Bonus.model';
import {EquipableSlot} from './EquipableSlot';
import {ItemMaterial} from './ItemMaterial';

export class EquipableTemplate extends ItemTemplate {
  public specialName: string;
  public slot: EquipableSlot;
  public bonuses: Bonus[] = [];
  public material: ItemMaterial;
  public power: Power;
}
