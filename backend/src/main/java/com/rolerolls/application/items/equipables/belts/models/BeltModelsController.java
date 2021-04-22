package com.rolerolls.application.items.equipables.belts.models;


import com.rolerolls.domain.items.equipables.belts.models.BeltModel;
import com.rolerolls.domain.items.equipables.belts.models.BeltModelsRepository;
import com.rolerolls.shared.LegacyBaseCrudController;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.RequestMapping;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/beltModels",  produces = "application/json; charset=UTF-8")
public class BeltModelsController extends LegacyBaseCrudController<BeltModel> {

    @Autowired
    public BeltModelsController(BeltModelsRepository repository) {
        super(repository);
    }

    @Override
    public BeltModel getnew() {
        return null;
    }
}
