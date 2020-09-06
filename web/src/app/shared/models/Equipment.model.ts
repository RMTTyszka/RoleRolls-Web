import {Entity} from './Entity.model';
import {ArmorInstance} from './items/ArmorInstance.model';
import {WeaponInstance} from './WeaponInstance.model';
import {GloveInstance} from './GloveInstance.model';
import {BeltInstance} from './BeltInstance.model';
import {HeadpieceInstance} from './HeadpieceInstance.model';
import {NeckAccessoryInstance} from './NeckAccessoryInstance.model';
import {RingInstance} from './RingInstance.model';
import {EquipableInstance} from './EquipableInstance.model';

export class Equipment extends Entity {
  armor: ArmorInstance = null;
  mainWeapon: WeaponInstance = new WeaponInstance();
  offWeapon: WeaponInstance = new WeaponInstance();
  gloves: GloveInstance = new GloveInstance();
  belt: BeltInstance = new BeltInstance();
  headpiece: HeadpieceInstance = new HeadpieceInstance();
  neckAccessory: NeckAccessoryInstance = new NeckAccessoryInstance();
  ringRight: RingInstance = new RingInstance();
  ringLeft: RingInstance = new RingInstance();
}
