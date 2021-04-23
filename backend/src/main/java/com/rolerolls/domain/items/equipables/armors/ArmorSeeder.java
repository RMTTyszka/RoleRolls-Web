package com.rolerolls.domain.items.equipables.armors;

import com.rolerolls.domain.items.equipables.armors.categories.ArmorCategory;
import com.rolerolls.domain.items.equipables.armors.instances.ArmorInstanceRepository;
import com.rolerolls.domain.items.equipables.armors.templates.ArmorTemplate;
import com.rolerolls.domain.items.equipables.armors.templates.ArmorTemplateRepository;
import com.rolerolls.domain.items.equipables.armors.base.BaseArmor;
import com.rolerolls.domain.items.equipables.armors.base.BaseArmorRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class ArmorSeeder {

    @Autowired
    private ArmorTemplateRepository armorTemplateRepository;
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
            ArmorTemplate dummyNoneArmor = new ArmorTemplate(DefaultArmors.dummyNoneArmor, baseArmor);
            dummyNoneArmor.setSystemDefault(true);
            armorTemplateRepository.save(dummyNoneArmor);
        }
        if (!baseArmorRepository.existsByNameAndSystemDefaultIsTrue(DefaultArmors.dummyLightArmor)){
            BaseArmor baseArmor = BaseArmor.DefaultBaseArmor(DefaultArmors.dummyLightArmor,  ArmorCategory.Light);
            baseArmor = baseArmorRepository.save(baseArmor);
            ArmorTemplate dummyNoneArmor = new ArmorTemplate(DefaultArmors.dummyLightArmor, baseArmor);
            dummyNoneArmor.setSystemDefault(true);
            armorTemplateRepository.save(dummyNoneArmor);
        }
        if (!baseArmorRepository.existsByNameAndSystemDefaultIsTrue(DefaultArmors.dummyMediumArmor)){
            BaseArmor baseArmor = BaseArmor.DefaultBaseArmor(DefaultArmors.dummyMediumArmor,  ArmorCategory.Medium);
            baseArmor = baseArmorRepository.save(baseArmor);
            ArmorTemplate dummyNoneArmor = new ArmorTemplate(DefaultArmors.dummyMediumArmor, baseArmor);
            dummyNoneArmor.setSystemDefault(true);
            armorTemplateRepository.save(dummyNoneArmor);
        }
        if (!baseArmorRepository.existsByNameAndSystemDefaultIsTrue(DefaultArmors.dummyHeavyArmor)){
            BaseArmor baseArmor = BaseArmor.DefaultBaseArmor(DefaultArmors.dummyHeavyArmor,  ArmorCategory.Heavy);
            baseArmor = baseArmorRepository.save(baseArmor);
            ArmorTemplate dummyNoneArmor = new ArmorTemplate(DefaultArmors.dummyHeavyArmor, baseArmor);
            dummyNoneArmor.setSystemDefault(true);
            armorTemplateRepository.save(dummyNoneArmor);
        }

        for (DefaultArmor armorName: DefaultArmors.lightArmors) {
            if(!armorTemplateRepository.existsByNameAndSystemDefaultIsTrue(armorName.name)) {
                BaseArmor baseArmor = baseArmorRepository.findByNameAndSystemDefaultTrue(armorName.name);
                ArmorTemplate armor = new ArmorTemplate("Common " + armorName.name, baseArmor);
                armor.setSystemDefault(true);
                armorTemplateRepository.save(armor);
            }
        }
        for (DefaultArmor armorName: DefaultArmors.mediumArmors) {
            if(!armorTemplateRepository.existsByNameAndSystemDefaultIsTrue(armorName.name)) {
                BaseArmor baseArmor = baseArmorRepository.findByNameAndSystemDefaultTrue(armorName.name);
                ArmorTemplate armor = new ArmorTemplate("Common " + armorName.name, baseArmor);
                armor.setSystemDefault(true);
                armorTemplateRepository.save(armor);
            }
        }
        for (DefaultArmor armorName: DefaultArmors.heavyArmors) {
            if(!armorTemplateRepository.existsByNameAndSystemDefaultIsTrue(armorName.name)) {
                BaseArmor baseArmor = baseArmorRepository.findByNameAndSystemDefaultTrue(armorName.name);
                ArmorTemplate armor = new ArmorTemplate("Common " + armorName.name, baseArmor);
                armor.setSystemDefault(true);
                armorTemplateRepository.save(armor);
            }
        }
        if(!armorTemplateRepository.existsByNameAndSystemDefaultIsTrue(DefaultArmors.NoneArmor)) {
            BaseArmor baseArmor = baseArmorRepository.findByNameAndSystemDefaultTrue(DefaultArmors.NoneArmor);
            ArmorTemplate armor = new ArmorTemplate(DefaultArmors.NoneArmor, baseArmor);
            armor.setSystemDefault(true);
            armorTemplateRepository.save(armor);
        }

    }
}
