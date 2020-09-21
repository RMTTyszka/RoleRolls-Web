import {Injectable, Injector} from '@angular/core';
import {BaseCombatCreatureService} from '../interfaces/baseCombatCreatureService';
import {Hero} from '../../shared/models/NewHero.model';
import {LegacyBaseCrudServiceComponent} from '../../shared/legacy-base-service/legacy-base-crud-service.component';
import {Observable, Subject} from 'rxjs';
import {CampaignSessionService} from '../../campaign-session/campaign-session.service';
import {Creature} from '../../shared/models/creatures/Creature.model';

@Injectable({
  providedIn: 'root'
})
export class CampaignCombatHeroService implements BaseCombatCreatureService<Hero> {
  onEntityChange = new Subject<Hero>();
  getEntitiesForSelect(filter: string, skipCount?: number, maxResultCount?: number): Observable<Hero[]> {
    return this.campaignService.getHeroesFromCampaign();
  }


  constructor(
    private injector: Injector,
    private campaignService: CampaignSessionService
  ) {
  }
}
