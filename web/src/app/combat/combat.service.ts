import { Injectable, Injector } from '@angular/core';
import { CombatActionDto } from '../shared/models/CombatActionDto';
import { BaseCrudServiceComponent } from '../shared/base-service/base-crud-service.component';
import { Observable } from 'rxjs';

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

  public fullAttackSimulated(attackerId: number | string, targetId: number | string): Observable<CombatActionDto> {
      return this.http.get<CombatActionDto>(this.serverUrl + this.path + '/fullAttack', {
        params: { attackerId: attackerId.toString(), targetId: targetId.toString() }} );
  }
}
