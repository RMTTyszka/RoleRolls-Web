package com.loh.items.weapons.baseWeapon;


import com.loh.shared.BaseCrudResponse;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import java.util.UUID;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/baseWeapons",  produces = "application/json; charset=UTF-8")
public class BaseWeaponController {

    @Autowired
    private BaseWeaponRepository repository;


    @GetMapping(path="/allPaged")
    public @ResponseBody
    Iterable<BaseWeapon> getAllPaged(@RequestParam String filter, @RequestParam int skipCount, @RequestParam int maxResultCount) {

        Pageable paged = PageRequest.of(skipCount, maxResultCount);
        if (filter.isEmpty() || filter == null) {
            return repository.findAll(paged);
        }
        return repository.findAllByNameIgnoreCaseContaining(filter, paged);
    }
    @GetMapping(path="/allFiltered")
    public @ResponseBody
    Iterable<BaseWeapon> getAllFiltered(@RequestParam String filter) {
        // This returns a JSON or XML with the users
        if (filter.isEmpty() || filter == null) {
            return repository.findAll();
        }
        return repository.findAllByNameIgnoreCaseContaining(filter);
    }
    @GetMapping(path="/find")
    public @ResponseBody
    BaseWeapon get(@RequestParam UUID id) {

        BaseWeapon weapon = repository.findById(id).get();

        return weapon;

    }
    @GetMapping(path="/getNew")
    public @ResponseBody BaseWeapon getNew() {
        return new BaseWeapon();
    }
    @PutMapping(path="/update")
    public @ResponseBody
    BaseCrudResponse<BaseWeapon> update(@RequestBody BaseWeapon baseWeapon) {
        BaseCrudResponse response = new BaseCrudResponse<BaseWeapon>();
        boolean isStatic = repository.findById(baseWeapon.getId()).get().isSystemDefault();
        if (isStatic) {
            response.Success = false;
            response.Entity = baseWeapon;
            response.setMessage("Cannot Update Default Base Weapons");
            return response;
        }
        BaseWeapon updatedBaseArmor = repository.save(baseWeapon);
        response.Success = true;
        response.Entity = updatedBaseArmor;
        return response;
    }

    @PostMapping(path="/create")
    public @ResponseBody
    BaseCrudResponse<BaseWeapon> create(@RequestBody BaseWeapon baseWeapon) {
        BaseCrudResponse response = new BaseCrudResponse<BaseWeapon>();
        try {
            BaseWeapon createdBaseArmor = repository.save(baseWeapon);
            response.Success = true;
            response.Message = "Successfully Created";
            response.Entity = createdBaseArmor;
            return response;
        } catch (Exception e) {
            response.setSuccess(false);
            response.setMessage(e.getMessage());
            return response;
        }

    }
    @DeleteMapping(path="/delete")
    public @ResponseBody
    BaseCrudResponse<BaseWeapon> delete(@RequestParam UUID id) {

        BaseWeapon baseWeapon = repository.findById(id).get();
        BaseCrudResponse response = new BaseCrudResponse();
        if (!baseWeapon.isSystemDefault()){
            repository.deleteById(id);
            response.Success = true;
            response.Entity = baseWeapon;
        } else {
            response.Success = false;
            response.Message = "cannot delete default base armorModel";
            response.Entity = baseWeapon;
        }

        return response;
    }

}
