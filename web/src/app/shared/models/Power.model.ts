import { Entity } from './Entity.model';

export class Power extends Entity {
    name = '';
    description = '';
    category: PowerCategory = null;
}

export enum PowerCategory {
  Support,
  Damage,
  Healing,
  Disabler,
  Utility
}
