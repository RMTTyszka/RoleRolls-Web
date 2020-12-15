package com.loh.domain.items.equipables.armors;

import com.loh.domain.items.equipables.armors.categories.ArmorCategory;
import com.loh.domain.items.equipables.armors.instances.ArmorInstanceRepository;
import com.loh.domain.items.equipables.armors.models.ArmorModel;
import com.loh.domain.items.equipables.armors.models.ArmorModelRepository;
import com.loh.domain.items.equipables.armors.base.BaseArmor;
import com.loh.domain.items.equipables.armors.base.BaseArmorRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class ArmorSeeder {

    @Autowired
    private ArmorModelRepository armorModelRepository;
    @Autowired
    private ArmorInstanceRepository armorInstanceRepository;
    @Autowired
    private BaseArmorRepository baseArmorRepository;

    public void seed() {

        if (baseArmorRepository.count() <= 0){
            for (String armorName: DefaultArmors.lightArmors) {
                BaseArmor armor = BaseArmor.DefaultBaseArmor(armorName, ArmorCategory.Light);
                baseArmorRepository.save(armor);
            }
            for (String armorName: DefaultArmors.mediumArmors) {
                BaseArmor armor = BaseArmor.DefaultBaseArmor(armorName, ArmorCategory.Medium);
                baseArmorRepository.save(armor);
            }
            for (String armorName: DefaultArmors.heavyArmors) {
                BaseArmor armor = BaseArmor.DefaultBaseArmor(armorName, ArmorCategory.Heavy);
                baseArmorRepository.save(armor);
            }

            BaseArmor empty = BaseArmor.DefaultBaseArmor("None Armor",  ArmorCategory.None);
            BaseArmor dummyNoneArmor = BaseArmor.DefaultBaseArmor(DefaultArmors.dummyNoneArmor,  ArmorCategory.None);
            BaseArmor dummyLightArmor = BaseArmor.DefaultBaseArmor(DefaultArmors.dummyLightArmor, ArmorCategory.Light);
            BaseArmor dummyMediumArmor = BaseArmor.DefaultBaseArmor(DefaultArmors.dummyMediumArmor, ArmorCategory.Medium);
            BaseArmor dummyHeavyArmor = BaseArmor.DefaultBaseArmor(DefaultArmors.dummyHeavyArmor, ArmorCategory.Heavy);
            baseArmorRepository.save(empty);

            baseArmorRepository.save(dummyHeavyArmor);
            baseArmorRepository.save(dummyMediumArmor);
            baseArmorRepository.save(dummyLightArmor);
            baseArmorRepository.save(dummyNoneArmor);
        }
        if (armorModelRepository.count() <= 0){
            BaseArmor baseNoneArmor = baseArmorRepository.findByNameAndSystemDefaultTrue(DefaultArmors.NoneArmor);

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

            ArmorModel noneArmor = new ArmorModel(DefaultArmors.NoneArmor, baseNoneArmor);
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
