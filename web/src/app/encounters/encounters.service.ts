import {Injectable, Injector} from '@angular/core';
import {BaseCrudServiceComponent} from '../shared/base-service/base-crud-service.component';
import {Encounter} from '../shared/models/Encounter.model';

@Injectable({
  providedIn: 'root'
})
export class EncountersService extends BaseCrudServiceComponent<Encounter> {

  constructor(
    injector: Injector,
  ) {
    super(injector);
    this.path = '/encounters';
  }

}
