package com.rolerolls.domain.items.equipables.gloves;

import com.rolerolls.domain.items.equipables.gloves.base.BaseGlove;
import com.rolerolls.domain.items.equipables.gloves.base.BaseGloveRepository;
import com.rolerolls.domain.items.equipables.gloves.base.DefaultGloves;
import com.rolerolls.domain.items.equipables.gloves.models.GloveModel;
import com.rolerolls.domain.items.equipables.gloves.models.GloveModelsRepository;
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
            if (baseGloveRepository.getByNameAndSystemDefaultTrue(baseGloveName) == null) {
                BaseGlove baseGlove = new BaseGlove(baseGloveName);
                baseGlove.setSystemDefault(true);
                baseGloveRepository.save(baseGlove);
            }
        }
        if (baseGloveRepository.getByNameAndSystemDefaultTrue(DefaultGloves.DummyGloves) == null) {
            BaseGlove baseGlove = new BaseGlove(DefaultGloves.DummyGloves);
            baseGlove.setSystemDefault(true);
            baseGloveRepository.save(baseGlove);
        }
        if (baseGloveRepository.getByNameAndSystemDefaultTrue(DefaultGloves.NoGloves) == null) {
            BaseGlove baseGlove = new BaseGlove(DefaultGloves.NoGloves);
            baseGlove.setSystemDefault(true);
            baseGloveRepository.save(baseGlove);
        }
        for(String baseGloveName : DefaultGloves.getList()) {
            if (gloveModelsRepository.getByNameAndSystemDefaultTrue("Common " + baseGloveName) == null) {
                GloveModel gloveModel = new GloveModel(baseGloveName);
                gloveModel.setSystemDefault(true);
                gloveModelsRepository.save(gloveModel);
            }
        }
        if (gloveModelsRepository.getByNameAndSystemDefaultTrue(DefaultGloves.DummyGloves) == null) {
            GloveModel gloveModel = new GloveModel(DefaultGloves.DummyGloves);
            gloveModel.setSystemDefault(true);
            gloveModelsRepository.save(gloveModel);
        }
        if (gloveModelsRepository.getByNameAndSystemDefaultTrue(DefaultGloves.NoGloves) == null) {
            GloveModel gloveModel = new GloveModel(DefaultGloves.NoGloves);
            gloveModel.setSystemDefault(true);
            gloveModelsRepository.save(gloveModel);
        }
    }
}
