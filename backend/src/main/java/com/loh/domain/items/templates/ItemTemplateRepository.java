package com.loh.domain.items.templates;

import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.UUID;

public interface ItemTemplateRepository  extends PagingAndSortingRepository<ItemTemplate, UUID> {

}
