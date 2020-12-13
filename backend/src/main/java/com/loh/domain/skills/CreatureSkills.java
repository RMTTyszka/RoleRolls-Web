package com.loh.domain.skills;

import com.loh.shared.Entity;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.OneToOne;
import java.util.Arrays;
import java.util.List;

@javax.persistence.Entity
public class CreatureSkills extends Entity {
    public CreatureSkills() {
        this.setLevel(1);
        sports = new Sports();
        nimbleness = new Nimbleness();
        knowledge = new Knowledge();
        combat = new CombatSkill();
        perception = new Perception();
        resistance = new Resistance();
        relationship = new Relationship();
    }

    @Getter @Setter @OneToOne
    private Sports sports;
    @Getter @Setter @OneToOne
    private Nimbleness nimbleness;
    @Getter @Setter @OneToOne
    private Knowledge knowledge;
    @Getter @Setter @OneToOne
    private CombatSkill combat;
    @Getter @Setter @OneToOne
    private Perception perception;
    @Getter @Setter @OneToOne
    private Resistance resistance;
    @Getter @Setter @OneToOne
    private Relationship relationship;
    @Getter @Setter
    private Integer level;
    public void levelUp() {
        this.level++;
        this.getSports().levelUp();
        this.getKnowledge().levelUp();
        this.getCombat().levelUp();
        this.getNimbleness().levelUp();
        this.getPerception().levelUp();
        this.getRelationship().levelUp();
        this.getResistance().levelUp();
    }
    public Integer getMaxPoints() {
        return this.level * 3;
    }
    public Integer getRemainingPoints() {
        return getMaxPoints() - sports.getPoints() - nimbleness.getPoints() - relationship.getPoints() - knowledge.getPoints() - perception.getPoints() - resistance.getPoints() - combat.getPoints();
    }
    public List<String> getSkillsList(){
        return Arrays.asList("sports", "nimbleness", "relationship", "knowledge", "perception",
                "resistance", "combat");
    }
    public Skill getMajorSkill(String skill){
        switch (skill) {
            case "sports":
                return sports;
            case "nimbleness":
                return nimbleness;
            case "relationship":
                return relationship;
            case "knowledge":
                return knowledge;
            case "perception":
                return perception;
            case "resistance":
                return resistance;
            case "combat":
                return combat;
            default:
                return null;
        }
    }
    public Skill getSkillByMinorSkill(String skill){
        switch (skill) {
            case "jump":
            case "climb":
            case "athleticism":
                return sports;
            case "steal":
            case "stealth":
            case "operateMechanisms":
                return nimbleness;
            case "diplomacy":
            case "bluff":
            case "intimidate":
                return relationship;
            case "arcane":
            case "religion":
            case "nature":
                return knowledge;
            case "perceive":
            case "feeling":
            case "search":
                return perception;
            case "mysticism":
            case "toughness":
            case "reflex":
                return resistance;
            case "attack":
            case "specialAttack":
            case "power":
            case "evasion":
                return combat;
            default:
                return null;
        }
    }

    public List<Skill> mainSKillsList(){
        return Arrays.asList(sports, nimbleness, relationship, knowledge, perception,
                resistance, combat);
    }
}
