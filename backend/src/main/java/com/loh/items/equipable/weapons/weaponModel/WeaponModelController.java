package com.loh.items.equipable.weapons.weaponModel;


import com.loh.shared.BaseCrudResponse;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import java.util.UUID;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/weaponModels",  produces = "application/json; charset=UTF-8")
public class WeaponModelController {

    @Autowired
    private WeaponModelRepository repository;

    @GetMapping(path="/allPaged")
    public @ResponseBody
    Iterable<WeaponModel> getAllPaged(@RequestParam String filter, @RequestParam int skipCount, @RequestParam int maxResultCount) {

        Pageable paged = PageRequest.of(skipCount, maxResultCount);
        if (filter.isEmpty() || filter == null) {
            return repository.findAll(paged);
        }
        return repository.findAllByNameIgnoreCaseContaining(filter, paged);
    }
    @GetMapping(path="/allFiltered")
    public @ResponseBody
    Iterable<WeaponModel> getAllFiltered(@RequestParam String filter) {
        // This returns a JSON or XML with the users
        if (filter.isEmpty() || filter == null) {
            return repository.findAll();
        }
        return repository.findAllByNameIgnoreCaseContaining(filter);
    }
    @GetMapping(path="/find")
    public @ResponseBody
    WeaponModel getWeapon(@RequestParam UUID id) {

        WeaponModel armor = repository.findById(id).get();

        return armor;

    }
    @GetMapping(path="/getNew")
    public @ResponseBody
    WeaponModel getNewWeapon() {
        return new WeaponModel();
    }
    @PutMapping(path="/update")
    public @ResponseBody
    BaseCrudResponse<WeaponModel> updateWeapon(@RequestBody WeaponModel weapon) {

        return saveAndGetWeaponBaseCrudResponse(weapon);
    }

    private BaseCrudResponse<WeaponModel> saveAndGetWeaponBaseCrudResponse(WeaponModel weapon) {
        WeaponModel updatedWeapon = repository.save(weapon);
        BaseCrudResponse response = new BaseCrudResponse<WeaponModel>();
        response.success = true;
        response.message = "Successfully Created Weapon";
        response.entity = updatedWeapon;
        return response;
    }

    @PostMapping(path="/create")
    public @ResponseBody
    BaseCrudResponse<WeaponModel> addWeapon(@RequestBody WeaponModel weapon) {

        return saveAndGetWeaponBaseCrudResponse(weapon);
    }
    @DeleteMapping(path="/delete")
    public @ResponseBody
    BaseCrudResponse<WeaponModel> deleteWeapon(@RequestParam UUID id) {

        WeaponModel weapon = repository.findById(id).get();
        BaseCrudResponse response = new BaseCrudResponse();
        response.success = true;
        response.entity = weapon;

        repository.deleteById(id);
        return response;
    }

}
