import {Entity} from './Entity.model';
import {Bonus} from './Bonus.model';

export class Role extends Entity {
    name = '';
    bonuses: Bonus[] = [];
    skillPoints = 0;
}
