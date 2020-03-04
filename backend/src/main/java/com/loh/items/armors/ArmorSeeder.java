package com.loh.items.armors;

import com.loh.items.armors.armorCategories.ArmorCategory;
import com.loh.items.armors.armorCategories.ArmorCategoryRepository;
import com.loh.items.armors.armorInstance.ArmorInstanceRepository;
import com.loh.items.armors.armorModel.ArmorModel;
import com.loh.items.armors.armorModel.ArmorModelRepository;
import com.loh.items.armors.armorTypes.ArmorType;
import com.loh.items.armors.baseArmor.BaseArmor;
import com.loh.items.armors.baseArmor.BaseArmorRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class ArmorSeeder {

    @Autowired
    private ArmorModelRepository armorModelRepository;
    @Autowired
    private ArmorInstanceRepository armorInstanceRepository;
    @Autowired
    private ArmorCategoryRepository armorCategoryRepository;
    @Autowired
    private BaseArmorRepository baseArmorRepository;

    public void seed() {

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
            for (String armorName: DefaultArmors.lightArmors) {
                BaseArmor armor = BaseArmor.DefaultBaseArmor(armorName, light);
                baseArmorRepository.save(armor);
            }
            for (String armorName: DefaultArmors.mediumArmors) {
                BaseArmor armor = BaseArmor.DefaultBaseArmor(armorName, medium);
                baseArmorRepository.save(armor);
            }
            for (String armorName: DefaultArmors.heavyArmors) {
                BaseArmor armor = BaseArmor.DefaultBaseArmor(armorName, heavy);
                baseArmorRepository.save(armor);
            }

            BaseArmor empty = BaseArmor.DefaultBaseArmor("None Armor", noneArmor);
            BaseArmor dummyHeavyArmor = BaseArmor.DefaultBaseArmor(DefaultArmors.dummyHeavyArmor, heavy);
            BaseArmor dummyMediumArmor = BaseArmor.DefaultBaseArmor(DefaultArmors.dummyMediumArmor, medium);
            BaseArmor dummyLightArmor = BaseArmor.DefaultBaseArmor(DefaultArmors.dummyLightArmor, light);
            BaseArmor dummyNoneArmor = BaseArmor.DefaultBaseArmor(DefaultArmors.dummyNoneArmor, noneArmor);
            baseArmorRepository.save(empty);

            baseArmorRepository.save(dummyHeavyArmor);
            baseArmorRepository.save(dummyMediumArmor);
            baseArmorRepository.save(dummyLightArmor);
            baseArmorRepository.save(dummyNoneArmor);
        }
        if (armorModelRepository.count() <= 0){
            BaseArmor baseNoneArmor = baseArmorRepository.findByNameAndSystemDefaultTrue("None Armor");

            for (String armorName: DefaultArmors.lightArmors) {
                BaseArmor baseLightArmor = baseArmorRepository.findByNameAndSystemDefaultTrue(armorName);
                ArmorModel armor = new ArmorModel("Common " + armorName, baseLightArmor);
                armor.setSystemDefault(true);
                armorModelRepository.save(armor);
            }
            for (String armorName: DefaultArmors.mediumArmors) {
                BaseArmor baseMediumArmor = baseArmorRepository.findByNameAndSystemDefaultTrue(armorName);
                ArmorModel armor = new ArmorModel("Common " + armorName, baseMediumArmor);
                armor.setSystemDefault(true);
                armorModelRepository.save(armor);
            }
            for (String armorName: DefaultArmors.heavyArmors) {
                BaseArmor baseHeavyArmor = baseArmorRepository.findByNameAndSystemDefaultTrue(armorName);
                ArmorModel armor = new ArmorModel("Common " + armorName, baseHeavyArmor);
                armor.setSystemDefault(true);
                armorModelRepository.save(armor);
            }

            ArmorModel noneArmor = new ArmorModel("None Armor", baseNoneArmor);
            noneArmor.setSystemDefault(true);
            armorModelRepository.save(noneArmor);

            BaseArmor baseDummyLightArmor = baseArmorRepository.findByNameAndSystemDefaultTrue(DefaultArmors.dummyLightArmor);
            BaseArmor baseDummyMediumArmor = baseArmorRepository.findByNameAndSystemDefaultTrue(DefaultArmors.dummyMediumArmor);
            BaseArmor baseDummyHeavyArmor = baseArmorRepository.findByNameAndSystemDefaultTrue(DefaultArmors.dummyHeavyArmor);
            BaseArmor baseDummyNoneArmor = baseArmorRepository.findByNameAndSystemDefaultTrue(DefaultArmors.dummyNoneArmor);
            ArmorModel dummyNoneArmor = new ArmorModel(DefaultArmors.dummyNoneArmor, baseDummyNoneArmor);
            ArmorModel dummyLightArmor = new ArmorModel(DefaultArmors.dummyLightArmor, baseDummyLightArmor);
            ArmorModel dummyMediumArmor = new ArmorModel(DefaultArmors.dummyMediumArmor, baseDummyMediumArmor);
            ArmorModel dummyHeavyArmor = new ArmorModel(DefaultArmors.dummyHeavyArmor, baseDummyHeavyArmor);
            dummyNoneArmor.setSystemDefault(true);
            dummyLightArmor.setSystemDefault(true);
            dummyMediumArmor.setSystemDefault(true);
            dummyHeavyArmor.setSystemDefault(true);

            armorModelRepository.save(dummyNoneArmor);
            armorModelRepository.save(dummyLightArmor);
            armorModelRepository.save(dummyMediumArmor);
            armorModelRepository.save(dummyHeavyArmor);
        }

    }
}
