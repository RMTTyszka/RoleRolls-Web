package com.loh.application.items.equipables.heads.base;


import com.loh.domain.items.equipables.heads.base.BaseHeadpiece;
import com.loh.domain.items.equipables.heads.base.BaseHeadpieceRepository;
import com.loh.shared.LegacyBaseCrudController;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.RequestMapping;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/baseHeadpiece",  produces = "application/json; charset=UTF-8")
public class BaseHeadpieceController extends LegacyBaseCrudController<BaseHeadpiece> {

    @Autowired
    public BaseHeadpieceController(BaseHeadpieceRepository repository) {
        super(repository);
    }

    @Override
    public BaseHeadpiece getnew() {
        return null;
    }
}
