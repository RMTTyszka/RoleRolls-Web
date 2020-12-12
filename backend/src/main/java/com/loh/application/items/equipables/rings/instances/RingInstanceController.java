package com.loh.application.items.equipables.rings.instances;


import com.loh.domain.items.equipables.rings.instances.RingInstance;
import com.loh.domain.items.equipables.rings.instances.RingInstanceRepository;
import com.loh.shared.LegacyBaseCrudController;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.RequestMapping;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/ringInstance",  produces = "application/json; charset=UTF-8")
public class RingInstanceController extends LegacyBaseCrudController<RingInstance> {

    @Autowired
    public RingInstanceController(RingInstanceRepository repository) {
        super(repository);
    }

    @Override
    public RingInstance getnew() {
        return null;
    }
}
