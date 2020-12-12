package com.loh.domain.items.equipables.gloves.instances;

import com.loh.domain.items.equipables.gloves.base.DefaultGloves;
import com.loh.domain.items.equipables.gloves.models.GloveModel;
import com.loh.domain.items.equipables.gloves.models.GloveModelsRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.UUID;

@Service
public class GloveInstanceService {

    @Autowired
    GloveInstanceRepository gloveInstanceRepository;
    @Autowired
    GloveModelsRepository gloveModelsRepository;

    public GloveInstance instantiateGloves(UUID armorModelId, Integer level) {
        GloveModel gloveModel = gloveModelsRepository.findById(armorModelId).get();
        return instantiateGloves(gloveModel, level);
    }
    public GloveInstance instantiateGloves(GloveModel gloveModel, Integer level) {
        GloveInstance weapon = new GloveInstance(gloveModel, level);
        return weapon;
    }

    public GloveInstance save(GloveInstance weaponInstance) {
        this.gloveInstanceRepository.save(weaponInstance);
        return weaponInstance;
    }

    public GloveInstance instantiateNoneGlove() {
        GloveModel gloveModel = gloveModelsRepository.getByNameAndSystemDefaultTrue(DefaultGloves.NoGloves);
        GloveInstance gloves = instantiateGloves(gloveModel, 1);
        gloves.setRemovable(false);
        gloveInstanceRepository.save(gloves);
        return gloves;
    }
    public GloveInstance instantiateDummyGlove() {
        GloveModel gloveModel = gloveModelsRepository.getByNameAndSystemDefaultTrue(DefaultGloves.DummyGloves);
        GloveInstance gloves = instantiateGloves(gloveModel, 1);
        gloveInstanceRepository.save(gloves);
        return gloves;
    }
}
