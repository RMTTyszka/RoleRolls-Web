package com.loh.shared;

import org.springframework.data.jpa.repository.JpaSpecificationExecutor;
import org.springframework.data.repository.NoRepositoryBean;
import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.UUID;

@NoRepositoryBean
public interface BaseRepository<T extends Entity> extends PagingAndSortingRepository<T, UUID>, JpaSpecificationExecutor<T> {
    @Override
    boolean existsById(UUID uuid);
}
