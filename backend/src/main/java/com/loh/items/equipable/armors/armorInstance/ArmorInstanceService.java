package com.loh.items.equipable.armors.armorInstance;

import com.loh.items.equipable.armors.DefaultArmors;
import com.loh.items.equipable.armors.armorModel.ArmorModel;
import com.loh.items.equipable.armors.armorModel.ArmorModelRepository;
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
        ArmorInstance armor = new ArmorInstance(armorModel, level);
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
