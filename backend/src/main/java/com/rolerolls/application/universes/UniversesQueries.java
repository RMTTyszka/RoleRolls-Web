package com.rolerolls.application.universes;

import com.rolerolls.domain.universes.UniverseType;
import org.springframework.data.jpa.domain.Specification;

public class UniversesQueries {

    public static Specification fromUniverse(UniverseType universeType) {
        return (entity, cq, cb) -> cb.equal(entity.get("universeType"), universeType);
    }
}
