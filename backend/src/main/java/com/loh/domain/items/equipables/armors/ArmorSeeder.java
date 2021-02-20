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

        for (DefaultArmor armorName: DefaultArmors.lightArmors) {
            if (!baseArmorRepository.existsByNameAndSystemDefaultIsTrue(armorName.name)) {
                BaseArmor armor = BaseArmor.DefaultBaseArmor(armorName.name, ArmorCategory.Light);
                baseArmorRepository.save(armor);
            }
        }
        for (DefaultArmor armorName: DefaultArmors.mediumArmors) {
            if (!baseArmorRepository.existsByNameAndSystemDefaultIsTrue(armorName.name)) {
                BaseArmor armor = BaseArmor.DefaultBaseArmor(armorName.name, ArmorCategory.Medium);
                baseArmorRepository.save(armor);
            }
        }
        for (DefaultArmor armorName: DefaultArmors.heavyArmors) {
            if (!baseArmorRepository.existsByNameAndSystemDefaultIsTrue(armorName.name)) {
                BaseArmor armor = BaseArmor.DefaultBaseArmor(armorName.name, ArmorCategory.Heavy);
                baseArmorRepository.save(armor);
            }
        }
        if (!baseArmorRepository.existsByNameAndSystemDefaultIsTrue(DefaultArmors.NoneArmor)){
            BaseArmor baseArmor = BaseArmor.DefaultBaseArmor(DefaultArmors.NoneArmor,  ArmorCategory.None);
            baseArmorRepository.save(baseArmor);
        }
        if (!baseArmorRepository.existsByNameAndSystemDefaultIsTrue(DefaultArmors.dummyNoneArmor)){
            BaseArmor baseArmor = BaseArmor.DefaultBaseArmor(DefaultArmors.dummyNoneArmor,  ArmorCategory.None);
            baseArmor = baseArmorRepository.save(baseArmor);
            ArmorModel dummyNoneArmor = new ArmorModel(DefaultArmors.dummyNoneArmor, baseArmor);
            dummyNoneArmor.setSystemDefault(true);
            armorModelRepository.save(dummyNoneArmor);
        }
        if (!baseArmorRepository.existsByNameAndSystemDefaultIsTrue(DefaultArmors.dummyLightArmor)){
            BaseArmor baseArmor = BaseArmor.DefaultBaseArmor(DefaultArmors.dummyLightArmor,  ArmorCategory.Light);
            baseArmor = baseArmorRepository.save(baseArmor);
            ArmorModel dummyNoneArmor = new ArmorModel(DefaultArmors.dummyLightArmor, baseArmor);
            dummyNoneArmor.setSystemDefault(true);
            armorModelRepository.save(dummyNoneArmor);
        }
        if (!baseArmorRepository.existsByNameAndSystemDefaultIsTrue(DefaultArmors.dummyMediumArmor)){
            BaseArmor baseArmor = BaseArmor.DefaultBaseArmor(DefaultArmors.dummyMediumArmor,  ArmorCategory.Medium);
            baseArmor = baseArmorRepository.save(baseArmor);
            ArmorModel dummyNoneArmor = new ArmorModel(DefaultArmors.dummyMediumArmor, baseArmor);
            dummyNoneArmor.setSystemDefault(true);
            armorModelRepository.save(dummyNoneArmor);
        }
        if (!baseArmorRepository.existsByNameAndSystemDefaultIsTrue(DefaultArmors.dummyHeavyArmor)){
            BaseArmor baseArmor = BaseArmor.DefaultBaseArmor(DefaultArmors.dummyHeavyArmor,  ArmorCategory.Heavy);
            baseArmor = baseArmorRepository.save(baseArmor);
            ArmorModel dummyNoneArmor = new ArmorModel(DefaultArmors.dummyHeavyArmor, baseArmor);
            dummyNoneArmor.setSystemDefault(true);
            armorModelRepository.save(dummyNoneArmor);
        }

        for (DefaultArmor armorName: DefaultArmors.lightArmors) {
            if(!armorModelRepository.existsByNameAndSystemDefaultIsTrue(armorName.name)) {
                BaseArmor baseArmor = baseArmorRepository.findByNameAndSystemDefaultTrue(armorName.name);
                ArmorModel armor = new ArmorModel("Common " + armorName.name, baseArmor);
                armor.setSystemDefault(true);
                armorModelRepository.save(armor);
            }
        }
        for (DefaultArmor armorName: DefaultArmors.mediumArmors) {
            if(!armorModelRepository.existsByNameAndSystemDefaultIsTrue(armorName.name)) {
                BaseArmor baseArmor = baseArmorRepository.findByNameAndSystemDefaultTrue(armorName.name);
                ArmorModel armor = new ArmorModel("Common " + armorName.name, baseArmor);
                armor.setSystemDefault(true);
                armorModelRepository.save(armor);
            }
        }
        for (DefaultArmor armorName: DefaultArmors.heavyArmors) {
            if(!armorModelRepository.existsByNameAndSystemDefaultIsTrue(armorName.name)) {
                BaseArmor baseArmor = baseArmorRepository.findByNameAndSystemDefaultTrue(armorName.name);
                ArmorModel armor = new ArmorModel("Common " + armorName.name, baseArmor);
                armor.setSystemDefault(true);
                armorModelRepository.save(armor);
            }
        }
        if(!armorModelRepository.existsByNameAndSystemDefaultIsTrue(DefaultArmors.NoneArmor)) {
            BaseArmor baseArmor = baseArmorRepository.findByNameAndSystemDefaultTrue(DefaultArmors.NoneArmor);
            ArmorModel armor = new ArmorModel("Common " + DefaultArmors.NoneArmor, baseArmor);
            armor.setSystemDefault(true);
            armorModelRepository.save(armor);
        }

    }
}
