import {Injectable, Injector} from '@angular/core';
import {BaseEntityService} from '../../../../shared/base-entity-service';
import {ArmorModel} from '../../../../shared/models/Armor.model';

@Injectable({
  providedIn: 'root'
})
export class ArmorService extends BaseEntityService<ArmorModel> {

  path = 'armors';

  constructor(
    injector: Injector
  ) {
    super(injector, ArmorModel);
  }
}
