package com.loh.application.creatures.dtos;

import com.loh.application.skills.dtos.CreatureSkillsDto;
import com.loh.domain.creatures.Attributes;
import com.loh.domain.creatures.CreatureStatus;
import com.loh.domain.creatures.CreatureType;
import com.loh.domain.creatures.Resistances;
import com.loh.domain.creatures.equipments.Equipment;
import com.loh.domain.creatures.inventory.Inventory;
import com.loh.domain.effects.EffectInstance;
import com.loh.domain.races.Race;
import com.loh.domain.roles.Role;
import com.loh.shared.Bonus;
import com.loh.shared.EntityDto;

import java.util.List;
import java.util.UUID;

public class CreatureDto extends EntityDto {
    public String name;
    public UUID ownerId;
    public UUID creatorId;
    public Attributes baseAttributes;
    public Attributes bonusAttributes;
    public CreatureStatus status;
    public Resistances resistances;
    public Integer currentLife;
    public Integer currentMoral;
    public Integer manaSpent;
    public Integer level;
    public Race race;
    public Role role;
    public CreatureSkillsDto skills;
    public Equipment equipment;
    public Inventory inventory;
    public List<Bonus> bonuses;
    public List<EffectInstance> effects;
    public CreatureType creatureType;
}
