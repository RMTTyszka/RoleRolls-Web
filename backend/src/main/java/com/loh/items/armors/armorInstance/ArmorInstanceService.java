package com.loh.items.armors.armorInstance;

import com.loh.items.armors.armorModel.ArmorModel;
import com.loh.items.armors.armorModel.ArmorModelRepository;
import com.loh.items.armors.armorTypes.ArmorType;
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
        ArmorModel armorModel = armorModelRepository.findTop1ArmorByBaseArmor_Category_ArmorType(ArmorType.None);
        ArmorInstance armor = instantiateArmor(armorModel, 1);
        armorInstanceRepository.save(armor);
        return armor;
    }
}
