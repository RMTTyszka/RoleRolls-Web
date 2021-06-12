package com.rolerolls.shared;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.rolerolls.domain.universes.UniverseType;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;

import java.util.UUID;

public abstract class BaseCrudService<T extends Entity,TCreateInput, TUpdateInput, R extends BaseRepository<T>> {
    public BaseCrudService(R repository) {
        this.repository = repository;
    }

    protected BaseRepository<T> repository;
    protected Page<T> filteredQuery(String filter, Pageable paged) {
        return repository.findAll(paged);
    }
    protected Page<T> unfilteredQuery(Pageable paged) {
        return repository.findAll(paged);
    }

    public Page<T> getList(String filter, int skipCount, int maxResultCount, UniverseType universeType) throws JsonProcessingException {
        Pageable paged = PageRequest.of(skipCount, maxResultCount);
        if (filter.isEmpty() || filter == null) {
            Page<T> list =  unfilteredQuery(paged);
            ObjectMapper o = new ObjectMapper();
            String s = o.writeValueAsString(list);
            return list;
        } else {
            return filteredQuery(filter, paged);
        }
    }
    public T get(UUID id) {

        T entity = repository.findById(id).get();
        return entity;

    }
    public abstract TCreateInput getNew();
    public BaseCrudResponse<T> update(UUID id, TUpdateInput input) {
        T entity = updateInputToEntity(id, input);
        return saveAndGetWeaponBaseCrudResponse(entity);
    }

    private BaseCrudResponse<T> saveAndGetWeaponBaseCrudResponse(T entity) {
        T updatedEntity = repository.save(entity);
        BaseCrudResponse response = new BaseCrudResponse<T>();
        response.success = true;
        response.message = "Successfully Created";
        response.entity = updatedEntity;
        return response;
    }

    protected abstract T createInputToEntity(TCreateInput input);
    protected abstract T updateInputToEntity(UUID id, TUpdateInput input);

    public BaseCrudResponse<T> add(TCreateInput input) throws Exception {
        T entity = createInputToEntity(input);
        return saveAndGetWeaponBaseCrudResponse(entity);
    }
    public BaseCrudResponse<T> delete(UUID id) {

        T entity = repository.findById(id).get();
        BaseCrudResponse response = new BaseCrudResponse();
        response.success = true;
        response.entity = entity;

        repository.deleteById(id);
        return response;
    }

}
