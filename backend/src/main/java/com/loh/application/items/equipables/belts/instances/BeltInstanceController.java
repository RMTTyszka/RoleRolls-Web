package com.loh.application.items.equipables.belts.instances;


import com.loh.domain.items.equipables.belts.instances.BeltInstance;
import com.loh.domain.items.equipables.belts.instances.BeltInstanceRepository;
import com.loh.shared.LegacyBaseCrudController;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.RequestMapping;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/beltInstance",  produces = "application/json; charset=UTF-8")
public class BeltInstanceController extends LegacyBaseCrudController<BeltInstance> {

    @Autowired
    public BeltInstanceController(BeltInstanceRepository repository) {
        super(repository);
    }

    @Override
    public BeltInstance getnew() {
        return null;
    }
}
