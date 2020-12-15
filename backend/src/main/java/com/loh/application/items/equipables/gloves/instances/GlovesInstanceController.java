package com.loh.application.items.equipables.gloves.instances;


import com.loh.domain.items.equipables.gloves.instances.GloveInstance;
import com.loh.domain.items.equipables.gloves.instances.GloveInstanceRepository;
import com.loh.shared.LegacyBaseCrudController;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.RequestMapping;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/glovesInstance",  produces = "application/json; charset=UTF-8")
public class GlovesInstanceController extends LegacyBaseCrudController<GloveInstance> {

    @Autowired
    public GlovesInstanceController(GloveInstanceRepository repository) {
        super(repository);
    }

    @Override
    public GloveInstance getnew() {
        return null;
    }
}
