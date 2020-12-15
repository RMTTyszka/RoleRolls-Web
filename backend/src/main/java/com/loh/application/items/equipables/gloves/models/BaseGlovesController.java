package com.loh.application.items.equipables.gloves.models;


import com.loh.domain.items.equipables.gloves.base.BaseGlove;
import com.loh.domain.items.equipables.gloves.base.BaseGloveRepository;
import com.loh.shared.LegacyBaseCrudController;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.RequestMapping;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/baseGloves",  produces = "application/json; charset=UTF-8")
public class BaseGlovesController extends LegacyBaseCrudController<BaseGlove> {

    @Autowired
    public BaseGlovesController(BaseGloveRepository repository) {
        super(repository);
    }

    @Override
    public BaseGlove getnew() {
        return null;
    }
}
