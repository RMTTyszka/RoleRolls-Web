package com.loh.domain.items.equipables.armors.instances;

import com.loh.domain.items.equipables.armors.DefaultArmors;
import com.loh.domain.items.equipables.armors.models.ArmorModel;
import com.loh.domain.items.equipables.armors.models.ArmorModelRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.UUID;

@Service
public class ArmorInstanceService {

    @Autowired
    ArmorInstanceRepository armorInstanceRepository;
    @Autowired
    ArmorModelRepository armorModelRepository;

    public ArmorInstance instantiateArmor(UUID armorModelId, Integer level) {
        ArmorModel armorModel = armorModelRepository.findById(armorModelId).get();
        return instantiateArmor(armorModel, level);
    }
    public ArmorInstance instantiateArmor(ArmorModel armorModel, Integer level) {
        ArmorInstance armor = new ArmorInstance(armorModel, level, 1);
        return armor;
    }
    public ArmorInstance instantiateNoneArmor() {
        ArmorModel armorModel = armorModelRepository.findByNameAndSystemDefaultTrue(DefaultArmors.NoneArmor);
        ArmorInstance armor = instantiateArmor(armorModel, 1);
        armor.setRemovable(false);
        armorInstanceRepository.save(armor);
        return armor;
    }
}
