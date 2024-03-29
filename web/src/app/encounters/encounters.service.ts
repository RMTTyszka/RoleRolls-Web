import {Injectable, Injector} from '@angular/core';
import {LegacyBaseCrudServiceComponent} from '../shared/legacy-base-service/legacy-base-crud-service.component';
import {Encounter} from '../shared/models/Encounter.model';
import {BaseCrudService} from '../shared/base-service/base-crud-service';
import {RRColumns} from '../shared/components/rr-grid/r-r-grid.component';
import {Observable} from 'rxjs';
import {MonsterModel} from '../shared/models/creatures/monsters/MonsterModel.model';
import {BaseCrudResponse} from '../shared/models/BaseCrudResponse';
import {tap} from 'rxjs/operators';
import {EncounterConfig} from './encounter-config';

// @ts-ignore
@Injectable({
  providedIn: 'root'
})
export class EncountersService extends BaseCrudService<Encounter, Encounter> {
  entityListColumns: RRColumns[];
  fieldName: string;
  path = 'encounters';
  selectModalColumns: RRColumns[];
  selectModalTitle: string;
  selectPlaceholder: string;
  config = new EncounterConfig();
  constructor(
    injector: Injector,
  ) {
    super(injector);
    BaseCrudService.setConfig(this, this.config);
  }

  addMonster(encounterId: string, monsterTemplate: MonsterModel): Observable<void> {
    return this.http.post<void>(this.serverUrl + this.path + `/${encounterId}/monsters`, monsterTemplate);
  }

  removeMonster(encounterId: string, monsterTemplateId): Observable<void> {
    return this.http.delete<void>(this.serverUrl + this.path + `/${encounterId}/monsters/${monsterTemplateId}`);
  }
}
