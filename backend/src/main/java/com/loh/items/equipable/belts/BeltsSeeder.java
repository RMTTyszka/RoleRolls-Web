package com.loh.items.equipable.belts;

import com.loh.items.equipable.belts.baseBelts.BaseBelt;
import com.loh.items.equipable.belts.baseBelts.BaseBeltsRepository;
import com.loh.items.equipable.belts.baseBelts.DefaultBelts;
import com.loh.items.equipable.belts.gloveModels.BeltModel;
import com.loh.items.equipable.belts.gloveModels.BeltModelsRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class BeltsSeeder {

    @Autowired
    private BaseBeltsRepository baseBeltsRepository;
    @Autowired
    private BeltModelsRepository beltModelsRepository;

    public void seed() {
        for(String baseGloveName : DefaultBelts.getList()) {
            if (baseBeltsRepository.getByNameAndSystemDefaultTrue(baseGloveName) == null) {
                BaseBelt baseBelt = new BaseBelt(baseGloveName);
                baseBelt.setSystemDefault(true);
                baseBeltsRepository.save(baseBelt);
            }
        }
        if (baseBeltsRepository.getByNameAndSystemDefaultTrue(DefaultBelts.DummyGloves) == null) {
            BaseBelt baseBelt = new BaseBelt(DefaultBelts.DummyGloves);
            baseBelt.setSystemDefault(true);
            baseBeltsRepository.save(baseBelt);
        }
        if (baseBeltsRepository.getByNameAndSystemDefaultTrue(DefaultBelts.NoGloves) == null) {
            BaseBelt baseBelt = new BaseBelt(DefaultBelts.NoGloves);
            baseBelt.setSystemDefault(true);
            baseBeltsRepository.save(baseBelt);
        }
        for(String baseGloveName : DefaultBelts.getList()) {
            if (beltModelsRepository.getByNameAndSystemDefaultTrue("Common " + baseGloveName) == null) {
                BeltModel beltModel = new BeltModel(baseGloveName);
                beltModel.setSystemDefault(true);
                beltModelsRepository.save(beltModel);
            }
        }
        if (beltModelsRepository.getByNameAndSystemDefaultTrue(DefaultBelts.DummyGloves) == null) {
            BeltModel beltModel = new BeltModel(DefaultBelts.DummyGloves);
            beltModel.setSystemDefault(true);
            beltModelsRepository.save(beltModel);
        }
        if (beltModelsRepository.getByNameAndSystemDefaultTrue(DefaultBelts.NoGloves) == null) {
            BeltModel beltModel = new BeltModel(DefaultBelts.NoGloves);
            beltModel.setSystemDefault(true);
            beltModelsRepository.save(beltModel);
        }
    }
}
