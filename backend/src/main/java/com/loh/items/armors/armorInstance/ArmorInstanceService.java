package com.loh.items.armors.armorInstance;

import com.loh.items.armors.armorModel.ArmorModel;
import com.loh.items.armors.armorModel.ArmorModelRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.UUID;

@Service
public class ArmorInstanceService {

    @Autowired
    ArmorInstanceRepository armorInstanceRepository;
    @Autowired
    ArmorModelRepository armorModelRepository;

    public ArmorInstance InstantiateArmor(UUID armorModelId, Integer level) {
        ArmorModel armorModel = armorModelRepository.findById(armorModelId).get();
        return InstantiateArmor(armorModel, level);
    }
    public ArmorInstance InstantiateArmor(ArmorModel armorModel, Integer level) {
        ArmorInstance armor = new ArmorInstance(armorModel, level);
        return armor;
    }
}
