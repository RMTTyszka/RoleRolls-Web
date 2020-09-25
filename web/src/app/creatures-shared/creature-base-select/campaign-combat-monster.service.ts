import {Injectable, Injector} from '@angular/core';
import {BaseCombatCreatureService} from '../interfaces/baseCombatCreatureService';
import {Hero} from '../../shared/models/NewHero.model';
import {LegacyBaseCrudServiceComponent} from '../../shared/legacy-base-service/legacy-base-crud-service.component';
import {Observable, Subject} from 'rxjs';
import {CampaignSessionService} from '../../campaign-session/campaign-session.service';
import {Creature} from '../../shared/models/creatures/Creature.model';
import {Monster} from '../../shared/models/Monster.model';
import {MonsterService} from '../../monsters/monster/monster.service';
import {MonsterCrudService} from '../../monsters/monster-crud.service';
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
    private monsterService: MonsterCrudService
  ) {
  }
}
