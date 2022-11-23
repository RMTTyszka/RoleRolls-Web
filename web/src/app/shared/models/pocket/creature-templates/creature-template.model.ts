import { Entity } from "../../Entity.model";

export class CreatureTemplateModel extends Entity {
  public maxAttributePoints: number;
  public totalAttributePoints: number;
  public totalSkillsPoints: number;
  public attributes: AttributeTemplateModel[];
  public skills: SkillTemplateModel[];
  public skillsByAttribute: Map<string, SkillTemplateModel[]>;
  public lifes: LifeTemplateModel[];

  constructor() {
    super();
    this.attributes = [];
    this.skills = [];
    this.lifes = [];
  }
}


export class AttributeTemplateModel extends Entity {
}

export class SkillTemplateModel extends Entity {
  public attributeId: string;
  public minorSkills: MinorSkillsTemplateModel[];
  constructor() {
    super();
    this.minorSkills = [];
  }
}

export class MinorSkillsTemplateModel extends Entity {

}

export class LifeTemplateModel extends Entity {
  public value: number;
  public maxValue: number;
}
