package com.loh.shared;

import org.springframework.data.domain.Pageable;
import org.springframework.data.repository.NoRepositoryBean;
import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.List;
import java.util.UUID;

@NoRepositoryBean
public interface BaseRepository<T extends Entity> extends PagingAndSortingRepository<T, UUID> {
    List<T> findAllByNameIgnoreCaseContaining(String name);
    List<T> findAllByNameIgnoreCaseContaining(String name, Pageable pageable);
}
