package com.rolerolls.shared;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.rolerolls.domain.universes.UniverseType;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.web.bind.annotation.*;

import java.util.UUID;

public abstract class BaseCrudControllerWithService<T extends Entity,TCreateInput, TUpdateInput, R extends BaseRepository<T>, Service extends BaseCrudService<T, TCreateInput, TUpdateInput, R>> {

    public BaseCrudControllerWithService(Service service) {
        this.service = service;
    }
    protected BaseCrudService<T, TCreateInput, TUpdateInput, R> service;

    protected Page<T> filteredQuery(String filter, Pageable paged) {
        return service.filteredQuery(filter, paged);
    }
    protected Page<T> unfilteredQuery(Pageable paged) {
        return service.unfilteredQuery(paged);
    }

    @GetMapping()
    public @ResponseBody
    Page<T> getList(@RequestParam String filter, @RequestParam int skipCount, @RequestParam int maxResultCount, @RequestHeader(value = "universe-type", defaultValue = "LandOfHeroes") UniverseType universeType) throws JsonProcessingException {
      return  this.service.getList(filter, skipCount, maxResultCount, universeType);
    }
    @GetMapping(path="/{id}")
    public @ResponseBody
    T get(@PathVariable UUID id) {
       return this.service.get(id);
    }
    @GetMapping(path="/new")
    public abstract  @ResponseBody
    TCreateInput getNew();
    @PutMapping(path="/{id}")
    public @ResponseBody
    BaseCrudResponse<T> update(@PathVariable UUID id, @RequestBody TUpdateInput input) {
        BaseCrudResponse<T> output = new BaseCrudResponse<>();
        output.entity = this.service.update(id, input);
        output.success = true;
        output.message = "Successfully Updated";
        return output;
    }

    @PostMapping(path="")
    public @ResponseBody
    BaseCrudResponse<T> add(@RequestBody TCreateInput input) throws Exception {
        BaseCrudResponse<T> output = new BaseCrudResponse<>();
        output.entity = this.service.add(input);
        output.success = true;
        output.message = "Successfully Created";
        return output;
    }
    @DeleteMapping(path="/{id}")
    public @ResponseBody
    BaseCrudResponse<T> delete(@PathVariable UUID id) {
        return this.service.delete(id);
    }

}
