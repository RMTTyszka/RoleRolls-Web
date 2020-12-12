package com.loh.application.items.equipables.necks.base;


import com.loh.domain.items.equipables.necks.base.BaseNeckAccessory;
import com.loh.domain.items.equipables.necks.base.BaseNeckAcessoryRepository;
import com.loh.shared.LegacyBaseCrudController;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.RequestMapping;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/baseNeckAcessory",  produces = "application/json; charset=UTF-8")
public class BaseNeckAcessoryController extends LegacyBaseCrudController<BaseNeckAccessory> {

    @Autowired
    public BaseNeckAcessoryController(BaseNeckAcessoryRepository repository) {
        super(repository);
    }

    @Override
    public BaseNeckAccessory getnew() {
        return null;
    }
}
