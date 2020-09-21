import {Injectable, Injector} from '@angular/core';
import {LegacyBaseCrudServiceComponent} from '../shared/legacy-base-service/legacy-base-crud-service.component';
import {Role} from '../shared/models/Role.model';

@Injectable({
  providedIn: 'root'
})
export class RolesService extends LegacyBaseCrudServiceComponent<Role> {


  constructor(
    injector: Injector
  ) {
    super(injector);
    this.path = 'roles';
   }
}
