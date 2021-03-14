package com.loh.application.universes;

import com.loh.domain.universes.UniverseType;
import org.springframework.data.jpa.domain.Specification;

public class UniversesQueries {

    public static Specification fromUniverse(UniverseType universeType) {
        return (entity, cq, cb) -> cb.equal(entity.get("universeType"), universeType);
    }
}
