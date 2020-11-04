import {Skill} from './Skill';

export class CreatureSkills {
  combat: Skill = new Skill();
  sports: Skill = new Skill();
  nimbleness: Skill = new Skill();
  knowledge: Skill = new Skill();
  perception: Skill = new Skill();
  resistance: Skill = new Skill();
  relationship: Skill = new Skill();
  skillsList: string[] = [];
  maxPoints: number;
  remainingPoints: number;
}
