package com.loh.items.equipable.belts.beltModels;


import com.loh.shared.BaseCrudController;
import com.loh.shared.BaseRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.RequestMapping;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/beltModels",  produces = "application/json; charset=UTF-8")
public class BeltModelsController extends BaseCrudController<BeltModel> {

    @Autowired
    public BeltModelsController(BeltModelsRepository repository) {
        super(repository);
    }
}
