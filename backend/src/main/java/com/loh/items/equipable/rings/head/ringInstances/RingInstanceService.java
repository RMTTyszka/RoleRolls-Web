package com.loh.items.equipable.rings.head.ringInstances;

import com.loh.items.equipable.rings.head.DefaultRings;
import com.loh.items.equipable.rings.head.ringModels.RingModel;
import com.loh.items.equipable.rings.head.ringModels.RingModelRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.UUID;

@Service
public class RingInstanceService {

    @Autowired
    RingInstanceRepository ringInstanceRepository;
    @Autowired
    RingModelRepository ringModelRepository;

    public RingInstance instantiate(UUID armorModelId, Integer level) {
        RingModel ringModel = ringModelRepository.findById(armorModelId).get();
        return instantiate(ringModel, level);
    }
    public RingInstance instantiate(RingModel ringModel, Integer level) {
        RingInstance weapon = new RingInstance(ringModel, level);
        return weapon;
    }

    public RingInstance save(RingInstance weaponInstance) {
        this.ringInstanceRepository.save(weaponInstance);
        return weaponInstance;
    }

    public RingInstance instantiateNone() {
        RingModel ringModel = ringModelRepository.getByNameAndSystemDefaultTrue(DefaultRings.NoRing);
        RingInstance gloves = instantiate(ringModel, 1);
        gloves.setRemovable(false);
        ringInstanceRepository.save(gloves);
        return gloves;
    }
    public RingInstance instantiateDummy() {
        RingModel ringModel = ringModelRepository.getByNameAndSystemDefaultTrue(DefaultRings.DummyRing);
        RingInstance gloves = instantiate(ringModel, 1);
        ringInstanceRepository.save(gloves);
        return gloves;
    }
}
