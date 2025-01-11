import {PocketEquipment} from './pocket-equipment';
import { Entity } from '../../models/Entity.model';
import { PocketInventory } from './pocket-inventory';
import { CreatureType } from './CreatureType';



export class PocketCreature extends Entity {
  public name!: string;
  public attributes: PocketAttribute[] = [];
  public skills: PocketSkill[] = [];
  public lifes: PocketLife[] = [];
  public defenses: PocketDefense[] = [];
  public creatureType!: CreatureType;
  public inventory: PocketInventory = new PocketInventory();
  public equipment: PocketEquipment = new PocketEquipment();
  public ownerId!: string;
  public level!: number;
}

export class PocketAttribute extends Entity {
  public name!: string;
  public value!: number;
  public attributeTemplateId!: string;
}

export class PocketSkill extends Entity {
  public name!: string;
  public attributeId!: string;
  public skillTemplateId!: string;
  public value!: number;
  public minorSkills: PocketMinorSkill[] = [];
  public pointsLimit!: number;
  public usedPoints!: number;

}
export class PocketMinorSkill extends Entity {
  public name!: string;
  public skillId!: string;
  public minorSkillTemplateId!: string;
  public points!: number;
}
export class PocketLife extends Entity {
  public name!: string;
  public value!: number;
  public maxValue!: number;
}
export class PocketDefense extends Entity {
  public name!: string;
  public value!: number;
}
export enum PocketSkillProficience {
  Expert = 0, // +4
  Good = 1, // +2
  Normal = 3, // 0
  Bad = 4, // + -1
  Crap = 5 // -3
}


export class PocketHero extends PocketCreature {
}

export class PocketMonster extends PocketCreature {
}
