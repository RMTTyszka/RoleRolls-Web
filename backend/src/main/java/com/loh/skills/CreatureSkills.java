package com.loh.skills;

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
    }
    public Integer getMaxPoints() {
        return this.level * 3;
    }
    public Integer getRemainingPoints() {
        return getMaxPoints() - sports.getPoints() - nimbleness.getPoints() - relationship.getPoints() - knowledge.getPoints() - perception.getPoints() - resistance.getPoints() - combat.getPoints();
    }
    public static List<String> getSkillNamesList(){
        return Arrays.asList("sports", "nimbleness", "relationship", "knowledge", "perception",
                "resistance", "combat");
    }    
    public List<Skill> getList(){
        return Arrays.asList(sports, nimbleness, relationship, knowledge, perception,
                resistance, combat);
    }
}
