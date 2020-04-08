import {Injectable, Injector} from '@angular/core';

import {BaseCrudServiceComponent} from '../shared/base-service/base-crud-service.component';
import {Race} from '../shared/models/Race.model';

@Injectable({
  providedIn: 'root'
})
export class RacesService extends BaseCrudServiceComponent<Race> {

  path = 'races';

  constructor(
    injector: Injector
  ) {
    super(injector);
   }

}
