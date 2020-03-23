package com.loh.items.equipable.gloves.baseGloves;


import com.loh.items.equipable.gloves.gloveModels.GloveModel;
import com.loh.items.equipable.gloves.gloveModels.GloveModelsRepository;
import com.loh.items.equipable.weapons.weaponModel.WeaponModel;
import com.loh.shared.BaseCrudController;
import com.loh.shared.BaseCrudResponse;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import java.util.UUID;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/glovesModels",  produces = "application/json; charset=UTF-8")
public class GloveModelsController extends BaseCrudController<GloveModel> {

    @Autowired
    protected GloveModelsRepository repository;

}
