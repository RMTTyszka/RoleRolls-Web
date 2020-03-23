package com.loh.items.equipable.gloves;

import com.loh.items.equipable.gloves.baseGloves.BaseGlove;
import com.loh.items.equipable.gloves.baseGloves.BaseGloveRepository;
import com.loh.items.equipable.gloves.baseGloves.DefaultGloves;
import com.loh.items.equipable.gloves.gloveModels.GloveModel;
import com.loh.items.equipable.gloves.gloveModels.GloveModelsRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class GlovesSeeder {

    @Autowired
    private BaseGloveRepository baseGloveRepository;
    @Autowired
    private GloveModelsRepository gloveModelsRepository;

    public void seed() {
        for(String baseGloveName : DefaultGloves.getList()) {
            if (baseGloveRepository.getByNameAndSystemDefaultTrue(baseGloveName) != null) {
                BaseGlove baseGlove = new BaseGlove(baseGloveName);
                baseGloveRepository.save(baseGlove);
            }
        }
        if (baseGloveRepository.getByNameAndSystemDefaultTrue(DefaultGloves.DummyGloves) != null) {
            BaseGlove baseGlove = new BaseGlove(DefaultGloves.DummyGloves);
            baseGloveRepository.save(baseGlove);
        }
        for(String baseGloveName : DefaultGloves.getList()) {
            if (gloveModelsRepository.getByNameAndSystemDefaultTrue("Common " + baseGloveName) != null) {
                GloveModel baseGlove = new GloveModel(baseGloveName);
                gloveModelsRepository.save(baseGlove);
            }
        }
        if (gloveModelsRepository.getByNameAndSystemDefaultTrue(DefaultGloves.DummyGloves) != null) {
            GloveModel gloveModel = new GloveModel(DefaultGloves.DummyGloves);
            gloveModelsRepository.save(gloveModel);
        }
    }
}
