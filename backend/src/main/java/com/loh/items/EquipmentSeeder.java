package com.loh.items;

import com.loh.creatures.Attributes;
import com.loh.items.armors.ArmorInstance;
import com.loh.items.armors.ArmorRepository;
import com.loh.items.armors.armorCategories.ArmorCategory;
import com.loh.items.armors.armorCategories.ArmorCategoryRepository;
import com.loh.items.armors.armorModel.ArmorModel;
import com.loh.items.armors.armorModel.ArmorModelRepository;
import com.loh.items.armors.armorTypes.ArmorType;
import com.loh.items.armors.baseArmor.BaseArmor;
import com.loh.items.armors.baseArmor.BaseArmorRepository;
import com.loh.items.weapons.baseWeapon.BaseWeapon;
import com.loh.items.weapons.baseWeapon.BaseWeaponRepository;
import com.loh.items.weapons.weaponCategory.WeaponCategory;
import com.loh.items.weapons.weaponCategory.WeaponCategoryRepository;
import com.loh.items.weapons.weaponCategory.WeaponHandleType;
import com.loh.items.weapons.weaponCategory.WeaponType;
import com.loh.items.weapons.weaponInstance.WeaponInstance;
import com.loh.items.weapons.weaponInstance.WeaponInstanceRepository;
import com.loh.items.weapons.weaponModel.WeaponModel;
import com.loh.items.weapons.weaponModel.WeaponModelRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.event.ContextRefreshedEvent;
import org.springframework.context.event.EventListener;
import org.springframework.stereotype.Component;
@Component
public class EquipmentSeeder {

	
	@Autowired
	private ArmorModelRepository armorModelRepository;
	@Autowired
	private ArmorRepository armorRepository;
	@Autowired
	private ArmorCategoryRepository armorCategoryRepository;
	@Autowired
	private BaseArmorRepository baseArmorRepository;
	@Autowired
	private WeaponInstanceRepository weaponInstanceRepository;
	@Autowired
	private WeaponModelRepository weaponModelRepository;
	@Autowired
	private WeaponCategoryRepository weaponCategoryRepository;
	@Autowired
	private BaseWeaponRepository baseWeaponRepository;
	
	@EventListener
	public void seed(ContextRefreshedEvent event) {
		CreateArmors();
	}
	
