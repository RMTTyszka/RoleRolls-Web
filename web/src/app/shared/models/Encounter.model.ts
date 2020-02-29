import {Entity} from './Entity.model';

export class Encounter extends Entity {
    name: string;
    level: number;
    monsters: string[] = [];
}
