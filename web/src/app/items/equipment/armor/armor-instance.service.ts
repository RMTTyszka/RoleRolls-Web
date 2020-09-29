import {Injectable, Injector} from '@angular/core';
import {LegacyBaseCrudServiceComponent} from '../../../shared/legacy-base-service/legacy-base-crud-service.component';
import {ArmorInstance} from '../../../shared/models/items/ArmorInstance.model';
import {ArmorModel} from '../../../shared/models/items/ArmorModel.model';
import {BaseCrudResponse} from '../../../shared/models/BaseCrudResponse';

@Injectable({
  providedIn: 'root'
})
export class ArmorInstanceService extends LegacyBaseCrudServiceComponent<ArmorInstance> {
  path = 'armorInstance';
  constructor(
    injector: Injector
  ) {
    super(injector);
  }

  instantiate(armorTemplate: ArmorModel, level: number) {
    return this.http.post<BaseCrudResponse<ArmorInstance>>(this.serverUrl + this.path + '/instantiate', {
      armorTemplate: armorTemplate,
      level: level
    });
  }
}
