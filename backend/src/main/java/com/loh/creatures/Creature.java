package com.loh.creatures;

import com.loh.combat.AttackDetails;
import com.loh.combat.AttackService;
import com.loh.creatures.heroes.equipment.Equipment;
import com.loh.creatures.heroes.inventory.Inventory;
import com.loh.dev.Loh;
import com.loh.items.ItemInstanceRepository;
import com.loh.race.Race;
import com.loh.role.Role;
import com.loh.shared.Bonus;
import com.loh.shared.Bonuses;
import com.loh.shared.Entity;
import com.loh.shared.Properties;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import java.util.ArrayList;
import java.util.List;

@javax.persistence.Entity
@Inheritance(strategy = InheritanceType.SINGLE_TABLE)
@DiscriminatorColumn(name = "CreatureType")
public class Creature extends Entity {

    public Creature() {
        this.bonuses = new ArrayList<>();
    }

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


    public Resistances getResistances() {
        Integer globalResistanceBonus = getBonusLevel(Properties.Resistance) + equipment.getHeadpiece().getBonus();
        Integer fear = getBonusLevel(Resistances.Fear);
        fear += globalResistanceBonus;
        Integer health = getBonusLevel(Resistances.Health);
        health += globalResistanceBonus;
        Integer magic = getBonusLevel(Resistances.Magic);
        magic += globalResistanceBonus;
        Integer physical = getBonusLevel(Resistances.Physical);
        physical += globalResistanceBonus;
        Integer reflex = getBonusLevel(Resistances.Reflex);
        reflex += globalResistanceBonus;
        return new Resistances(fear, health, magic, physical, reflex);
    }

    public Integer getMana() {
        return level / 5 + 1 + equipment.getNeckAccessory().getManaBonus();
    }

    @Getter @Setter
    private Integer manaSpent;
    @Getter @Setter
    private String specialPowerMainAttribute;

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

    @ElementCollection
    @CollectionTable()
    @Getter
    @Setter
    private List<Bonus> bonuses;

    public Integer getDefense() {
        return CreatureStatus.defense(this);
    }
    public Integer getEvasion() {
        return CreatureStatus.evasion(this);
    }
    public Integer getDodge() {
        return CreatureStatus.dodge(this);
    }
    public Integer getSpecialAttack() {
        return CreatureStatus.specialAttack(this);
    }
    public Integer getMagicDefense() {
        return CreatureStatus.magicDefense(this);
    }
    public Integer getSpecialPower() {
        return CreatureStatus.specialPower(this);
    }

    public Integer getBonusLevel(String property) {
        Integer creatureRaceBonus = Bonuses.GetInnateBonusLevel(getRace().getBonuses(), property);
        Integer creatureRoleBonus = Bonuses.GetInnateBonusLevel(getRole().getBonuses(), property);
        Integer equipmentBonus = equipment.getBonusLevel(property);
        Integer creatureMagicalBonus = Bonuses.GetMagicalBonusLevel(bonuses, property);
        Integer creatureMoralBonus = Bonuses.GetMoralBonusLevel(bonuses, property);
        return creatureRaceBonus + creatureRoleBonus + equipmentBonus + creatureMagicalBonus + creatureMoralBonus;
    }

    public Integer getLife() {
        return CreatureStatus.life(this);
    }
    public Integer getMoral() {
        return CreatureStatus.moral(this);
    }

    public Integer getMainWeaponHitBonus() {
        if (equipment.getMainWeapon() != null) {
            Integer attributeBonus =  getAttributeLevel(equipment.getMainWeapon().getWeaponModel().getBaseWeapon().getHitAttribute());
            Integer bonuses = getBonusLevel(equipment.getMainWeapon().getWeaponModel().getBaseWeapon().getHitAttribute());
            return attributeBonus + bonuses;
        }
        return 0;
    }
    public Integer getOffWeaponHitBonus() {
        if (equipment.getOffWeapon() != null) {
            Integer attributeBonus =  getAttributeLevel(equipment.getOffWeapon().getWeaponModel().getBaseWeapon().getHitAttribute());
            Integer bonuses = getBonusLevel(equipment.getOffWeapon().getWeaponModel().getBaseWeapon().getHitAttribute());
            return attributeBonus + bonuses;
        }
        return 0;
    }

