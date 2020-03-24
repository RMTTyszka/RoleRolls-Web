import {Entity} from './Entity.model';
import {Race} from './Race.model';
import {Role} from './Role.model';
import {Equipment} from './Equipment.model';
import {Inventory} from './Inventory.model';
import {WeaponAttributes} from './WeaponAttributes.model';
import {Resistances} from './Resistances.model';

export class BaseAttributes {
    strength: number;
    agility: number;
    vitality: number;
    wisdom: number;
    intuition: number;
    charisma: number;
  }

  export class InitialAttributes {
    strength?: any;
    agility?: any;
    vitality?: any;
    wisdom?: any;
    intuition?: any;
    charisma?: any;
  }

  export class BonusAttributes {
    strength?: any;
    agility?: any;
    vitality?: any;
    wisdom?: any;
    intuition?: any;
    charisma?: any;
  }

  export class NewHero extends Entity {
    id: string;
    baseAttributes: BaseAttributes;
    initialAttributes: InitialAttributes;
    bonusAttributes: BonusAttributes;
    totalInitialPoint: number;
    maxInitialAttributePoints: number;
    maxAttributeBonusPoints: number;
    totalAttributesBonusPoints: number;
    level: number;
    race: Race;
    role: Role;
    equipment: Equipment;
    inventory: Inventory;
    mana: number;
    defense: number;
    evasion: number;
    specialAttack: number;
    magicDefense: number;
    resistances = new Resistances();
    mainWeaponAttributes = new WeaponAttributes();
    offWeaponAttributes = new WeaponAttributes();
  }

