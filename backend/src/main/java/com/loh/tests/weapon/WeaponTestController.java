package com.loh.tests.weapon;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/weaponTest",  produces = "application/json; charset=UTF-8")
public class WeaponTestController {

@Autowired
private WeaponTest weaponTest;

    @GetMapping(path="/makeTest")
    public @ResponseBody
    void makeTest() {
        weaponTest.test();
    }
}
