package com.loh.items.armors.armorCategories;


import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/armorCategory",  produces = "application/json; charset=UTF-8")
public class ArmorCategoryController {


    @GetMapping(path="/all")
    public @ResponseBody
    List<ArmorCategoryDto> getAllArmorCategory() {
        return ArmorCategoryDto.getList();
    }
    @GetMapping(path="/find")
    public @ResponseBody
    ArmorCategoryDto getArmorCategory(@RequestParam String name) {

        ArmorCategoryDto armorCategory = new ArmorCategoryDto(ArmorCategory.valueOf(name));
        return armorCategory;

    }

}
