package com.loh.creatures.monsters;


import com.loh.shared.BaseCrudController;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.RequestMapping;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/monsters",  produces = "application/json; charset=UTF-8")
public class MonsterController extends BaseCrudController<Monster> {

    @Autowired
    public MonsterController(MonsterRepository repository) {
        super(repository);
    }

    @Override
    public Monster getnew() {
        return null;
    }
}
