import {Equipment} from 'app/campaigns/models/equipment';
import { Entity } from '../../models/Entity.model';
import { Inventory } from 'app/campaigns/models/inventory';
import { CreatureType } from './CreatureType';



export class Creature implements Entity {
  public id: string;
  public name!: string;
  public attributes: Attribute[] = [];
  public skills: Skill[] = [];
  public lifes: Life[] = [];
  public defenses: Defense[] = [];
  public creatureType!: CreatureType;
  public inventory: Inventory = new Inventory();
  public equipment: Equipment = new Equipment();
  public ownerId!: string;
  public level!: number;
}

export class Attribute implements Entity {
  public id: string;
  public name!: string;
  public value!: number;
  public attributeTemplateId!: string;
}

export class Skill implements Entity {
  public id: string;
  public name!: string;
  public attributeId!: string;
  public skillTemplateId!: string;
  public value!: number;
  public minorSkills: MinorSkill[] = [];
  public pointsLimit!: number;
  public usedPoints!: number;

}
export class MinorSkill implements Entity {
  public id: string;
  public name!: string;
  public skillId!: string;
  public minorSkillTemplateId!: string;
  public points!: number;
}
export class Life implements Entity {
  public id: string;
  public name!: string;
  public value!: number;
  public maxValue!: number;
}
export class Defense implements Entity {
  public id: string;
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
