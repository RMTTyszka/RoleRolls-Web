package com.loh.domain.items.equipables.necks;

import com.loh.domain.items.equipables.necks.base.BaseNeckAccessory;
import com.loh.domain.items.equipables.necks.base.BaseNeckAcessoryRepository;
import com.loh.domain.items.equipables.necks.base.DefaultNeckAcessories;
import com.loh.domain.items.equipables.necks.models.NeckAcessoryModel;
import com.loh.domain.items.equipables.necks.models.NeckAccessoryModelRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class NeckAccessorySeeder {

    @Autowired
    private BaseNeckAcessoryRepository baseNeckAcessoryRepository;
    @Autowired
    private NeckAccessoryModelRepository neckAccessoryModelRepository;

    public void seed() {
        for(String baseBeltName : DefaultNeckAcessories.getList()) {
            if (baseNeckAcessoryRepository.getByNameAndSystemDefaultTrue(baseBeltName) == null) {
                BaseNeckAccessory baseHeadPiece = new BaseNeckAccessory(baseBeltName);
                baseHeadPiece.setSystemDefault(true);
                baseNeckAcessoryRepository.save(baseHeadPiece);
            }
        }
        if (baseNeckAcessoryRepository.getByNameAndSystemDefaultTrue(DefaultNeckAcessories.DummyNeckAcessory) == null) {
            BaseNeckAccessory baseHeadPiece = new BaseNeckAccessory(DefaultNeckAcessories.DummyNeckAcessory);
            baseHeadPiece.setSystemDefault(true);
            baseNeckAcessoryRepository.save(baseHeadPiece);
        }
        if (baseNeckAcessoryRepository.getByNameAndSystemDefaultTrue(DefaultNeckAcessories.NoNeckAcessory) == null) {
            BaseNeckAccessory baseHeadPiece = new BaseNeckAccessory(DefaultNeckAcessories.NoNeckAcessory);
            baseHeadPiece.setSystemDefault(true);
            baseNeckAcessoryRepository.save(baseHeadPiece);
        }
        for(String baseBeltName : DefaultNeckAcessories.getList()) {
            if (neckAccessoryModelRepository.getByNameAndSystemDefaultTrue("Common " + baseBeltName) == null) {
                NeckAcessoryModel neckAcessoryModel = new NeckAcessoryModel(baseBeltName);
                neckAcessoryModel.setSystemDefault(true);
                neckAccessoryModelRepository.save(neckAcessoryModel);
            }
        }
        if (neckAccessoryModelRepository.getByNameAndSystemDefaultTrue(DefaultNeckAcessories.DummyNeckAcessory) == null) {
            NeckAcessoryModel neckAcessoryModel = new NeckAcessoryModel(DefaultNeckAcessories.DummyNeckAcessory);
            neckAcessoryModel.setSystemDefault(true);
            neckAccessoryModelRepository.save(neckAcessoryModel);
        }
        if (neckAccessoryModelRepository.getByNameAndSystemDefaultTrue(DefaultNeckAcessories.NoNeckAcessory) == null) {
            NeckAcessoryModel neckAcessoryModel = new NeckAcessoryModel(DefaultNeckAcessories.NoNeckAcessory);
            neckAcessoryModel.setSystemDefault(true);
            neckAccessoryModelRepository.save(neckAcessoryModel);
        }
    }
}
