import {Attributes} from '../Attributes.model';
import {Race} from '../Race.model';
import {Role} from '../Role.model';
import {Equipment} from '../Equipment.model';
import {Inventory} from '../Inventory.model';
import {CreatureStatus} from './CreatureStatus.model';
import {Resistances} from '../Resistances.model';
import {WeaponAttributes} from '../WeaponAttributes.model';
import {Entity} from '../Entity.model';
import {EffectInstance} from '../effects/EffectInstance.model';

export class Creature extends Entity {
  baseAttributes: Attributes = new Attributes();
  bonusAttributes: Attributes = new Attributes();
  totalAttributes: Attributes = new Attributes();
  totalInitialPoint: number;
  maxInitialAttributePoints: number;
  maxAttributeBonusPoints: number;
  totalAttributesBonusPoints: number;
  level: number;
  race: Race = new Race();
  role: Role = new Role();
  equipment: Equipment;
  inventory: Inventory;
  status: CreatureStatus = new CreatureStatus();
  resistances = new Resistances();
  effects = new Array<EffectInstance>();
  mainWeaponAttributes = new WeaponAttributes();
  offWeaponAttributes = new WeaponAttributes();
  currentLife = 0;
  currentMoral = 0;
}