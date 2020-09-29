package com.loh.creatures.monsters;

import com.loh.creatures.monsters.models.MonsterModel;

public class NewMonsterDto {
    public String name;
    public MonsterModel monsterModel;

    public NewMonsterDto() {
        monsterModel = new MonsterModel();
    }
}
