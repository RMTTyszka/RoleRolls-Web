import {Injectable, Injector} from '@angular/core';

import {LegacyBaseCrudServiceComponent} from '../shared/legacy-base-service/legacy-base-crud-service.component';
import {Race} from '../shared/models/Race.model';
import {BaseCrudService} from '../shared/base-service/base-crud-service';
import {RRColumns} from '../shared/components/cm-grid/cm-grid.component';

@Injectable({
  providedIn: 'root'
})
export class RacesService extends BaseCrudService<Race> {
  editorModal: any;
  entityListColumns: RRColumns[] = [
    {
      header: 'Name',
      property: 'name'
    }
  ];
  fieldName = 'name'
  selectModalColumns: RRColumns[] = [
    {
      header: 'Name',
      property: 'name'
    }
  ];
  selectModalTitle = 'Race';
  selectPlaceholder = 'Race';

  path = 'races';

  constructor(
    injector: Injector
  ) {
    super(injector);
   }

}
