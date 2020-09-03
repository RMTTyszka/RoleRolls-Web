package com.loh.items.equipable.head.headpieceInstances;

import com.loh.items.equipable.head.baseHeadpieces.DefaultHeadpieces;
import com.loh.items.equipable.head.headpieceModel.HeadpieceModel;
import com.loh.items.equipable.head.headpieceModel.HeadpieceModelRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.UUID;

@Service
public class HeadpieceInstanceService {

    @Autowired
    HeadpieceInstanceRepository headpieceInstanceRepository;
    @Autowired
    HeadpieceModelRepository headpieceModelRepository;

    public HeadpieceInstance instantiate(UUID armorModelId, Integer level) {
        HeadpieceModel headpieceModel = headpieceModelRepository.findById(armorModelId).get();
        return instantiate(headpieceModel, level);
    }
    public HeadpieceInstance instantiate(HeadpieceModel headpieceModel, Integer level) {
        HeadpieceInstance weapon = new HeadpieceInstance(headpieceModel, level);
        return weapon;
    }

    public HeadpieceInstance save(HeadpieceInstance weaponInstance) {
        this.headpieceInstanceRepository.save(weaponInstance);
        return weaponInstance;
    }

    public HeadpieceInstance instantiateNone() {
        HeadpieceModel headpieceModel = headpieceModelRepository.getByNameAndSystemDefaultTrue(DefaultHeadpieces.NoHeadpiece);
        HeadpieceInstance gloves = instantiate(headpieceModel, 1);
        gloves.setRemovable(false);
        headpieceInstanceRepository.save(gloves);
        return gloves;
    }
    public HeadpieceInstance instantiateDummy() {
        HeadpieceModel headpieceModel = headpieceModelRepository.getByNameAndSystemDefaultTrue(DefaultHeadpieces.DummyHeadpiece);
        HeadpieceInstance gloves = instantiate(headpieceModel, 1);
        headpieceInstanceRepository.save(gloves);
        return gloves;
    }
}
