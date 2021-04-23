package com.rolerolls.application.items.equipables.weapons.categories;


import com.rolerolls.domain.items.equipables.weapons.categories.WeaponCategory;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

import java.util.List;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/weaponCategory",  produces = "application/json; charset=UTF-8")
public class WeaponCategoryController {


    @GetMapping(path="/all")
    public @ResponseBody
    List<WeaponCategory> getAllWeaponCategory() {

        List<WeaponCategory> allBaseWeapons = WeaponCategory.getList();
        return allBaseWeapons;
    }
    @GetMapping(path="/find")
    public @ResponseBody
    WeaponCategory getWeaponCategory(WeaponCategory weaponCategory) {

        return weaponCategory;
    }

}
