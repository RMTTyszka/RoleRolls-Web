import {Injectable, Injector} from '@angular/core';
import {BaseEntityService} from '../shared/base-entity-service';
import {Hero} from '../shared/models/NewHero.model';


@Injectable({
  providedIn: 'root'
})
export class NewHeroService extends BaseEntityService<Hero> {
  path = 'hero';

  constructor(
    injector: Injector
  ) {
    super(injector, Hero);
  }


}

