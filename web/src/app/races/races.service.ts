import {Injectable, Injector} from '@angular/core';

import {LegacyBaseCrudServiceComponent} from '../shared/legacy-base-service/legacy-base-crud-service.component';
import {Race} from '../shared/models/Race.model';

@Injectable({
  providedIn: 'root'
})
export class RacesService extends LegacyBaseCrudServiceComponent<Race> {

  path = 'races';

  constructor(
    injector: Injector
  ) {
    super(injector);
   }

}
