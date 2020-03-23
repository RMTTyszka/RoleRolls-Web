package com.loh.items.equipable.head;

import com.loh.items.equipable.head.baseHeadpieces.BaseHeadpiece;
import com.loh.items.equipable.head.baseHeadpieces.BaseHeadpieceRepository;
import com.loh.items.equipable.head.baseHeadpieces.DefaultHeadpieces;
import com.loh.items.equipable.head.headpieceModel.HeadpieceModel;
import com.loh.items.equipable.head.headpieceModel.HeadpieceModelRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class HeadpieceSeeder {

    @Autowired
    private BaseHeadpieceRepository baseHeadpieceRepository;
    @Autowired
    private HeadpieceModelRepository headpieceModelRepository;

    public void seed() {
        for(String baseBeltName : DefaultHeadpieces.getList()) {
            if (baseHeadpieceRepository.getByNameAndSystemDefaultTrue(baseBeltName) == null) {
                BaseHeadpiece baseHeadPiece = new BaseHeadpiece(baseBeltName);
                baseHeadPiece.setSystemDefault(true);
                baseHeadpieceRepository.save(baseHeadPiece);
            }
        }
        if (baseHeadpieceRepository.getByNameAndSystemDefaultTrue(DefaultHeadpieces.DummyHeadpiece) == null) {
            BaseHeadpiece baseHeadPiece = new BaseHeadpiece(DefaultHeadpieces.DummyHeadpiece);
            baseHeadPiece.setSystemDefault(true);
            baseHeadpieceRepository.save(baseHeadPiece);
        }
        if (baseHeadpieceRepository.getByNameAndSystemDefaultTrue(DefaultHeadpieces.NoHeadpiece) == null) {
            BaseHeadpiece baseHeadPiece = new BaseHeadpiece(DefaultHeadpieces.NoHeadpiece);
            baseHeadPiece.setSystemDefault(true);
            baseHeadpieceRepository.save(baseHeadPiece);
        }
        for(String baseBeltName : DefaultHeadpieces.getList()) {
            if (headpieceModelRepository.getByNameAndSystemDefaultTrue("Common " + baseBeltName) == null) {
                HeadpieceModel headpieceModel = new HeadpieceModel(baseBeltName);
                headpieceModel.setSystemDefault(true);
                headpieceModelRepository.save(headpieceModel);
            }
        }
        if (headpieceModelRepository.getByNameAndSystemDefaultTrue(DefaultHeadpieces.DummyHeadpiece) == null) {
            HeadpieceModel headpieceModel = new HeadpieceModel(DefaultHeadpieces.DummyHeadpiece);
            headpieceModel.setSystemDefault(true);
            headpieceModelRepository.save(headpieceModel);
        }
        if (headpieceModelRepository.getByNameAndSystemDefaultTrue(DefaultHeadpieces.NoHeadpiece) == null) {
            HeadpieceModel headpieceModel = new HeadpieceModel(DefaultHeadpieces.NoHeadpiece);
            headpieceModel.setSystemDefault(true);
            headpieceModelRepository.save(headpieceModel);
        }
    }
}
