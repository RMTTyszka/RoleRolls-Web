import {Injectable, Injector} from '@angular/core';
import {CombatActionDto} from '../shared/models/combat/CombatActionDto';
import {LegacyBaseCrudServiceComponent} from '../shared/legacy-base-service/legacy-base-crud-service.component';
import {Observable} from 'rxjs';
import {HttpParams} from '@angular/common/http';
import {Combat} from '../shared/models/combat/Combat.model';
import {AddOrRemoveCreatureToCombatInput} from '../shared/models/combat/AddOrRemoveCreatureToCombatInput';
import {EndTurnInput} from '../shared/models/combat/EndTurnInput';
import {Hero} from '../shared/models/NewHero.model';
import {Monster} from '../shared/models/creatures/monsters/Monster.model';
import {AttackInput} from '../shared/models/combat/AttackInput';

@Injectable({
  providedIn: 'root'
})
export class CombatService extends LegacyBaseCrudServiceComponent<Combat> {

  path = 'combats';
  constructor(
    injector: Injector
  ) {
    super(injector);
  }

  public fullAttackSimulated(attackerId: string, targetId: string): Observable<CombatActionDto> {
      const params = new HttpParams().set('attackerId', attackerId).set('targetId', targetId).set('isFullAttack', 'true')
      return this.http.get<CombatActionDto>(this.serverUrl + this.path + '/getAttackRoll', {params} );
  }
  public fullAttack(combatId: string, input: AttackInput): Observable<CombatActionDto> {
      return this.http.post<CombatActionDto>(this.serverUrl + this.path + `/${combatId}` + '/attack', input );
  }
  public getCombat(id: string): Observable<Combat> {
      const params = new HttpParams().set('id', id);
      return this.http.get<Combat>(this.serverUrl + this.path + '/getCombat', {params} );
  }
  public startCombat(combat: Combat): Observable<CombatActionDto> {
      return this.http.post<CombatActionDto>(this.serverUrl + this.path + '/getCombat', {combat} );
  }
  public addHero(combatId: string, input: AddOrRemoveCreatureToCombatInput<Hero>): Observable<Combat> {
      return this.http.post<Combat>(this.serverUrl + this.path + `/${combatId}` + '/hero', input );
  }

  public removeHero(combatId: string, creatureId: string) {
    const params = new HttpParams().set('creatureId', creatureId);
    return this.http.delete<Combat>(this.serverUrl + this.path + `/${combatId}` + '/hero', {params});
  }
  public removeMonster(combatId: string, creatureId: string): Observable<Combat> {
    const params = new HttpParams().set('creatureId', creatureId);
    return this.http.delete<Combat>(this.serverUrl + this.path + `/${combatId}` + '/monster', {params});
  }

  public addMonster(combatId: string, input: AddOrRemoveCreatureToCombatInput<Monster>) {
    return this.http.post<Combat>(this.serverUrl + this.path + `/${combatId}` + '/monster', input );
  }
  public endTurn(combatId: string, input: EndTurnInput) {
    return this.http.post<Combat>(this.serverUrl + this.path + `/${combatId}` + '/endTurn', input );
  }

  public getHeroesTargets(combat: Combat) {
    return combat.monsters.concat(combat.heroes);
  }
  getMonsterTargets(combat: Combat) {
    return combat.heroes.concat(combat.monsters);
  }
}
