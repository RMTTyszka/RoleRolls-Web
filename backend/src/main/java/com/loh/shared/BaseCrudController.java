package com.loh.shared;

import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.web.bind.annotation.*;

import java.util.UUID;

public abstract class BaseCrudController<T extends Entity, R extends BaseRepository<T>> {
    public BaseCrudController(R repository) {
        this.repository = repository;
    }

    protected BaseRepository<T> repository;
    protected Page<T> filteredQuery(String filter, Pageable paged) {
        return repository.findAll(paged);
    }
    protected Page<T> unfilteredQuery(Pageable paged) {
        return repository.findAll(paged);
    }

    @GetMapping()
    public @ResponseBody
    Page<T> getList(@RequestParam String filter, @RequestParam int skipCount, @RequestParam int maxResultCount) {
        Pageable paged = PageRequest.of(skipCount, maxResultCount);
        if (filter.isEmpty() || filter == null) {
            Page<T> list =  unfilteredQuery(paged);
            return list;
        } else {
            return filteredQuery(filter, paged);
        }
    }
    @GetMapping(path="/{id}")
    public @ResponseBody
    T get(@PathVariable UUID id) {

        T entity = repository.findById(id).get();
        return entity;

    }
    @GetMapping(path="/new")
    public abstract  @ResponseBody
    T getnew();
    @PutMapping(path="")
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

    @PostMapping(path="")
    public @ResponseBody
    BaseCrudResponse<T> add(@RequestBody T entity) {

        return saveAndGetWeaponBaseCrudResponse(entity);
    }
    @DeleteMapping(path="/{id}")
    public @ResponseBody
    BaseCrudResponse<T> delete(@PathVariable UUID id) {

        T entity = repository.findById(id).get();
        BaseCrudResponse response = new BaseCrudResponse();
        response.success = true;
        response.entity = entity;

        repository.deleteById(id);
        return response;
    }

}
