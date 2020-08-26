import {Injectable, Injector} from '@angular/core';
import {BaseEntityService} from '../../shared/base-entity-service';
import {Observable} from 'rxjs';
import {Shop} from '../../shared/models/inputs/Shop.model';
import {HttpClient, HttpParams} from '@angular/common/http';
import {LOH_API} from '../../loh.api';

@Injectable({
  providedIn: 'root'
})
export class HeroCreateShopService {
  path = 'shop'
  serverUrl = LOH_API.myBackUrl;
  constructor(
    private httpClient: HttpClient
  ) {
  }

  public getShop(): Observable<Shop> {
    const params = new HttpParams().set('isShopForCreatingHero', 'true')
    return this.httpClient.get<Shop>(this.serverUrl + this.path + '/find', { params: params} );
  }
}
