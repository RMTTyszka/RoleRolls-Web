import { Injectable } from '@angular/core';
import {Subject} from 'rxjs';
import {CreatureRollResult} from '../../shared/models/rolls/CreatureRollResult';

@Injectable()
export class CampaignRollsService {

  private _creatureRolled = new Subject<CreatureRollResult>();
  constructor() { }


  creatureRolled() {
    return this._creatureRolled.asObservable();
  }
  emitCreatureRolled(rollResult: CreatureRollResult) {
    this._creatureRolled.next(rollResult);
  }
}
