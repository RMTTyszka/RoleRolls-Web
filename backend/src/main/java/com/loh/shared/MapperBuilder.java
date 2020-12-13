package com.loh.shared;

import org.mapstruct.factory.Mappers;

public class MapperBuilder<T> {
    public T build(Class aClass) {
        return (T)Mappers.getMapper(aClass);
    }
}
