package com.rolerolls.application.items.equipables.rings.models;


import com.rolerolls.domain.items.equipables.rings.models.RingModel;
import com.rolerolls.domain.items.equipables.rings.models.RingModelRepository;
import com.rolerolls.shared.LegacyBaseCrudController;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.RequestMapping;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/ringModel",  produces = "application/json; charset=UTF-8")
public class RingModelController extends LegacyBaseCrudController<RingModel> {

    @Autowired
    public RingModelController(RingModelRepository repository) {
        super(repository);
    }

    @Override
    public RingModel getnew() {
        return null;
    }
}
