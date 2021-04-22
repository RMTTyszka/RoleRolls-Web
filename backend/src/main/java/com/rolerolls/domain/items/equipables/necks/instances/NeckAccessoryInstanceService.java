package com.rolerolls.domain.items.equipables.necks.instances;

import com.rolerolls.domain.items.equipables.necks.base.DefaultNeckAcessories;
import com.rolerolls.domain.items.equipables.necks.models.NeckAcessoryModel;
import com.rolerolls.domain.items.equipables.necks.models.NeckAccessoryModelRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.UUID;

@Service
public class NeckAccessoryInstanceService {

    @Autowired
    NeckAccessoryInstanceRepository neckAccessoryInstanceRepository;
    @Autowired
    NeckAccessoryModelRepository neckAccessoryModelRepository;

    public NeckAccessoryInstance instantiate(UUID armorModelId, Integer level) {
        NeckAcessoryModel neckAcessoryModel = neckAccessoryModelRepository.findById(armorModelId).get();
        return instantiate(neckAcessoryModel, level);
    }
    public NeckAccessoryInstance instantiate(NeckAcessoryModel neckAcessoryModel, Integer level) {
        NeckAccessoryInstance weapon = new NeckAccessoryInstance(neckAcessoryModel, level);
        return weapon;
    }

    public NeckAccessoryInstance save(NeckAccessoryInstance weaponInstance) {
        this.neckAccessoryInstanceRepository.save(weaponInstance);
        return weaponInstance;
    }

    public NeckAccessoryInstance instantiateNone() {
        NeckAcessoryModel neckAcessoryModel = neckAccessoryModelRepository.getByNameAndSystemDefaultTrue(DefaultNeckAcessories.NoNeckAcessory);
        NeckAccessoryInstance gloves = instantiate(neckAcessoryModel, 1);
        gloves.setRemovable(false);
        neckAccessoryInstanceRepository.save(gloves);
        return gloves;
    }
    public NeckAccessoryInstance instantiateDummy() {
        NeckAcessoryModel neckAcessoryModel = neckAccessoryModelRepository.getByNameAndSystemDefaultTrue(DefaultNeckAcessories.DummyNeckAcessory);
        NeckAccessoryInstance gloves = instantiate(neckAcessoryModel, 1);
        neckAccessoryInstanceRepository.save(gloves);
        return gloves;
    }
}
