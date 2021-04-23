package com.rolerolls.shared;

import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.web.bind.annotation.*;

import java.util.UUID;

public abstract class LegacyBaseCrudController<T extends Entity> {
    public LegacyBaseCrudController(LegacyBaseRepository<T> repository) {
        this.repository = repository;
    }

    protected LegacyBaseRepository<T> repository;

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

        T entity = repository.findById(id).get();
        return entity;

    }
    @GetMapping(path="/getNew")
    public abstract  @ResponseBody
    T getnew();
    @PutMapping(path="/update")
    public @ResponseBody
    BaseCrudResponse<T> update(@RequestBody T entity) {

        return saveAndGetWeaponBaseCrudResponse(entity);
    }

    private BaseCrudResponse<T> saveAndGetWeaponBaseCrudResponse(T entity) {
        T updatedWeapon = repository.save(entity);
        BaseCrudResponse response = new BaseCrudResponse<T>();
        response.success = true;
        response.message = "Successfully Created Weapon";
        response.entity = updatedWeapon;
        return response;
    }

    @PostMapping(path="/create")
    public @ResponseBody
    BaseCrudResponse<T> add(@RequestBody T entity) {

        return saveAndGetWeaponBaseCrudResponse(entity);
    }
    @DeleteMapping(path="/delete")
    public @ResponseBody
    BaseCrudResponse<T> delete(@RequestParam UUID id) {

        T entity = repository.findById(id).get();
        BaseCrudResponse response = new BaseCrudResponse();
        response.success = true;
        response.entity = entity;

        repository.deleteById(id);
        return response;
    }

}
