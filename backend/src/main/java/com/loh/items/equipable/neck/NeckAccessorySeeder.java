package com.loh.items.equipable.neck;

import com.loh.items.equipable.neck.baseNeckAccessory.BaseNeckAccessory;
import com.loh.items.equipable.neck.baseNeckAccessory.BaseNeckAcessoryRepository;
import com.loh.items.equipable.neck.baseNeckAccessory.DefaultNeckAcessories;
import com.loh.items.equipable.neck.neckAccessoryModels.NeckAcessoryModel;
import com.loh.items.equipable.neck.neckAccessoryModels.NeckAccessoryModelRepository;
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
