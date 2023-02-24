import { PocketEntity } from "src/app/shared/models/pocket/pocket-entity"
import { RollOrigin } from "../campaign-heroes/RollOrigin";

export class PocketRoll extends PocketEntity{
  public campaignId: string;
  public actorId: string;
  public actorName: string;
  public rolledDices: string;
  public numberOfDices: number;
  public propertyId: string;
  public propertyType: RollOrigin;
  public propertyName: string;
  public numberOfSuccesses: number;
  public numberOfCriticalSuccesses: number;
  public numberOfCriticalFailures: number;
  public difficulty: number;
  public complexity: number;
  public rollBonus: number;
  public success: boolean;
  public sceneId: string;
  public dateTime: string;
}
