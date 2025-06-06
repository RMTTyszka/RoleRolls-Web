import {Equipment} from 'app/campaigns/models/equipment';
import { Entity } from '../../models/Entity.model';
import { Inventory } from 'app/campaigns/models/inventory';
import { CreatureCategory } from 'app/campaigns/models/CreatureCategory';



export class Creature implements Entity {
  public id: string;
  public name!: string;
  public attributes: Attribute[] = [];
  public skills: Skill[] = [];
  public attributelessSkills: Skill[] = [];
  public vitalities: Vitality[] = [];
  public defenses: Defense[] = [];
  public category!: CreatureCategory;
  public inventory: Inventory = new Inventory();
  public equipment: Equipment = new Equipment();
  public ownerId!: string;
  public level!: number;
  public isTemplate!: boolean;
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
  public attributeId?: string | null;
  public skillTemplateId!: string;
  public value!: number;
  public specificSkills: SpecificSkill[] = [];
  public pointsLimit!: number;
  public usedPoints!: number;

}
export class SpecificSkill implements Entity {
  public id: string;
  public name!: string;
  public skillId!: string;
  public specificSkillTemplateId!: string;
  public points!: number;
}
export class Vitality implements Entity {
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
