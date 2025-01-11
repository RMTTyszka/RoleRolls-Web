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
  name: string
  minorSkills: MinorSkillsTemplateModel[];
}

export interface MinorSkillsTemplateModel extends Entity {
  name: string
  skillTemplateId: string;
}

export interface LifeTemplateModel extends Entity {
  name: string
  formula: string;
}
export interface DefenseTemplateModel extends Entity {
  formula: string;
  name: string;
}
