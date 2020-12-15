package com.loh.application.items.equipables.heads.models;


import com.loh.domain.items.equipables.heads.models.HeadpieceModel;
import com.loh.domain.items.equipables.heads.models.HeadpieceModelRepository;
import com.loh.shared.LegacyBaseCrudController;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.RequestMapping;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/headpieceModel",  produces = "application/json; charset=UTF-8")
public class HeadpieceModelController extends LegacyBaseCrudController<HeadpieceModel> {

    @Autowired
    public HeadpieceModelController(HeadpieceModelRepository repository) {
        super(repository);
    }

    @Override
    public HeadpieceModel getnew() {
        return null;
    }
}
