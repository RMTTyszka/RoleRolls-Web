import { Entity } from './Entity.model';
import { Attributes } from './Attributes.model';
import { Race } from './Race.model';
import { Role } from './Role.model';
import { Bonus } from './Bonus.model';
import { Skills } from './Skills.model';

export class Hero extends Entity {
  name = '';
  level = 1;
  experience = 0;
  nextLevel = 1000;
  attributes: Attributes = new Attributes();
  baseAttributes: Attributes = new Attributes();
  race: Race = new Race();
  role: Role = new Role();
  enviroment = [];
  bonuses: Bonus[] = [];
  skills: Skills =  new Skills();
  maxAttributesBonusPoints = 0;
  maxSkillsBonusPoints = 0;
  totalAttributesBonusPoints = 0;
  totalSkillsBonusPoints = 0;
  maxLife = 0;

}
