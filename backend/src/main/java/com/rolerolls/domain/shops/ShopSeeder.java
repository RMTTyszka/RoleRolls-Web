package com.rolerolls.domain.shops;

import com.rolerolls.domain.items.equipables.armors.DefaultArmor;
import com.rolerolls.domain.items.equipables.armors.DefaultArmors;
import com.rolerolls.domain.items.equipables.armors.templates.ArmorTemplate;
import com.rolerolls.domain.items.equipables.armors.templates.ArmorTemplateRepository;
import com.rolerolls.domain.items.equipables.weapons.DefaultWeapon;
import com.rolerolls.domain.items.equipables.weapons.DefaultWeapons;
import com.rolerolls.domain.items.equipables.weapons.models.WeaponModel;
import com.rolerolls.domain.items.equipables.weapons.models.WeaponModelRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class ShopSeeder {

    @Autowired
    ShopRepository shopRepository;
    @Autowired
    ArmorTemplateRepository armorTemplateRepository;
    @Autowired
    WeaponModelRepository weaponModelRepository;
    @Autowired
    ShopArmorRepository shopArmorRepository;
    @Autowired
    ShopWeaponRepository shopWeaponRepository;

    public void seed() {
        Shop shop = shopRepository.findBySystemDefaultAndName(true, ShopTokens.HeroCreationShopName);
        if (shop == null) {
            shop = new Shop();
            shop.setName(ShopTokens.HeroCreationShopName);
            shop.setSystemDefault(true);
        }
        for (DefaultArmor armorName: DefaultArmors.lightArmors) {
            AddArmor(shop, armorName.name, 10);
        }
        for (DefaultArmor armorName: DefaultArmors.mediumArmors) {
            AddArmor(shop, armorName.name, 20);
        }
        for (DefaultArmor armorName: DefaultArmors.heavyArmors) {
            AddArmor(shop, armorName.name, 30);
        }
        for (DefaultWeapon weapon: DefaultWeapons.lightWeapons) {
            AddWeapon(shop, weapon.name, 10);
        }
        for (DefaultWeapon weapon: DefaultWeapons.mediumWeapons) {
            AddWeapon(shop, weapon.name, 20);
        }
        for (DefaultWeapon weapon: DefaultWeapons.heavyWeapons) {
            AddWeapon(shop, weapon.name, 30);
        }
        for (DefaultWeapon shield: DefaultWeapons.lightShields) {
            AddWeapon(shop, shield.name, 10);
        }
        for (DefaultWeapon shield: DefaultWeapons.mediumShields) {
            AddWeapon(shop, shield.name, 20);
        }
        for (DefaultWeapon shield: DefaultWeapons.heavyShields) {
            AddWeapon(shop, shield.name, 30);
        }

        shopRepository.save(shop);
    }

    private void AddArmor(Shop shop, String armorName, int value) {
        if (!shop.getItems().stream().filter(a -> a.getItem().getName().equals("Common " + armorName)).findFirst().isPresent()) {
            ArmorTemplate armorTemplate = armorTemplateRepository.findByNameAndSystemDefaultTrue("Common " + armorName);
            ShopArmor shopArmor = new ShopArmor(armorTemplate, 33, armorTemplate.getValue() != null ? armorTemplate.getValue()  : value, 1);
            shopArmorRepository.save(shopArmor);
            shop.addItem(shopArmor);
        }
    }
    private void AddWeapon(Shop shop, String weaponName, int value) {
        if (!shop.getItems().stream().filter(a -> a.getItem().getName().equals("Common " + weaponName)).findFirst().isPresent()) {
            WeaponModel weaponModel = weaponModelRepository.findByNameAndSystemDefaultTrue("Common " + weaponName);
            ShopWeapon shopWeapon = new ShopWeapon(weaponModel, 33, weaponModel.getValue() != null ? weaponModel.getValue()  : value, 1);
            shopWeaponRepository.save(shopWeapon);
            shop.addItem(shopWeapon);
        }
    }
}
