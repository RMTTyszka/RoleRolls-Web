import { Injectable, Injector } from '@angular/core';
import { BaseCrudServiceComponent } from '../shared/base-service/base-crud-service.component';
import { Role } from '../shared/models/Role.model';

@Injectable({
  providedIn: 'root'
})
export class RolesService extends BaseCrudServiceComponent<Role> {


  constructor(
    injector: Injector
  ) {
    super(injector);
    this.path = 'roles';
   }
}
