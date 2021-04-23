package com.rolerolls.application.items.equipables.belts.base;


import com.rolerolls.domain.items.equipables.belts.base.BaseBelt;
import com.rolerolls.domain.items.equipables.belts.base.BaseBeltsRepository;
import com.rolerolls.shared.LegacyBaseCrudController;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.RequestMapping;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/baseBelts",  produces = "application/json; charset=UTF-8")
public class BaseBeltsController extends LegacyBaseCrudController<BaseBelt> {

    @Autowired
    public BaseBeltsController(BaseBeltsRepository repository) {
        super(repository);
    }

    @Override
    public BaseBelt getnew() {
        return null;
    }
}
