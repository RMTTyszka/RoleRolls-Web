package com.loh.application.items.equipables.gloves.base;


import com.loh.domain.items.equipables.gloves.models.GloveModel;
import com.loh.domain.items.equipables.gloves.models.GloveModelsRepository;
import com.loh.shared.LegacyBaseCrudController;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.RequestMapping;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/glovesModels",  produces = "application/json; charset=UTF-8")
public class GloveModelsController extends LegacyBaseCrudController<GloveModel> {

    @Autowired
    public GloveModelsController(GloveModelsRepository repository) {
        super(repository);
    }

    @Override
    public GloveModel getnew() {
        return null;
    }
}
