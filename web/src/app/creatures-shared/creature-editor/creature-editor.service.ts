import { Injectable } from '@angular/core';
import {Subject, Subscription} from 'rxjs';
import {Creature} from '../../shared/models/creatures/Creature.model';

@Injectable()
export class CreatureEditorService {

  private creatureUpdated = new Subject<Creature>()
  private creatureUpdatedSubscriptions = new Map<string, Subscription>();
  constructor() { }

  public subscribeToCreatureUpdated(componentName: string, callBack: (creature: Creature) => void) {
      if (this.creatureUpdatedSubscriptions.has(componentName)) {
        this.creatureUpdatedSubscriptions.get(componentName).unsubscribe();
      }
    this.creatureUpdatedSubscriptions.set(componentName, this.creatureUpdated.subscribe(creature => callBack(creature)));
  }
  public publishCreatureUpdated(creature: Creature) {
    this.creatureUpdated.next(creature);
  }
  public unsubscribeCreatureUpdated(componentName: string) {
    if (this.creatureUpdatedSubscriptions.has(componentName)) {
      this.creatureUpdatedSubscriptions.get(componentName).unsubscribe();
    }
  }
}
