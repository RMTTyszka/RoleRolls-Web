package com.loh.domain.creatures.equipments;

import com.loh.domain.creatures.Creature;
import com.loh.domain.items.equipables.armors.instances.ArmorInstance;
import com.loh.domain.items.equipables.weapons.instances.WeaponInstance;
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
    public Creature equipOffWeapon(Creature creature, UUID weaponInstanceId) throws Exception {
        WeaponInstance weapon = (WeaponInstance) creature.getInventory().getItem(weaponInstanceId);
        creature.equipOffhandWeapon(weapon);
        return creature;
    }
}
