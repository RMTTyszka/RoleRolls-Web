package com.rolerolls.application.creatures.dtos;

import com.rolerolls.application.skills.dtos.CreatureSkillsDto;
import com.rolerolls.domain.creatures.Attributes;
import com.rolerolls.domain.creatures.CreatureStatus;
import com.rolerolls.domain.creatures.CreatureType;
import com.rolerolls.domain.creatures.Resistances;
import com.rolerolls.domain.creatures.equipments.Equipment;
import com.rolerolls.domain.creatures.inventory.Inventory;
import com.rolerolls.domain.effects.EffectInstance;
import com.rolerolls.domain.races.Race;
import com.rolerolls.domain.roles.Role;
import com.rolerolls.shared.Bonus;
import com.rolerolls.shared.EntityDto;

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