    public WeaponAttributes getMainWeaponAttributes() {
        return equipment.getMainWeapon() != null ? new WeaponAttributes(
                equipment.getMainWeaponGripType(),
                getAttributeLevel(equipment.getMainWeapon().getWeaponModel().getBaseWeapon().getDamageAttribute()),
                getMainWeaponHitBonus(),
                equipment.getMainWeapon().getBonus(),
                equipment.getOffWeaponGridType()) : null;
    }
    public WeaponAttributes getOffWeaponAttributes() {
        return equipment.getOffWeapon() != null ? new WeaponAttributes(
                equipment.getOffWeaponGridType(),
                getAttributeLevel(equipment.getOffWeapon().getWeaponModel().getBaseWeapon().getDamageAttribute()),
                getOffWeaponHitBonus(),
                equipment.getOffWeapon().getBonus(),
                equipment.getMainWeaponGripType()) : null;
    }

    public Integer getAttributePoints(String attr) {
        Integer base = baseAttributes.getAttributePoints(attr);
        Integer attributeBonusPoints = bonusAttributes.getAttributePoints(attr);
        Integer raceAttribute = race.getAttributePoints(attr);
        Integer roleAttribute = role.getAttributePoints(attr);
        Integer bonusAttribute = getBonusLevel(attr);
        return  base + attributeBonusPoints + raceAttribute + roleAttribute + bonusAttribute;
    }

    public Integer getAttributeLevel(String attr) {
        return Loh.getLevel(getAttributePoints(attr));
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

    public Integer getExpToNextLevel(){
        return CreatureLevel.calculateExpToNextLevel(level);
    }

    public AttackDetails fullAttack(Creature target, AttackService service) {
        return service.fullAttack(this, target);
    }

    public void levelUpforTest(CreatureRepository creatureRepository, ItemInstanceRepository itemInstanceRepository, Integer level) {
        for (int levelUp = this.level; levelUp <= level ; levelUp++) {
            bonusAttributes.levelUp(Attributes.Agility);
            bonusAttributes.levelUp(Attributes.Strength);
            bonusAttributes.levelUp(Attributes.Vitality);
            bonusAttributes.levelUp(Attributes.Charisma);
            bonusAttributes.levelUp(Attributes.Intuition);
            bonusAttributes.levelUp(Attributes.Wisdom);
            equipment.getMainWeapon().levelUp(itemInstanceRepository);
            itemInstanceRepository.save(equipment.getMainWeapon());
            if (equipment.getOffWeapon() != null) {
                equipment.getOffWeapon().levelUpForTest(itemInstanceRepository);
            }
            equipment.getArmor().levelUpForTest(itemInstanceRepository);
            equipment.getGloves().levelUpForTest(itemInstanceRepository);
            equipment.getBelt().levelUpForTest(itemInstanceRepository);
            equipment.getHeadpiece().levelUpForTest(itemInstanceRepository);
        }
        bonuses.add(new Bonus(Attributes.Strength, (level / 5) * 5, 0));
        bonuses.add(new Bonus(Attributes.Agility, (level / 5) * 5, 0));
        bonuses.add(new Bonus(Attributes.Vitality, (level / 5) * 5, 0));
        bonuses.add(new Bonus(Attributes.Wisdom, (level / 5) * 5, 0));
        bonuses.add(new Bonus(Attributes.Intuition, (level / 5) * 5, 0));
        bonuses.add(new Bonus(Attributes.Charisma, (level / 5) * 5, 0));
        this.level = level;
        creatureRepository.save(this);
    }
    public void levelUp(List<String> attributesToLevel) {
        level++;
        for (String attribute: attributesToLevel) {
            bonusAttributes.levelUp(attribute);
        }
    }

    public Integer getInateLevelBonus(Integer attributePoints) {
        return (attributePoints - 5) / 5 * 2;
    }
    public Integer getEvasionInnateBonus() {
        return level/2;
    }



}
