import { Creature } from '@app/campaigns/models/creature';
import { Entity } from '@app/models/Entity.model';

export class Encounter implements Entity {
  public id: string;
  public name: string;
  public creatures: Creature[];
}
