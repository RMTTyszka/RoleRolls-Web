import { CreatureType } from "../../creatures/CreatureType";
import { PocketEntity } from "../pocket-entity";



export class PocketCreature extends PocketEntity {
  public name: string;
  public attributes: PocketAttribute[] = [];
  public skills: PocketSkill[] = [];
  public lifes: PocketLife[] = [];
  public creatureType: CreatureType;
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
export enum PocketSkillProficience {
  Expert = 0, // +4
  Good = 1, // +2
  Normal = 3, // 0
  Bad = 4, // + -1
  Crap = 5 // -3
}


export class PocketHero extends PocketCreature {
  public ownerId: string;
}

export class PocketMonster extends PocketCreature {
}
