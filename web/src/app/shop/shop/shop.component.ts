import { Component, OnInit } from '@angular/core';
import {ShopArmor} from '../../shared/models/shop/ShopArmor.model';
import {ShopItem} from '../../shared/models/shop/ShopItem.model';
import {ShopService} from '../shop.service';
import {HeroManagementService} from '../../heroes/hero-management.service';

@Component({
  selector: 'loh-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.css']
})
export class ShopComponent implements OnInit {
  filterArmors: boolean;
  filterWeapons: boolean;
  itemsShow: ShopItem[] = [];
  initialItems: ShopItem[] = [];
  items: ShopItem[] = [];
  itemToBuyShow: ShopItem[] = [];
  itemToBuy: ShopItem[] = [];
  totalCost = 0;
  cash = 0;
  shopName = 'Shop';
  get hasEnoughCash() {
    return this.cash >= this.totalCost;
  }
  constructor(
    private shopService: ShopService,
    private heroManagementService: HeroManagementService
  ) {
    this.shopService.getShop().subscribe(shop => {
      this.shopName = shop.name;
      shop.armors.forEach(item => {
        item.quantityToBuy = 0;
      });
      this.initialItems.push(...shop.armors.map(i => <ShopItem> {
        id: i.id,
        quantity: i.quantity,
        quantityToBuy: 0,
        value: i.value,
        name: i.name,
        item: i.item
      }));
      this.items.push(...shop.armors);
      this.itemsShow.push(...shop.armors);
    });
  }

  ngOnInit(): void {
    this.cash = this.heroManagementService.hero.inventory.cash1;
  }

  buy() {
    if (this.hasEnoughCash) {
      this.heroManagementService.buyItems(this.itemToBuy);
      this.itemToBuy = [];
      this.itemToBuyShow = [];
    }
  }
  reset() {
    this.itemToBuy = [];
    this.itemToBuyShow = [];
    this.items = [];
    this.items.push(...this.initialItems);
    this.itemsShow = [];
    this.itemsShow.push(...this.initialItems);
    this.totalCost = 0;
  }

  itemAddedToBuy(order: {items: Array<ShopItem>}) {
    order.items.forEach(item => {
        if (item.quantity > 0) {
          let alredyBoughtItem = this.itemToBuy.find(i => i.id === item.id);
          if (alredyBoughtItem) {
            alredyBoughtItem.quantityToBuy++;
          } else {
            alredyBoughtItem = <ShopItem> {
              id: item.id,
              quantityToBuy: 1,
              value: item.value,
              name: item.name,
              item: item.item
            };
            this.itemToBuy.push(alredyBoughtItem);
          }
          this.itemToBuyShow = [];
          this.itemToBuyShow.push(...this.itemToBuy);

          let itemSelled = this.items.find(i => i.id === item.id);
          if (itemSelled) {
            itemSelled.quantity--;
          } else {
            itemSelled = <ShopItem> {
              id: item.id,
              quantityToBuy: 0,
              quantity: item.quantity - 1,
              value: item.value,
              name: item.name,
              item: item.item
            };
            this.items.push(itemSelled);
          }
          if (itemSelled.quantity <= 0) {
            this.items.splice(this.items.indexOf(alredyBoughtItem), 1);
          }
          this.itemsShow = [];
          this.itemsShow.push(...this.items);

          this.totalCost += item.value;
        }
      });
  }
  itemRemovedFromToBuy(order: {items: Array<ShopItem>}) {
    order.items.forEach(item => {
        const alreadyBoughtItem = this.itemToBuy.find(i => i.id === item.id);
      alreadyBoughtItem.quantityToBuy--;
        if (alreadyBoughtItem.quantityToBuy <= 0) {
          this.itemToBuy.splice(this.itemToBuy.indexOf(alreadyBoughtItem), 1);
        }
        this.itemToBuyShow = [];
        this.itemToBuyShow.push(...this.itemToBuy);
        const itemSelled = this.items.find(i => i.id === item.id);
        itemSelled.quantity++;
        this.itemsShow = [];
        this.itemsShow.push(...this.items);

      this.totalCost -= item.value;

    });
  }
}
