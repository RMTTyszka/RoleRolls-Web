package com.loh.creatures;

import com.loh.combat.AttackDetails;
import com.loh.combat.AttackService;
import com.loh.creatures.equipment.Equipment;
import com.loh.creatures.inventory.Inventory;
import com.loh.dev.Loh;
import com.loh.effects.EffectInstance;
import com.loh.effects.EffectProcessor;
import com.loh.items.ItemInstanceRepository;
import com.loh.race.Race;
import com.loh.role.Role;
import com.loh.shared.Entity;
import com.loh.shared.*;
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
        level = 1;
        this.baseAttributes = new Attributes();
        this.bonusAttributes = new Attributes();
        this.bonuses = new ArrayList<>();
        this.race = new Race();
        this.role = new Role();
        this.equipment = new Equipment();
        setCurrentLife(getStatus().getLife());
        setCurrentMoral(getStatus().getMoral());
    }

    protected CreatureType getCreatureType() {
        return null;
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


    public CreatureStatus getStatus() {
        return new CreatureStatus(this);
    }
    @Getter
    private Integer currentLife;
    @Getter
    private Integer currentMoral;
    public void setCurrentLife(Integer val) {
        if (currentLife == null) {
            currentLife = getStatus().getLife();
        }
        currentLife = val;
        currentLife = Integer.min(currentLife, getStatus().getLife());
    }
    public void setCurrentMoral(Integer val) {
        if (currentMoral == null) {
            currentMoral = getStatus().getMoral();
        }
        currentMoral = val;
        currentMoral = Integer.max(currentMoral, 0);
        currentMoral = Integer.min(currentMoral, getStatus().getMoral());

    }
    @Getter @Setter
    private Integer manaSpent;
    @Getter @Setter
    private String specialPowerMainAttribute = Attributes.Strength;

    public Attributes getTotalAttributes(){
        Attributes totalAttributes = bonusAttributes.GetSumOfAttributes(baseAttributes);
        totalAttributes.strength += getAttributeLevel(Attributes.Strength);
        totalAttributes.agility += getAttributeLevel(Attributes.Agility);
        totalAttributes.vitality += getAttributeLevel(Attributes.Vitality);
        totalAttributes.wisdom += getAttributeLevel(Attributes.Wisdom);
        totalAttributes.intuition += getAttributeLevel(Attributes.Intuition);
        totalAttributes.charisma += getAttributeLevel(Attributes.Charisma);

        return totalAttributes;
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

    public Integer getBonusLevel(String property) {
        Integer creatureRaceBonus = Bonuses.GetInnateBonusLevel(getRace().getBonuses(), property);
        Integer creatureRoleBonus = Bonuses.GetInnateBonusLevel(getRole().getBonuses(), property);
        Integer equipmentBonus = equipment.getBonusLevel(property);
        Integer creatureMagicalBonus = Bonuses.GetMagicalBonusLevel(bonuses, property);
        Integer creatureMoralBonus = Bonuses.GetMoralBonusLevel(bonuses, property);
        return creatureRaceBonus + creatureRoleBonus + equipmentBonus + creatureMagicalBonus + creatureMoralBonus;
    }

    @ElementCollection
    @CollectionTable()
    @Getter
    @Setter
    private List<EffectInstance> effects;
    public void updateEffect(EffectInstance effect) {
        effects = EffectProcessor.updateEffect(effects, effect);
    }
    public void removeEffect(EffectInstance effect) {
        effects = EffectProcessor.removeEffect(effects, effect);
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
        bonuses.add(new Bonus(Attributes.Strength, (level / 5) * 5, 0, BonusType.Innate));
        bonuses.add(new Bonus(Attributes.Agility, (level / 5) * 5, 0, BonusType.Innate));
        bonuses.add(new Bonus(Attributes.Vitality, (level / 5) * 5, 0, BonusType.Innate));
        bonuses.add(new Bonus(Attributes.Wisdom, (level / 5) * 5, 0, BonusType.Innate));
        bonuses.add(new Bonus(Attributes.Intuition, (level / 5) * 5, 0, BonusType.Innate));
        bonuses.add(new Bonus(Attributes.Charisma, (level / 5) * 5, 0, BonusType.Innate));
        this.level = level;
        this.currentLife = this.getStatus().getLife();
        this.currentMoral = this.getStatus().getMoral();
        creatureRepository.save(this);
    }
    public void levelUp(List<String> attributesToLevel) {
        level++;
        for (String attribute: attributesToLevel) {
            bonusAttributes.levelUp(attribute);
        }
    }

    public Integer getInnateLevelBonus(Integer attributePoints) {
        return (attributePoints - 5) / 5 * 2;
    }
    public Integer getEvasionInnateBonus() {
        return level/2;
    }

    public Integer takeDamage(Integer damage) {
        Integer reducedDamage = damage - getStatus().getDefense();
        reducedDamage = Integer.max(reducedDamage, 1);
        Integer remainingDamage = reducedDamage;
        if (currentMoral > 0) {
            remainingDamage -= currentMoral;
            setCurrentMoral(currentMoral - reducedDamage);
        }
        if (remainingDamage > 0) {
            setCurrentLife(currentLife - remainingDamage);
        }

        if (currentLife <= 0) {
            this.updateEffect(DeathProcessor.getDeathEffect(currentLife));
        }

        return reducedDamage;
    }

    public Creature processEndOfTurn(CreatureRepository creatureRepository) {
        return creatureRepository.save(this);
    }



}
