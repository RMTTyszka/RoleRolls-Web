import {Injectable, Injector} from '@angular/core';
import {BaseEntityService} from 'src/app/shared/base-entity-service';
import {Race} from 'src/app/shared/models/Race.model';

@Injectable({
  providedIn: 'root'
})
export class RaceService extends BaseEntityService<Race> {
  path = 'races';
  constructor(
    injector: Injector,
    ) {
    super(injector, Race);
   }
}
