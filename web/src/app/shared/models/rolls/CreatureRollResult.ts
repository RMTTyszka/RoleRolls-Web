import {Roll} from './Roll';

export class CreatureRollResult {
  public creatureId: string;
  public creatureName: string;
  public property: string;
  public success: boolean;
  public rolls: Roll[] = [];
  public bonusDice: number;
  public numberOfRolls: number;
  public rollSuccesses: number;
  public successes: number;
  public criticalSuccesses: number;
  public criticalFailures: number;
  public difficulty: number;
  public complexity: number;
  public creationTime: string;
}
