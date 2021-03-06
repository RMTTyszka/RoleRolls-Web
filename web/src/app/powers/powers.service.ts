import {Injectable, Injector} from '@angular/core';
import {Observable} from 'rxjs';
import {LOH_API} from '../loh.api';
import {LegacyBaseCrudServiceComponent} from '../shared/legacy-base-service/legacy-base-crud-service.component';
import {Power} from '../shared/models/Power.model';

@Injectable({
  providedIn: 'root'
})
export class PowersService extends  LegacyBaseCrudServiceComponent<Power> {

  path = 'powers';

  constructor(
    injector: Injector
  ) {
    super(injector);
   }

  getAllPowers(): Observable<string[]> {
    return this.http.get<string[]>(LOH_API.myBackUrl + 'powers/all');
  }

  getAllTraits(): Observable<string[]> {
    return this.http.get<string[]>(LOH_API.myBackUrl + 'powers/alltraits');
  }
}
