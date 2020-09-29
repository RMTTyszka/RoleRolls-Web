package com.loh.creatures.monsters.models;

import java.util.HashMap;
import java.util.UUID;

public class DefaultMonsterModelsMapping {
    public static HashMap<DefaultMonsterModel, UUID> models = new HashMap<>();
    static {
        models.put(DefaultMonsterModel.Zombie,  UUID.fromString("2386b5ea-85eb-4b5b-aced-b98cde69136b"));
        models.put(DefaultMonsterModel.Skeleton, UUID.fromString("ee5fe0e9-3248-4158-84a2-1e8b1bc8a611"));
    }
}
