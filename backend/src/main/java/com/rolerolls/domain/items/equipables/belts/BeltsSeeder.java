package com.rolerolls.domain.items.equipables.belts;

import com.rolerolls.domain.items.equipables.belts.base.BaseBelt;
import com.rolerolls.domain.items.equipables.belts.base.BaseBeltsRepository;
import com.rolerolls.domain.items.equipables.belts.base.DefaultBelts;
import com.rolerolls.domain.items.equipables.belts.models.BeltModel;
import com.rolerolls.domain.items.equipables.belts.models.BeltModelsRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class BeltsSeeder {

    @Autowired
    private BaseBeltsRepository baseBeltsRepository;
    @Autowired
    private BeltModelsRepository beltModelsRepository;

    public void seed() {
        for(String baseBeltName : DefaultBelts.getList()) {
            if (baseBeltsRepository.getByNameAndSystemDefaultTrue(baseBeltName) == null) {
                BaseBelt baseBelt = new BaseBelt(baseBeltName);
                baseBelt.setSystemDefault(true);
                baseBeltsRepository.save(baseBelt);
            }
        }
        if (baseBeltsRepository.getByNameAndSystemDefaultTrue(DefaultBelts.DummyBelt) == null) {
            BaseBelt baseBelt = new BaseBelt(DefaultBelts.DummyBelt);
            baseBelt.setSystemDefault(true);
            baseBeltsRepository.save(baseBelt);
        }
        if (baseBeltsRepository.getByNameAndSystemDefaultTrue(DefaultBelts.NoBelt) == null) {
            BaseBelt baseBelt = new BaseBelt(DefaultBelts.NoBelt);
            baseBelt.setSystemDefault(true);
            baseBeltsRepository.save(baseBelt);
        }
        for(String baseBeltName : DefaultBelts.getList()) {
            if (beltModelsRepository.getByNameAndSystemDefaultTrue("Common " + baseBeltName) == null) {
                BeltModel beltModel = new BeltModel(baseBeltName);
                beltModel.setSystemDefault(true);
                beltModelsRepository.save(beltModel);
            }
        }
        if (beltModelsRepository.getByNameAndSystemDefaultTrue(DefaultBelts.DummyBelt) == null) {
            BeltModel beltModel = new BeltModel(DefaultBelts.DummyBelt);
            beltModel.setSystemDefault(true);
            beltModelsRepository.save(beltModel);
        }
        if (beltModelsRepository.getByNameAndSystemDefaultTrue(DefaultBelts.NoBelt) == null) {
            BeltModel beltModel = new BeltModel(DefaultBelts.NoBelt);
            beltModel.setSystemDefault(true);
            beltModelsRepository.save(beltModel);
        }
    }
}
