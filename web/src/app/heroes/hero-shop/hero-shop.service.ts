import { Injectable } from '@angular/core';
import {LOH_API} from '../../loh.api';
import {HttpClient} from '@angular/common/http';
import {BuyOutput} from '../../shared/models/creatures/heroes/heroShop/BuyOutput';
import {BuyInput} from '../../shared/models/creatures/heroes/heroShop/BuyInput';
import {ShopService} from '../../shop/shop.service';
import {finalize, tap} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class HeroShopService {

  private serviceEndpoint = 'HeroShop'
  private buyUrl = '/buy'
  private serverUrl = LOH_API.myBackUrl;
  constructor(
    private http: HttpClient,
    private shopService: ShopService
  ) { }


  public buy(heroId: string, shopId: string, itemId: string, quantity: number) {
    const buyInput = <BuyInput> {
      heroId: heroId,
      shopItemId: itemId,
      quantity: quantity,
      shopId: shopId
    }
    return this.http.post<BuyOutput>(this.serverUrl + this.serviceEndpoint + this.buyUrl, buyInput)
      .pipe(
        tap((itemBought) => {
          this.shopService.itemBought.next(itemBought);
        })
      );
  }
}
