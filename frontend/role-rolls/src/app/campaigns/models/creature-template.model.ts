import { Entity } from '../../models/Entity.model';

export class CreatureTemplateModel extends Entity {
  public maxAttributePoints: number = 0;
  public totalAttributePoints: number = 0;
  public totalSkillsPoints: number = 0;
  public attributes: AttributeTemplateModel[] = [];
  public skills: SkillTemplateModel[] = [];
  public lifes: LifeTemplateModel[] = [];
  public defenses: DefenseTemplateModel[] = [];
  public default: boolean = false;
}


export interface AttributeTemplateModel extends Entity {
  name: string
}

export interface SkillTemplateModel extends Entity {
  attributeId: string;
  minorSkills: MinorSkillsTemplateModel[];
}

export class MinorSkillsTemplateModel extends Entity {
  public skillTemplateId!: string;
}

export class LifeTemplateModel extends Entity {
  public formula!: string;
}
export interface DefenseTemplateModel extends Entity {
  formula: string;
  name: string;
}
