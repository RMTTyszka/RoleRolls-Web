import {Injectable, Injector} from '@angular/core';
import {BaseCombatCreatureService} from '../interfaces/baseCombatCreatureService';
import {Hero} from '../../shared/models/NewHero.model';
import {BaseCrudServiceComponent} from '../../shared/base-service/base-crud-service.component';
import {Observable, Subject} from 'rxjs';
import {CampaignSessionService} from '../../campaign-session/campaign-session.service';
import {Creature} from '../../shared/models/creatures/Creature.model';
import {Monster} from '../../shared/models/Monster.model';
import {MonsterService} from '../../monsters/monster/monster.service';

@Injectable({
  providedIn: 'root'
})
export class CampaignCombatMonsterService implements BaseCombatCreatureService<Monster> {
  onEntityChange = new Subject<Monster>();
  getEntitiesForSelect(filter: string, skipCount?: number, maxResultCount?: number): Observable<Monster[]> {
    return this.campaignService.getMonsterFromCampaign();
  }


  constructor(
    private injector: Injector,
    private campaignService: MonsterService
  ) {
  }
}
