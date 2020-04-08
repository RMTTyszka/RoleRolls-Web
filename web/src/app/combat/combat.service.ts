import {Injectable, Injector} from '@angular/core';
import {CombatActionDto} from '../shared/models/CombatActionDto';
import {BaseCrudServiceComponent} from '../shared/base-service/base-crud-service.component';
import {Observable} from 'rxjs';
import {HttpParams} from '@angular/common/http';
import {Combat} from '../shared/models/Combat.model';

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
  public fullAttack(attackerId: string, targetId: string): Observable<CombatActionDto> {
      const params = new HttpParams().set('attackerId', attackerId).set('targetId', targetId);
      return this.http.get<CombatActionDto>(this.serverUrl + this.path + '/fullAttack', {params} );
  }
  public getCombat(id: string): Observable<CombatActionDto> {
      const params = new HttpParams().set('id', id);
      return this.http.get<CombatActionDto>(this.serverUrl + this.path + '/getCombat', {params} );
  }
  public startCombat(combat: Combat): Observable<CombatActionDto> {
      return this.http.post<CombatActionDto>(this.serverUrl + this.path + '/getCombat', {combat} );
  }
}
