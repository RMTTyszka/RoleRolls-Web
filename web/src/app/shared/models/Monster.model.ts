import { Role } from './Role.model';
import { Entity } from './Entity.model';
import { Race } from './Race.model';
import { Bonus } from './Bonus.model';
import { MonsterModel } from './MonsterModel.model';
import { Attributes } from './Attributes.model';
import { Skills } from './Skills.model';



export class Monster extends Entity {
  name = '';
  level = 1;
  monsterBase = new MonsterModel();
  attributes: Attributes = new Attributes();
  race: Race = new Race();
  role: Role = new Role();
  enviroment = [];
  bonuses: Bonus[] = [];
  skills: Skills =  new Skills();
  maxAttributesBonusPoints = 0;
  maxSkillsBonusPoints = 0;
  totalAttributesBonusPoints = 0;
  totalSkillsBonusPoints = 0;

}
