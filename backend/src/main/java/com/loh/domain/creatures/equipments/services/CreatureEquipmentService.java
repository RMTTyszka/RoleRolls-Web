package com.loh.domain.creatures.equipments.services;

import com.loh.domain.creatures.Creature;
import com.loh.domain.creatures.equipments.services.dtos.EquipItemValidationType;
import com.loh.domain.items.equipables.armors.instances.ArmorInstance;
import com.loh.domain.items.equipables.weapons.instances.WeaponInstance;
import com.loh.shared.ValidationResult;
import org.springframework.stereotype.Service;

import java.util.UUID;

@Service
public class CreatureEquipmentService {

    public Creature equipArmor(Creature creature, UUID armorInstanceId) {
        ArmorInstance armor = (ArmorInstance) creature.getInventory().getItem(armorInstanceId);
        creature.equipArmor(armor);
        return creature;
    }
    public Creature equipMainWeapon(Creature creature, UUID weaponInstanceId) throws Exception {
        WeaponInstance weapon = (WeaponInstance) creature.getInventory().getItem(weaponInstanceId);
        creature.equipMainWeapon(weapon);
        return creature;
    }
    public ValidationResult<Creature, EquipItemValidationType> equipOffWeapon(Creature creature, UUID weaponInstanceId) throws Exception {
        WeaponInstance weapon = (WeaponInstance) creature.getInventory().getItem(weaponInstanceId);
        EquipItemValidationType validationResult = creature.equipOffhandWeapon(weapon);
        return new ValidationResult<Creature, EquipItemValidationType>(creature, validationResult);
    }
}
