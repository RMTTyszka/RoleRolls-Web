package com.loh.items.equipable.gloves.gloveModels;


import com.loh.items.equipable.gloves.baseGloves.BaseGlove;
import com.loh.items.equipable.gloves.baseGloves.BaseGloveRepository;
import com.loh.shared.BaseCrudController;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.RequestMapping;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/baseGloves",  produces = "application/json; charset=UTF-8")
public class BaseGlovesController extends BaseCrudController<BaseGlove> {

    @Autowired
    public BaseGlovesController(BaseGloveRepository repository) {
        super(repository);
    }
}
