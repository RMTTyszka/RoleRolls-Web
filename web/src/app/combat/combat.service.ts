import {Injectable, Injector} from '@angular/core';
import {CombatActionDto} from '../shared/models/CombatActionDto';
import {BaseCrudServiceComponent} from '../shared/base-service/base-crud-service.component';
import {Observable} from 'rxjs';
import {HttpParams} from '@angular/common/http';
import {Combat} from '../shared/models/Combat.model';
import {AddHeroToCombatInput} from '../shared/models/combat/AddHeroToCombatInput';
import {Initiative} from '../shared/models/Iniciative.model';
import {AddMonsterToCombatInput} from '../shared/models/combat/AddMonsterToCombatInput';
import {EndTurnInput} from '../shared/models/combat/EndTurnInput';

@Injectable({
  providedIn: 'root'
})
export class CombatService extends BaseCrudServiceComponent<Combat> {

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
  public getCombat(id: string): Observable<Combat> {
      const params = new HttpParams().set('id', id);
      return this.http.get<Combat>(this.serverUrl + this.path + '/getCombat', {params} );
  }
  public startCombat(combat: Combat): Observable<CombatActionDto> {
      return this.http.post<CombatActionDto>(this.serverUrl + this.path + '/getCombat', {combat} );
  }
  public addHero(input: AddHeroToCombatInput): Observable<Initiative> {
      return this.http.post<Initiative>(this.serverUrl + this.path + '/addHero', input );
  }

  public addMonster(input: AddMonsterToCombatInput) {
    return this.http.post<Initiative>(this.serverUrl + this.path + '/addMonster', input );
  }
  public endTurn(input: EndTurnInput) {
    return this.http.post<Initiative>(this.serverUrl + this.path + '/endTurn', input );
  }

  public getHeroesTargets(combat: Combat) {
    return combat.monsters.concat(combat.heroes);
  }
  getMonsterTargets(combat: Combat) {
    return combat.heroes.concat(combat.monsters);
  }
}
