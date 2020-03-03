package com.loh.items.weapons.weaponCategory;


import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import java.util.UUID;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/weaponCategory",  produces = "application/json; charset=UTF-8")
public class WeaponCategoryController {

    @Autowired
    private WeaponCategoryRepository weaponCategoryRepository;


    @GetMapping(path="/all")
    public @ResponseBody
    Iterable<WeaponCategory> getAllWeaponCategory() {

        Iterable<WeaponCategory> allBaseWeapons = weaponCategoryRepository.findAll();
        return allBaseWeapons;
    }
    @GetMapping(path="/find")
    public @ResponseBody
    WeaponCategory getWeaponCategory(@RequestParam UUID id) {

        Iterable<WeaponCategory> weapon = weaponCategoryRepository.findAll();
        return weapon.iterator().next();

    }

}
