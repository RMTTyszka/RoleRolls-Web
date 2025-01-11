import { Entity } from '../../models/Entity.model';
import { RollOrigin } from '../../models/RollOrigin';

export class PocketRoll extends Entity{
  public campaignId!: string;
  public actorId!: string;
  public actorName!: string;
  public rolledDices!: string;
  public numberOfDices!: number;
  public propertyId!: string;
  public propertyType!: RollOrigin;
  public propertyName!: string;
  public numberOfSuccesses!: number;
  public numberOfCriticalSuccesses!: number;
  public numberOfCriticalFailures!: number;
  public difficulty!: number;
  public complexity!: number;
  public rollBonus!: number;
  public success!: boolean;
  public sceneId!: string;
  public dateTime!: string;
}
