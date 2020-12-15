import {Injectable, Injector} from '@angular/core';
import {BaseEntityService} from '../shared/base-entity-service';
import {Hero} from '../shared/models/NewHero.model';
import {ItemInstance} from '../shared/models/ItemInstance.model';
import {AuthenticationService} from '../authentication/authentication.service';
import {BaseCrudResponse} from '../shared/models/BaseCrudResponse';
import {Observable} from 'rxjs';
import {BaseCrudService} from '../shared/base-service/base-crud-service';
import {RRColumns} from '../shared/components/rr-grid/r-r-grid.component';

@Injectable({
  providedIn: 'root'
})
export class HeroesService extends BaseCrudService<Hero, Hero> {
  entityListColumns: RRColumns[];
  fieldName: string;
  selectModalColumns: RRColumns[];
  selectModalTitle: string;
  selectPlaceholder: string;
  path = 'heroes';
  constructor(
    injector: Injector,
    private authService: AuthenticationService
  ) {
    super(injector);
  }

  create(entity: Hero): Observable<BaseCrudResponse<Hero>> {
    entity.ownerId = this.authService.userId;
    return super.create(entity);
  }

/*  static getTotalSkillsBonusPoints(level: number) {
    return level * 6 + 12;
  }

  static getMaximumSkillsBonusPoints(level: number) {
    return Number(level) + 2;
  }*/

  public addItemsToInventory(heroId: string, items: ItemInstance[]) {
    return this.http.put(this.serverUrl + this.path + '/addItems', {
      items: items,
      heroId: heroId
    }).toPromise();
  }

}
