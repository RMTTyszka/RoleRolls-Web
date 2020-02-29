import {Entity} from './Entity.model';

export class ArmorCategory extends Entity {
  armorType: ArmorType = ArmorType.Light;
  defense = 0;
  evasion = 0;
  basedefense = 0;
}


export enum ArmorType {
  Light,
  Medium ,
  Heavy
}
