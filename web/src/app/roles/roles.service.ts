import {Injectable, Injector} from '@angular/core';
import {LegacyBaseCrudServiceComponent} from '../shared/legacy-base-service/legacy-base-crud-service.component';
import {Role} from '../shared/models/Role.model';
import {BaseCrudService} from '../shared/base-service/base-crud-service';
import {RRColumns} from '../shared/components/cm-grid/cm-grid.component';

@Injectable({
  providedIn: 'root'
})
export class RolesService extends BaseCrudService<Role, Role> {
  editorModal: any;
  entityListColumns: RRColumns[] = [
    {
      header: 'Name',
      property: 'name',
    }
  ];
  fieldName = 'name';
  selectModalColumns: RRColumns[] = [
    {
      header: 'Name',
      property: 'name',
    }
  ];
  path = 'roles';
  selectModalTitle: 'Role';
  selectPlaceholder = 'Role';


  constructor(
    injector: Injector
  ) {
    super(injector);
    this.path = 'roles';
   }
}
