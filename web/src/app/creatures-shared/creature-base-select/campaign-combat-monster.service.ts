import {Injectable, Injector} from '@angular/core';
import {BaseCombatCreatureService} from '../interfaces/baseCombatCreatureService';
import {Observable, Subject} from 'rxjs';
import {Monster} from '../../shared/models/creatures/monsters/Monster.model';
import {MonsterService} from '../../monsters/monster/monster.service';
import {map} from 'rxjs/operators';
import {PagedOutput} from '../../shared/dtos/PagedOutput';

@Injectable({
  providedIn: 'root'
})
export class CampaignCombatMonsterService implements BaseCombatCreatureService<Monster> {
  onEntityChange = new Subject<Monster>();
  getEntitiesForSelect(filter: string, skipCount?: number, maxResultCount?: number): Observable<Monster[]> {
    return this.monsterService.list(filter, skipCount, maxResultCount).pipe(map((pagedResult: PagedOutput<Monster>) => pagedResult.content));
  }


  constructor(
    private injector: Injector,
    private monsterService: MonsterService
  ) {
  }
}
