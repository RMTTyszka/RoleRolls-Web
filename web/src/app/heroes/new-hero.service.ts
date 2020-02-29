import {Injectable, Injector} from '@angular/core';
import {BaseEntityService} from '../shared/base-entity-service';
import {NewHero} from '../shared/models/NewHero.model';


@Injectable({
  providedIn: 'root'
})
export class NewHeroService extends BaseEntityService<NewHero> {
  path = 'hero';

  constructor(
    injector: Injector
  ) {
    super(injector, NewHero);
  }


}

