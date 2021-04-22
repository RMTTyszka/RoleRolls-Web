package com.rolerolls.domain.items.equipables.belts.instances;

import com.rolerolls.domain.items.equipables.belts.base.DefaultBelts;
import com.rolerolls.domain.items.equipables.belts.models.BeltModel;
import com.rolerolls.domain.items.equipables.belts.models.BeltModelsRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.UUID;

@Service
public class BeltInstanceService {

    @Autowired
    BeltInstanceRepository beltInstanceRepository;
    @Autowired
    BeltModelsRepository beltModelsRepository;

    public BeltInstance instantiateBelt(UUID armorModelId, Integer level) {
        BeltModel beltModel = beltModelsRepository.findById(armorModelId).get();
        return instantiateBelt(beltModel, level);
    }
    public BeltInstance instantiateBelt(BeltModel beltModel, Integer level) {
        BeltInstance weapon = new BeltInstance(beltModel, level);
        return weapon;
    }

    public BeltInstance save(BeltInstance weaponInstance) {
        this.beltInstanceRepository.save(weaponInstance);
        return weaponInstance;
    }

    public BeltInstance instantiateNoneBelt() {
        BeltModel beltModel = beltModelsRepository.getByNameAndSystemDefaultTrue(DefaultBelts.NoBelt);
        BeltInstance gloves = instantiateBelt(beltModel, 1);
        gloves.setRemovable(false);
        beltInstanceRepository.save(gloves);
        return gloves;
    }
    public BeltInstance instantiateDummyBelt() {
        BeltModel beltModel = beltModelsRepository.getByNameAndSystemDefaultTrue(DefaultBelts.DummyBelt);
        BeltInstance gloves = instantiateBelt(beltModel, 1);
        beltInstanceRepository.save(gloves);
        return gloves;
    }
}
