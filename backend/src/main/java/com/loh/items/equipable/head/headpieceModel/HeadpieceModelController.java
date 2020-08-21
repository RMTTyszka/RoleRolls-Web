package com.loh.items.equipable.head.headpieceModel;


import com.loh.shared.BaseCrudController;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.RequestMapping;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/headpieceModel",  produces = "application/json; charset=UTF-8")
public class HeadpieceModelController extends BaseCrudController<HeadpieceModel> {

    @Autowired
    public HeadpieceModelController(HeadpieceModelRepository repository) {
        super(repository);
    }

    @Override
    public HeadpieceModel getnew() {
        return null;
    }
}
