import {Component, Input, OnInit} from '@angular/core';
import {ShopItem} from '../../shared/models/shop/ShopItem.model';
import {ShopService} from '../shop.service';
import {HeroManagementService} from '../../heroes/hero-management.service';
import {Hero} from '../../shared/models/NewHero.model';
import {Shop} from '../../shared/models/shop/Shop.model';
import {BuyOutput} from '../../shared/models/creatures/heroes/heroShop/BuyOutput';
import {ItemTemplateType} from '../../shared/models/items/ItemTemplateType.enum';
import {ArmorModel} from '../../shared/models/items/ArmorModel.model';
import {ArmorCategory} from '../../shared/models/items/ArmorCategory.model';
import {WeaponCategory} from '../../shared/models/WeaponCategory.model';
import {WeaponModel} from '../../shared/models/WeaponModel.model';

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
  @Input() hero: Hero;
  shopName = 'Shop';
  shop: Shop;
  armorCategoryFilter: ArmorCategory;
  armorCategory = ArmorCategory;
  weaponCategoryFilter: WeaponCategory;
  weaponCategory = WeaponCategory;
  get hasEnoughCash() {
    return this.hero.inventory.cash1 >= this.totalCost;
  }
  constructor(
    private shopService: ShopService,
    private heroManagementService: HeroManagementService
  ) {
    this.shopService.itemBought.subscribe((itemBought: BuyOutput) => {
      this.updateItemOnShop(itemBought.shopItem);
    })
    this.shopService.getShop().subscribe(shop => {
      this.shopName = shop.name;
      this.shop = shop;
      shop.items.forEach(item => {
        item.quantityToBuy = 0;
      });
      this.initialItems.push(...shop.items.map(i => <ShopItem> {
        id: i.id,
        quantity: i.quantity,
        quantityToBuy: 0,
        value: i.value,
        name: i.name,
        item: i.item
      }));
      this.items.push(...shop.items);
      this.itemsShow.push(...shop.items);
    });
  }

  private updateItemOnShop(itemBought: ShopItem) {
    let itemOnShop = this.shop.items.find(i => i.id === itemBought.id);
    itemOnShop = itemBought;
    let itemOnList = this.items.find(i => i.id === itemBought.id);
    itemOnList = itemBought;
    let itemOnView = this.itemsShow.find(i => i.id === itemBought.id);
    itemOnView = itemBought;
  }

  ngOnInit(): void {
  }

  async buy() {
    if (this.hasEnoughCash) {
      this.heroManagementService.buyItems(this.shop, this.itemToBuy);
      this.itemToBuy = [];
      this.itemToBuyShow = [];
      this.totalCost = 0;
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

  updateFilter() {
    this.itemsShow = [];
    let weaponsToShow = [];
    let armorsToShow = [];
    if (this.filterWeapons) {
      weaponsToShow.push(...this.items.filter(item => item.item.itemTemplateType === ItemTemplateType.Weapon));
      if (this.weaponCategoryFilter === WeaponCategory.Light) {
        weaponsToShow = weaponsToShow.filter((item: ShopItem) => (item.item as WeaponModel).baseWeapon.category === WeaponCategory.Light);
      } else if (this.weaponCategoryFilter === WeaponCategory.Medium) {
        weaponsToShow = weaponsToShow.filter((item: ShopItem) => (item.item as WeaponModel).baseWeapon.category === WeaponCategory.Medium);
      } else if (this.weaponCategoryFilter === WeaponCategory.Heavy) {
        weaponsToShow = weaponsToShow.filter((item: ShopItem) => (item.item as WeaponModel).baseWeapon.category === WeaponCategory.Heavy);
      }
      this.itemsShow.push(...weaponsToShow);
    }
    if (this.filterArmors) {
      armorsToShow.push(...this.items.filter(item => item.item.itemTemplateType === ItemTemplateType.Armor));
      if (this.armorCategoryFilter === ArmorCategory.Light) {
        armorsToShow = armorsToShow.filter((item: ShopItem) => (item.item as ArmorModel).baseArmor.category === ArmorCategory.Light);
      } else if (this.armorCategoryFilter === ArmorCategory.Medium) {
        armorsToShow = armorsToShow.filter((item: ShopItem) => (item.item as ArmorModel).baseArmor.category === ArmorCategory.Medium);
      } else if (this.armorCategoryFilter === ArmorCategory.Heavy) {
        armorsToShow = armorsToShow.filter((item: ShopItem) => (item.item as ArmorModel).baseArmor.category === ArmorCategory.Heavy);
      }
      this.itemsShow.push(...armorsToShow);
    }
    if (!this.filterArmors && !this.filterWeapons) {
      this.itemsShow.push(...this.items);
    }
  }
}
