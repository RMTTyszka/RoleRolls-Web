import {Injectable, Injector} from '@angular/core';
import {LegacyBaseCrudServiceComponent} from '../shared/legacy-base-service/legacy-base-crud-service.component';
import {Encounter} from '../shared/models/Encounter.model';

@Injectable({
  providedIn: 'root'
})
export class EncountersService extends LegacyBaseCrudServiceComponent<Encounter> {

  constructor(
    injector: Injector,
  ) {
    super(injector);
    this.path = '/encounters';
  }

}
