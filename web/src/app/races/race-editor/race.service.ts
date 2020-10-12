import {Injectable, Injector} from '@angular/core';
import {BaseEntityService} from 'src/app/shared/base-entity-service';
import {Race} from 'src/app/shared/models/Race.model';
import {BaseCrudService} from '../../shared/base-service/base-crud-service';
import {RRColumns} from '../../shared/components/rr-grid/r-r-grid.component';

@Injectable({
  providedIn: 'root'
})
export class RaceService extends BaseCrudService<Race, Race> {
  entityListColumns: RRColumns[];
  fieldName: string;
  selectModalColumns: RRColumns[];
  selectModalTitle: string;
  selectPlaceholder: string;
  path = 'races';
  constructor(
    injector: Injector,
    ) {
    super(injector);
   }
}
