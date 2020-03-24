package com.loh.creatures;

import com.loh.shared.Properties;

public class CreatureStatus {

    public static Integer baseLife = 5;
    public static Integer lifeLevelMultiplier = 4;
    public static Integer baseMoral = 5;
    public static Integer moralLevelMultiplier = 4;

    public static Integer specialAttack(Creature creature) {
        Integer gloveBonus = creature.equipment.getGloves().getBonus();
        Integer creatureBonus = creature.getBonusLevel(Properties.SpecialAttack);
        return gloveBonus + creatureBonus;
    }
    public static Integer magicDefense(Creature creature) {
        Integer beltBonus = creature.equipment.getBelt().getBonus();
        Integer creatureBonus = creature.getBonusLevel(Properties.MagicDefense);
        return beltBonus + creatureBonus;
    }
    public static Integer dodge(Creature creature) {
        Integer dodge = creature.equipment.getDodge();
        return dodge;
    }
    public static Integer specialPower(Creature creature) {
        Integer attributeLevel = creature.getAttributeLevel(creature.getSpecialPowerMainAttribute());
        Integer ringRightBonus = creature.equipment.getRingRight().getBonus() * 2;
        Integer ringLeftBonus = creature.equipment.getRingLeft().getBonus() * 2;
        return attributeLevel + ringLeftBonus + ringRightBonus;
    }
    public static Integer life(Creature creature) {
        return baseLife + lifeLevelMultiplier * creature.level + (creature.level  + 2) * creature.getAttributeLevel(Attributes.Vitality);
    }
    public static Integer moral(Creature creature) {
        return baseMoral + moralLevelMultiplier * creature.level + (creature.level  + 2) * creature.getAttributeLevel(Attributes.Intuition) / 2;
    }
}
