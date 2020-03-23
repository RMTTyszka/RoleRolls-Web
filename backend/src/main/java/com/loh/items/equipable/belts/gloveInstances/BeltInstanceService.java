package com.loh.items.equipable.belts.gloveInstances;

import com.loh.items.equipable.belts.baseBelts.DefaultBelts;
import com.loh.items.equipable.belts.gloveModels.BeltModel;
import com.loh.items.equipable.belts.gloveModels.BeltModelsRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.UUID;

@Service
public class BeltInstanceService {

    @Autowired
    BeltInstanceRepository beltInstanceRepository;
    @Autowired
    BeltModelsRepository beltModelsRepository;

    public BeltInstance instantiateGloves(UUID armorModelId, Integer level) {
        BeltModel beltModel = beltModelsRepository.findById(armorModelId).get();
        return instantiateGloves(beltModel, level);
    }
    public BeltInstance instantiateGloves(BeltModel beltModel, Integer level) {
        BeltInstance weapon = new BeltInstance(beltModel, level);
        return weapon;
    }

    public BeltInstance save(BeltInstance weaponInstance) {
        this.beltInstanceRepository.save(weaponInstance);
        return weaponInstance;
    }

    public BeltInstance instantiateNoneGlove() {
        BeltModel beltModel = beltModelsRepository.getByNameAndSystemDefaultTrue(DefaultBelts.NoGloves);
        BeltInstance gloves = instantiateGloves(beltModel, 1);
        beltInstanceRepository.save(gloves);
        return gloves;
    }
    public BeltInstance instantiateDummyGlove() {
        BeltModel beltModel = beltModelsRepository.getByNameAndSystemDefaultTrue(DefaultBelts.DummyGloves);
        BeltInstance gloves = instantiateGloves(beltModel, 1);
        beltInstanceRepository.save(gloves);
        return gloves;
    }
}
