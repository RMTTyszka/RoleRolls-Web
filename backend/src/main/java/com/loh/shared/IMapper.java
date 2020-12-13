package com.loh.shared;

public interface IMapper<TEntity, TDto>{
    TEntity mapToEntity(TDto dto);
    TDto mapToDto(TEntity entity);
}
