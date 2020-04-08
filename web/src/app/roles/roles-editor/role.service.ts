import {Injectable, Injector} from '@angular/core';
import {BaseEntityService} from 'src/app/shared/base-entity-service';
import {Role} from 'src/app/shared/models/Role.model';

@Injectable({
  providedIn: 'root'
})
export class RoleService extends BaseEntityService<Role> {

  path = 'roles';
  constructor(
    injector: Injector) {
    super(injector, Role);
   }
}
