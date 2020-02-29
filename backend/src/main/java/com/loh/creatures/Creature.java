package com.loh.creatures;

import com.loh.dev.Loh;
import com.loh.creatures.heroes.equipment.Equipment;
import com.loh.creatures.heroes.inventory.Inventory;
import com.loh.race.Race;
import com.loh.role.Role;
import com.loh.shared.Entity;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;

@MappedSuperclass
public class Creature extends Entity {
    @Embedded
    @Getter @Setter
    @AttributeOverrides
            ({
                    @AttributeOverride(name="strength", column = @Column(name="base_strength") ),
                    @AttributeOverride(name="agility", column = @Column(name="base_agility") ),
                    @AttributeOverride(name="vitality", column = @Column(name="base_vitality") ),
                    @AttributeOverride(name="wisdom", column = @Column(name="base_wisdom") ),
                    @AttributeOverride(name="intuition", column = @Column(name="base_intuition") ),
                    @AttributeOverride(name="charisma", column = @Column(name="base_charisma") )})
    protected Attributes baseAttributes;

    @Embedded
    @Getter @Setter
    @AttributeOverrides
            ({
                    @AttributeOverride(name="strength", column = @Column(name="bonus_strength") ),
                    @AttributeOverride(name="agility", column = @Column(name="bonus_agility") ),
                    @AttributeOverride(name="vitality", column = @Column(name="bonus_vitality") ),
                    @AttributeOverride(name="wisdom", column = @Column(name="bonus_wisdom") ),
                    @AttributeOverride(name="intuition", column = @Column(name="bonus_intuition") ),
                    @AttributeOverride(name="charisma", column = @Column(name="bonus_charisma") )})
    protected Attributes bonusAttributes;

    @Transient
    protected Attributes totalAttributes;
    protected Attributes getTotalAttributes(){
        return bonusAttributes.GetSumOfAttributes(baseAttributes);
    }

    @Getter @Setter
    protected Integer level;

    @Getter @Setter
    protected String name;

    @Getter @Setter @ManyToOne
    protected Race race;

    @Getter @Setter @ManyToOne
    protected Role role;

    @Getter @Setter @OneToOne
    protected Equipment equipment;

    @Getter @Setter @OneToOne
    protected Inventory inventory;

    public Integer getDefense() {
        return equipment.getDefense() + getAttributeLevel(Attributes.Strength);
    }
    public Integer getEvasion() {
        return 10 + equipment.getEvasion();
    }
    public Integer life() {
        return 5 + 4 * level +  (level  + 2) * getTotalAttributes().vitality;
    }

    public Integer getAttributeLevel(String attr) {
        return
                baseAttributes.getAttributeLevel(attr)
                + bonusAttributes.getAttributeLevel(attr)
                + race.getAttributeLevel(attr)
                + role.getAttributeLevel(attr);
    }

    public Integer getAttributeModifier(String attr) {
        return Loh.getModifier(getAttributeLevel(attr));
    }

    @Transient
    protected Integer totalAttributesInitialPoints;
    public Integer getTotalAttributesInitialPoints(){
        return 8 + 6 + 4 + 2 + 2 + 8*6;
    }

    @Transient
    protected Integer maxInitialAttributePoints;
    public Integer getMaxInitialAttributePoints(){
        return 8+6;
    }
    @Transient
    protected Integer maxAttributeBonusPoints;
    public Integer getMaxAttributeBonusPoints(){
        return level - 1;
    }
    @Transient
    protected Integer totalAttributesBonusPoints;
    public Integer getTotalAttributesBonusPoints(){
        return (level - 1) * 2;
    }
    @Transient
    protected Integer expToNextLevel;
    public Integer getExpToNextLevel(){
        return calculateExpToNextLevel(level);
    }

    public static Integer getExpToNextLevel(Integer level){
        return calculateExpToNextLevel(level);
    }
    public static Integer calculateExpToNextLevel(Integer level) {
        if (level > 1) {
            return calculateExpToNextLevel(level - 1) +
                    5 * x(level);
        } else {
            return 500;
        }
    }
    public static Integer x (Integer level) {
        if (level > 1) {
            return x(level - 1) + 50 * (level - 1);
        } else {
            return 100;
        }
    }





}
