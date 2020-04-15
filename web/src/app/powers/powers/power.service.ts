import {Injectable, Injector} from '@angular/core';
import {Power} from 'src/app/shared/models/Power.model';
import {BaseEntityService} from 'src/app/shared/base-entity-service';

@Injectable({
  providedIn: 'root'
})
export class PowerService extends BaseEntityService<Power> {

  path = 'powers';
  constructor(
    injector: Injector
  ) {
    super(injector, Power);
   }

   getPowerUsages(id: number | string) {
    return this.http.get<any>(this.serverUrl + this.path + '/findPowerUsages', {params: {id: id.toString()}});
   }
}
