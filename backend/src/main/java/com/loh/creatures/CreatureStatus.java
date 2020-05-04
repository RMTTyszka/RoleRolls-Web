package com.loh.creatures;

import com.loh.shared.Properties;
import lombok.Getter;

import java.util.UUID;

public class CreatureStatus {

    @Getter
    private UUID ownerId;
    @Getter
    private Integer specialAttack;
    @Getter
    private Integer magicDefense;
    @Getter
    private Integer dodge;
    @Getter
    private Integer specialPower;
    @Getter
    private Integer life;

    @Getter
    private Integer moral;

    @Getter
    private Integer evasion;
    @Getter
    private Integer defense;
    @Getter
    private Integer mana;

    public CreatureStatus() {
    }

    public CreatureStatus(Creature creature) {
        this.ownerId = creature.getId();
        specialAttack = getSpecialAttack(creature);
        this.magicDefense = getMagicDefense(creature);
        this.dodge = getDodge(creature);
        this.specialPower = getSpecialPower(creature);
        this.life = getLife(creature);
        this.moral = getMoral(creature);
        this.evasion = getEvasion(creature);
        this.defense = getDefense(creature);
        this.mana = getMana(creature);
    }

    private static Integer baseLife = 5;
    private static Integer lifeLevelMultiplier = 4;
    private static Integer baseMoral = 5;
    private static Integer moralLevelMultiplier = 4;


    public static Integer getMana(Creature creature) {
        return creature.level / 5 + 1 + creature.equipment.getNeckAccessory().getManaBonus();
    }
    public static Integer getSpecialAttack(Creature creature) {
        Integer gloveBonus = (creature.equipment != null && creature.equipment.getGloves() != null) ? creature.equipment.getGloves().getBonus() : 0;
        Integer creatureBonus = creature.getBonusLevel(Properties.SpecialAttack);
        return gloveBonus + creatureBonus;
    }
    public static Integer getMagicDefense(Creature creature) {
        Integer beltBonus = (creature.equipment != null && creature.equipment.getBelt() != null) ? creature.equipment.getBelt().getBonus() : 0;
        Integer creatureBonus = creature.getBonusLevel(Properties.MagicDefense);
        return beltBonus + creatureBonus;
    }
    public static Integer getDodge(Creature creature) {
        Integer dodge = (creature.equipment != null) ? creature.equipment.getDodge() : 0;
        return dodge;
    }
    public static Integer getSpecialPower(Creature creature) {
        Integer attributeLevel = creature.getAttributeLevel(creature.getSpecialPowerMainAttribute());
        Integer ringRightBonus = creature.equipment.getRingRight().getBonus() * 2;
        Integer ringLeftBonus = creature.equipment.getRingLeft().getBonus() * 2;
        return attributeLevel + ringLeftBonus + ringRightBonus;
    }
    public static Integer getLife(Creature creature) {
        return baseLife + lifeLevelMultiplier * creature.level + (creature.level  + 2) * creature.getAttributeLevel(Attributes.Vitality);
    }
    public static Integer getMoral(Creature creature) {
        return baseMoral + moralLevelMultiplier * creature.level + (creature.level  + 2) * creature.getAttributeLevel(Attributes.Intuition) / 2;
    }
    public static Integer getEvasion(Creature creature) {
        return 10 + creature.equipment.getEvasion() +  creature.getAttributeLevel(Attributes.Agility) + creature.getEvasionInnateBonus();
    }
    public static Integer getDefense(Creature creature) {
        return creature.equipment.getDefense() + creature.getAttributeLevel(Attributes.Vitality);
    }

}
