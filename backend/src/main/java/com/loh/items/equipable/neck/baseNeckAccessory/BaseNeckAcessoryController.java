package com.loh.items.equipable.neck.baseNeckAccessory;


import com.loh.shared.BaseCrudController;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.RequestMapping;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/baseNeckAcessory",  produces = "application/json; charset=UTF-8")
public class BaseNeckAcessoryController extends BaseCrudController<BaseNeckAccessory> {

    @Autowired
    protected BaseNeckAcessoryRepository repository;

}
