import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormGroup} from '@angular/forms';
import {Inventory} from '../../../shared/models/Inventory.model';
import {WeaponInstance} from '../../../shared/models/WeaponInstance.model';
import {HeroCreateShopService} from '../hero-create-shop.service';
import {subscribeOn} from 'rxjs/operators';
import {Shop} from '../../../shared/models/shop/Shop.model';

@Component({
  selector: 'loh-create-hero-weapon-select',
  templateUrl: './create-hero-weapon-select.component.html',
  styleUrls: ['./create-hero-weapon-select.component.css']
})
export class CreateHeroWeaponSelectComponent implements OnInit {
  placeholder = 'Weapon';
  shop: Shop
  constructor(
    private shopService: HeroCreateShopService
  ) { }

  ngOnInit(): void {
    this.shopService.getShop().subscribe((shop) => {
      this.shop = shop;
    });
  }



}
