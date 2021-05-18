package com.rolerolls.application.encounters.dtos;

import com.rolerolls.application.environments.Enviroment;
import com.rolerolls.domain.creatures.monsters.models.MonsterModel;
import lombok.Getter;
import lombok.Setter;

import java.util.ArrayList;
import java.util.List;

public class EncounterInput {
    private Integer level;

    @Getter
    @Setter
    private List<MonsterModel> monsters = new ArrayList<>();

    @Getter
    @Setter
    private List<Enviroment> enviroments;

    public Integer getLevel() {
        return level;
    }
    public void setLevel(Integer level) {
        this.level = level;
    }

    public EncounterInput() {
        enviroments = new ArrayList<>();
        monsters = new ArrayList<>();
    }
}
