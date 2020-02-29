import { Entity } from './Entity.model';

export class CombatActionDto extends Entity {

    mainWeaponHits: number[];
    offWeaponHits: number[];

}
