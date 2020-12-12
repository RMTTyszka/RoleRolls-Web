package com.loh.application.items.equipables.necks.models;


import com.loh.domain.items.equipables.necks.models.NeckAccessoryModelRepository;
import com.loh.domain.items.equipables.necks.models.NeckAcessoryModel;
import com.loh.shared.LegacyBaseCrudController;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.RequestMapping;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/neckAcessoryModel",  produces = "application/json; charset=UTF-8")
public class NeckAcessoryModelController extends LegacyBaseCrudController<NeckAcessoryModel> {

    @Autowired
    public NeckAcessoryModelController(NeckAccessoryModelRepository repository) {
        super(repository);
    }

    @Override
    public NeckAcessoryModel getnew() {
        return null;
    }
}
