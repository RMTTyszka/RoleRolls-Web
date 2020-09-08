package com.loh.context;

import org.springframework.data.jpa.repository.JpaSpecificationExecutor;
import org.springframework.data.repository.CrudRepository;

import java.util.UUID;

public interface PlayerRepository extends CrudRepository<Player, UUID>, JpaSpecificationExecutor<Player> {

}