	private void CreateArmors()
	{
/*		if (armorRepo.count() <= 0) {
			ArmorModel newArmor = new ArmorModel();
			newArmor.name = ("Full plate");
			armorRepo.save(newArmor);
		}*/
		if (armorCategoryRepository.count() <= 0) {
			ArmorCategory lightArmor = new ArmorCategory(ArmorType.Light, 1, 1, 1);
			ArmorCategory mediumArmor = new ArmorCategory(ArmorType.Medium, 2, 0, 3);
			ArmorCategory heavyArmor = new ArmorCategory(ArmorType.Heavy, 3, -1, 4);
			ArmorCategory noArmor = new ArmorCategory(ArmorType.None, 0, 1, 0);
			armorCategoryRepository.save(lightArmor);
			armorCategoryRepository.save(mediumArmor);
			armorCategoryRepository.save(heavyArmor);
			armorCategoryRepository.save(noArmor);
		}
		if (baseArmorRepository.count() <= 0){
			ArmorCategory heavy = armorCategoryRepository.findArmorCategoryByArmorType(ArmorType.Heavy);
			ArmorCategory medium = armorCategoryRepository.findArmorCategoryByArmorType(ArmorType.Medium);
			ArmorCategory light = armorCategoryRepository.findArmorCategoryByArmorType(ArmorType.Light);
			ArmorCategory noneArmor = armorCategoryRepository.findArmorCategoryByArmorType(ArmorType.None);
			BaseArmor fullPlate = BaseArmor.DefaultBaseArmor("Full Plate", heavy);
			BaseArmor chainMail = BaseArmor.DefaultBaseArmor("Chain Mail", medium);
			BaseArmor leatherArmor = BaseArmor.DefaultBaseArmor("Leather Armor", light);
			BaseArmor empty = BaseArmor.DefaultBaseArmor("None Armor", noneArmor);
			baseArmorRepository.save(fullPlate);
			baseArmorRepository.save(chainMail);
			baseArmorRepository.save(leatherArmor);
			baseArmorRepository.save(empty);
		}
		if (armorModelRepository.count() <= 0){
			BaseArmor baseNoneArmor = baseArmorRepository.findByCategory_ArmorType(ArmorType.None);
			ArmorModel noneArmor = new ArmorModel();
			noneArmor.setStatic(true);
			noneArmor.setBaseArmor(baseNoneArmor);
			noneArmor.setName("None Armor");
			armorModelRepository.save(noneArmor);
		}
		if (armorRepository.findByArmorModel_BaseArmor_Category_ArmorType(ArmorType.None) == null) {
			ArmorInstance armor = new ArmorInstance();
			ArmorModel noArmor = armorModelRepository.findArmorByBaseArmor_Category_ArmorType(ArmorType.None);
			armor.setArmorModel(noArmor);
			armor.setName(noArmor.name);
			armorRepository.save(armor);
		}



		if (weaponCategoryRepository.count() <= 0) {
			WeaponCategory lightWeapon = new WeaponCategory(WeaponType.Light, WeaponHandleType.OneHanded);
			WeaponCategory mediumWeapon = new WeaponCategory(WeaponType.Medium, WeaponHandleType.OneHanded);
			WeaponCategory heavyWeapon = new WeaponCategory(WeaponType.Heavy, WeaponHandleType.TwoHanded);
			WeaponCategory noWeapon = new WeaponCategory(WeaponType.None, WeaponHandleType.OneHanded);
			WeaponCategory shield = new WeaponCategory(WeaponType.Shield, WeaponHandleType.OneHanded);
			weaponCategoryRepository.save(lightWeapon);
			weaponCategoryRepository.save(mediumWeapon);
			weaponCategoryRepository.save(heavyWeapon);
			weaponCategoryRepository.save(noWeapon);
			weaponCategoryRepository.save(shield);
		}
		if (baseWeaponRepository.count() <= 0){
			WeaponCategory heavy = weaponCategoryRepository.findWeaponCategoryByWeaponType(WeaponType.Heavy);
			WeaponCategory medium = weaponCategoryRepository.findWeaponCategoryByWeaponType(WeaponType.Medium);
			WeaponCategory light = weaponCategoryRepository.findWeaponCategoryByWeaponType(WeaponType.Light);
			WeaponCategory noneWeapon = weaponCategoryRepository.findWeaponCategoryByWeaponType(WeaponType.None);
			BaseWeapon fullPlate = BaseWeapon.DefaultBaseWeapon("Great Axe", heavy, Attributes.Strength, Attributes.Strength);
			BaseWeapon chainMail = BaseWeapon.DefaultBaseWeapon("Long Sword", medium, Attributes.Strength, Attributes.Strength);
			BaseWeapon leatherArmor = BaseWeapon.DefaultBaseWeapon("Dagger", light, Attributes.Agility, Attributes.Agility);
			BaseWeapon empty = BaseWeapon.DefaultBaseWeapon("Bare hands", noneWeapon, Attributes.Strength, Attributes.Strength);
			baseWeaponRepository.save(fullPlate);
			baseWeaponRepository.save(chainMail);
			baseWeaponRepository.save(leatherArmor);
			baseWeaponRepository.save(empty);
		}
		if (weaponModelRepository.count() <= 0){
			BaseWeapon baseNoneWeapon = baseWeaponRepository.findByCategory_WeaponType(WeaponType.None);
			WeaponModel noneModel = new WeaponModel();
			noneModel.setStatic(true);
			noneModel.setBaseWeapon(baseNoneWeapon);
			noneModel.setName("Base Hands");
			weaponModelRepository.save(noneModel);
		}
		if (weaponInstanceRepository.findByWeaponModel_BaseWeapon_Category_WeaponType(WeaponType.None) == null) {
			WeaponInstance weapon = new WeaponInstance();
			WeaponModel noWeapon = weaponModelRepository.findArmorByBaseWeapon_Category_WeaponType(WeaponType.None);
			weapon.setWeaponModel(noWeapon);
			weapon.setName(noWeapon.name);
			weaponInstanceRepository.save(weapon);
		}


	}
}
