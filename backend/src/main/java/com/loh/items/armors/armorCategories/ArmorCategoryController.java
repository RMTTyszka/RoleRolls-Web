package com.loh.items.armors.armorCategories;


import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import javax.json.Json;
import javax.json.JsonObject;
import java.util.UUID;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/armorCategory",  produces = "application/json; charset=UTF-8")
public class ArmorCategoryController {

    @Autowired
    private ArmorCategoryRepository armorCategoryRepository;


    @GetMapping(path="/all")
    public @ResponseBody
    Iterable<ArmorCategory> getAllArmorCategory() {

        Iterable<ArmorCategory> allBaseArmors = armorCategoryRepository.findAll();
        return allBaseArmors;
    }
    @GetMapping(path="/find")
    public @ResponseBody
    ArmorCategory getArmorCategory(@RequestParam UUID id) {

        Iterable<ArmorCategory> armor = armorCategoryRepository.findAll();


        return armor.iterator().next();

    }
    @GetMapping(path="/getNew")
    public @ResponseBody ArmorCategory getNewBaseArmor() {
        return new ArmorCategory();
    }
    @PutMapping(path="/update")
    public @ResponseBody
    JsonObject updateBaseArmor(@RequestBody ArmorCategory baseArmor) {

        armorCategoryRepository.save(baseArmor);

        return Json.createObjectBuilder()
                .add("text", "role updated with success").build();
    }
    @PutMapping(path="/add")
    public @ResponseBody
    JsonObject addBaseArmor(@RequestBody ArmorCategory baseArmor) {

        armorCategoryRepository.save(baseArmor);

        return Json.createObjectBuilder()
                .add("text", "role created with success").build();
    }
    @DeleteMapping(path="/delete")
    public @ResponseBody JsonObject deleteBaseArmor(@RequestParam UUID id) {

        ArmorCategory baseArmor = armorCategoryRepository.findById(id).get();
        armorCategoryRepository.deleteById(id);

        return Json.createObjectBuilder()
                .add("text", "role deleted with success").build();
    }


}
