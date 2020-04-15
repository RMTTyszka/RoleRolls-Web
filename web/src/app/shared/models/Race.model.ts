import {Power} from './Power.model';
import {Bonus} from './Bonus.model';
import {Entity} from './Entity.model';

export class Race extends Entity {
    bonuses: Bonus[] = [];
    name = '';
    powers: Power[] = [];
    traits: any[] = [];
}
