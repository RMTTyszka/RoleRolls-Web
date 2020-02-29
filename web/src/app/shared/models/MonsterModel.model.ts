import { Race } from './Race.model';
import { Role } from './Role.model';
import { Bonus } from './bonus.model';
import { Entity } from './Entity.model';
import { Attributes } from './Attributes.model';

export class MonsterModel extends Entity {
    name = '';
    race: Race = new Race();
    enviroment = [];
    role: Role = new Role();
    bonuses: Bonus[] = [];
    mainSkills: string[] = [];
    attributes: Attributes = new Attributes();
    maxLife = 0;
}
