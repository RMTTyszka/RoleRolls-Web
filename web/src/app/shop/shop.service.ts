import { Injectable } from '@angular/core';
import {LOH_API} from '../loh.api';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Observable, Subject} from 'rxjs';
import {Shop} from '../shared/models/shop/Shop.model';
import {BuyOutput} from '../shared/models/creatures/heroes/heroShop/BuyOutput';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  path = 'shop'
  serverUrl = LOH_API.myBackUrl;
  public itemBought = new Subject<BuyOutput>();
  constructor(
    private httpClient: HttpClient
  ) {
  }

  public getShop(): Observable<Shop> {
    const params = new HttpParams().set('isShopForCreatingHero', 'true')
    return this.httpClient.get<Shop>(this.serverUrl + this.path + '/find', {params: params});
  }
}
