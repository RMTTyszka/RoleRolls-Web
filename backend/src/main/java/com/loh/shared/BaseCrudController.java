package com.loh.shared;

import com.loh.items.equipable.weapons.weaponModel.WeaponModel;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.web.bind.annotation.*;

import java.util.UUID;

public class BaseCrudController<T extends Entity> {
    public BaseCrudController(BaseRepository<T> repository) {
        this.repository = repository;
    }

    protected BaseRepository<T> repository;

    @GetMapping(path="/allPaged")
    public @ResponseBody
    Iterable<T> getAllPaged(@RequestParam String filter, @RequestParam int skipCount, @RequestParam int maxResultCount) {

        Pageable paged = PageRequest.of(skipCount, maxResultCount);
        if (filter.isEmpty() || filter == null) {
            Iterable<T> list =  repository.findAll(paged);
            return list;
        }
        return repository.findAllByNameIgnoreCaseContaining(filter, paged);
    }
    @GetMapping(path="/allFiltered")
    public @ResponseBody
    Iterable<T> getAllFiltered(@RequestParam String filter) {
        // This returns a JSON or XML with the users
        if (filter.isEmpty() || filter == null) {
            return repository.findAll();
        }
        return repository.findAllByNameIgnoreCaseContaining(filter);
    }
    @GetMapping(path="/find")
    public @ResponseBody
    T get(@RequestParam UUID id) {

        T armor = repository.findById(id).get();
        return armor;

    }
    @GetMapping(path="/getNew")
    public @ResponseBody
    WeaponModel getnew() {
        return new WeaponModel();
    }
    @PutMapping(path="/update")
    public @ResponseBody
    BaseCrudResponse<T> update(@RequestBody T weapon) {

        return saveAndGetWeaponBaseCrudResponse(weapon);
    }

    private BaseCrudResponse<T> saveAndGetWeaponBaseCrudResponse(T weapon) {
        T updatedWeapon = repository.save(weapon);
        BaseCrudResponse response = new BaseCrudResponse<T>();
        response.success = true;
        response.message = "Successfully Created Weapon";
        response.entity = updatedWeapon;
        return response;
    }

    @PostMapping(path="/create")
    public @ResponseBody
    BaseCrudResponse<T> add(@RequestBody T weapon) {

        return saveAndGetWeaponBaseCrudResponse(weapon);
    }
    @DeleteMapping(path="/delete")
    public @ResponseBody
    BaseCrudResponse<T> delete(@RequestParam UUID id) {

        T weapon = repository.findById(id).get();
        BaseCrudResponse response = new BaseCrudResponse();
        response.success = true;
        response.entity = weapon;

        repository.deleteById(id);
        return response;
    }

}
