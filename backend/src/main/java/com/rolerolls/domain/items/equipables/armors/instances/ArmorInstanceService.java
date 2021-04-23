package com.rolerolls.domain.items.equipables.armors.instances;

import com.rolerolls.domain.items.equipables.armors.DefaultArmors;
import com.rolerolls.domain.items.equipables.armors.templates.ArmorTemplate;
import com.rolerolls.domain.items.equipables.armors.templates.ArmorTemplateRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.UUID;

@Service
public class ArmorInstanceService {

    @Autowired
    ArmorInstanceRepository armorInstanceRepository;
    @Autowired
    ArmorTemplateRepository armorTemplateRepository;

    public ArmorInstance instantiateArmor(UUID armorModelId, Integer level) {
        ArmorTemplate armorTemplate = armorTemplateRepository.findById(armorModelId).get();
        return instantiateArmor(armorTemplate, level);
    }
    public ArmorInstance instantiateArmor(ArmorTemplate armorTemplate, Integer level) {
        ArmorInstance armor = new ArmorInstance(armorTemplate, level, 1);
        return armor;
    }
    public ArmorInstance instantiateNoneArmor() {
        ArmorTemplate armorTemplate = armorTemplateRepository.findByNameAndSystemDefaultTrue(DefaultArmors.NoneArmor);
        ArmorInstance armor = instantiateArmor(armorTemplate, 1);
        armor.setRemovable(false);
        armorInstanceRepository.save(armor);
        return armor;
    }
}
