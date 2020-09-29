import {Creature} from '../../shared/models/creatures/Creature.model';
import {Observable, Subject} from 'rxjs';

export interface BaseCombatCreatureService<T extends Creature> {
  onEntityChange: Subject<T>;
  getEntitiesForSelect(filter: string, skipCount?: number, maxResultCount?: number): Observable<T[]>;
}
