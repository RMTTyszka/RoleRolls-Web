package com.loh.application.items.equipables.heads.instances;


import com.loh.domain.items.equipables.heads.instances.HeadpieceInstance;
import com.loh.domain.items.equipables.heads.instances.HeadpieceInstanceRepository;
import com.loh.shared.LegacyBaseCrudController;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.RequestMapping;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/headpieceInstance",  produces = "application/json; charset=UTF-8")
public class HeadpieceInstanceController extends LegacyBaseCrudController<HeadpieceInstance> {

    @Autowired
    public HeadpieceInstanceController(HeadpieceInstanceRepository repository) {
        super(repository);
    }

    @Override
    public HeadpieceInstance getnew() {
        return null;
    }
}
