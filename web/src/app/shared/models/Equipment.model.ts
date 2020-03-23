import {Entity} from './Entity.model';
import {ArmorInstance} from './ArmorInstance.model';
import { WeaponInstance } from './WeaponInstance.model';
import {GloveInstance} from './GloveInstance.model';
import {BeltInstance} from './BeltInstance.model';

export class Equipment extends Entity {
  armor: ArmorInstance = null;
  mainWeapon: WeaponInstance = new WeaponInstance();
  offWeapon: WeaponInstance = new WeaponInstance();
  gloves: GloveInstance = new GloveInstance();
  belt: BeltInstance = new BeltInstance();
}
