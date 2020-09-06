import {Injectable, Injector} from '@angular/core';
import {BaseEntityService} from '../../../../shared/base-entity-service';
import {ArmorModel} from 'src/app/shared/models/items/ArmorModel.model';

@Injectable({
  providedIn: 'root'
})
export class ArmorTemplateService extends BaseEntityService<ArmorModel> {

  path = 'armorModels';

  constructor(
    injector: Injector
  ) {
    super(injector, ArmorModel);
  }
}
