package com.loh.domain.creatures.monsters;

import com.loh.domain.creatures.monsters.models.MonsterModel;

public class NewMonsterDto {
    public String name;
    public MonsterModel monsterModel;

    public NewMonsterDto() {
        monsterModel = new MonsterModel();
    }
}
