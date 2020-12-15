package com.loh.application.items.equipables.necks.instances;


import com.loh.domain.items.equipables.necks.instances.NeckAccessoryInstance;
import com.loh.domain.items.equipables.necks.instances.NeckAccessoryInstanceRepository;
import com.loh.shared.LegacyBaseCrudController;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.RequestMapping;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/neckAcessoryInstance",  produces = "application/json; charset=UTF-8")
public class NeckAcessoryInstanceController extends LegacyBaseCrudController<NeckAccessoryInstance> {

    @Autowired
    public NeckAcessoryInstanceController(NeckAccessoryInstanceRepository repository) {
        super(repository);
    }

    @Override
    public NeckAccessoryInstance getnew() {
        return null;
    }
}
