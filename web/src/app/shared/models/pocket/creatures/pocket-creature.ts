import { CreatureType } from '../../creatures/CreatureType';
import { PocketEntity } from '../pocket-entity';
import {PocketInventory} from 'src/app/shared/models/pocket/creatures/pocket-inventory';
import {PocketEquipment} from './pocket-equipment';



export class PocketCreature extends PocketEntity {
  public name: string;
  public attributes: PocketAttribute[] = [];
  public skills: PocketSkill[] = [];
  public lifes: PocketLife[] = [];
  public defenses: PocketDefense[] = [];
  public creatureType: CreatureType;
  public inventory: PocketInventory = new PocketInventory();
  public equipment: PocketEquipment = new PocketEquipment();
  public ownerId: string;
  public level: number;
}

export class PocketAttribute extends PocketEntity {
  public name: string;
  public value: number;
  public attributeTemplateId: string;
}

export class PocketSkill extends PocketEntity {
  public name: string;
  public attributeId: string;
  public skillTemplateId: string;
  public value: number;
  public minorSkills: PocketMinorSkill[] = [];
  public pointsLimit: number;
  public usedPoints: number;

}
export class PocketMinorSkill extends PocketEntity {
  public name: string;
  public skillId: string;
  public minorSkillTemplateId: string;
  public points: number;
}
export class PocketLife extends PocketEntity {
  public name: string;
  public value: number;
  public maxValue: number;
}
export class PocketDefense extends PocketEntity {
  public name: string;
  public value: number;
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
