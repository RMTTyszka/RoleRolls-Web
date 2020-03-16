import { Injectable, Injector } from '@angular/core';
import { CombatActionDto } from '../shared/models/CombatActionDto';
import { BaseCrudServiceComponent } from '../shared/base-service/base-crud-service.component';
import { Observable } from 'rxjs';
import {HttpParams} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CombatService extends BaseCrudServiceComponent<CombatActionDto> {

  path = 'combat';
  constructor(
    injector: Injector
  ) {
    super(injector);
  }

  public fullAttackSimulated(attackerId: string, targetId: string): Observable<CombatActionDto> {
      const params = new HttpParams().set('attackerId', attackerId).set('targetId', targetId).set('isFullAttack', 'true')
      return this.http.get<CombatActionDto>(this.serverUrl + this.path + '/getAttackRoll', {params} );
  }
}
