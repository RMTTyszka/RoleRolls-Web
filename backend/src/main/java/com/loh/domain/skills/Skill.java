package com.loh.domain.skills;

import com.loh.shared.Entity;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.DiscriminatorColumn;
import javax.persistence.DiscriminatorType;
import javax.persistence.Inheritance;
import javax.persistence.InheritanceType;
import java.lang.reflect.Field;
import java.util.Arrays;
import java.util.List;

@javax.persistence.Entity
@Inheritance(strategy = InheritanceType.SINGLE_TABLE)
@DiscriminatorColumn(name = "MainSkill", discriminatorType = DiscriminatorType.STRING)
public abstract class Skill extends Entity {

    public Skill() {
        level = 1;
    }

    @Getter @Setter
    private Integer level;
    @Getter @Setter
    private Integer points = 0;


    public Integer getMaxPointPerSkill() {
        return level / 2 + 3;
    }
    public Integer getMaxPointPerSkill(Integer level) {
        return level / 2 + 2;
    }
    public Integer getMaxTotalMajorPoints() {
        return level;
    }

    public Integer getTotalMinorPoints() {
        return points * 6;
    }
    public Integer getRemainingMinorPoints() {
        return getTotalMinorPoints() - getUsedMinorPoints();
    }
    public boolean hasRemainingMinorPoints() {
        return getRemainingMinorPoints() > 0;
    }
    public boolean canRemoveMajorSkillPoint() {
        return !getList().stream().anyMatch(skill -> getMinorSkillPoint(skill) >= getMaxPointPerSkill(level - 1))
                && getRemainingMinorPoints() >= 6;
    }

    public void levelUp() {
        this.level++;
    }
    public void addPoint() {
        if (canAddMajorPoint()) {
            this.points++;
        }
    }
    public void removePoint() {
        this.points--;
    }
    public void addPoint(String skillName) {
        try {
            Field field = this.getClass().getDeclaredField(skillName);
            field.setAccessible(true);
            Integer points = (Integer) field.get(this);
            points++;
            field.set(this, points);
        } catch (NoSuchFieldException e) {
            e.printStackTrace();
        } catch (IllegalAccessException e) {
            e.printStackTrace();
        }
    }

    public void removePoint(String skillName) {
        try {
            Field field = this.getClass().getDeclaredField(skillName);
            field.setAccessible(true);
            Integer points = (Integer) field.get(this);
            points--;
            field.set(this, points);
        } catch (NoSuchFieldException e) {
            e.printStackTrace();
        } catch (IllegalAccessException e) {
            e.printStackTrace();
        }
    }

    public Integer getMinorSkillPoint(String skillName) {
        try {
            Field field = this.getClass().getDeclaredField(skillName);
            field.setAccessible(true);
            Integer value = (Integer)field.get(this);
            return value;
        } catch (NoSuchFieldException e) {
            e.printStackTrace();
        } catch (IllegalAccessException e) {
            e.printStackTrace();
        }
        return 0;
    }
    public boolean canAddMajorPoint() {
        return this.points < this.getMaxTotalMajorPoints();

    }
    public boolean canAddMinorPoint(String skillName) {
        return getMinorSkillPoint(skillName) < getMaxPointPerSkill();

    }

    protected abstract Integer getUsedMinorPoints();
    protected abstract List<String> getList();

    public static List<String> getListOld(){
        return Arrays.asList("jump", "climb", "athleticism", "steal", "stealth",
                "operateMechanisms", "diplomacy", "bluff", "intimidate", "arcane","religion",
                "nature","perceive","feeling","search","perceive","feeling","search",
                "mysticism","toughness","reflex", "attack","specialAttack","power", "evasion");
    }

}
