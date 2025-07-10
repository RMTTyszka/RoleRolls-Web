package com.rolerolls.domain.creatures;

import com.rolerolls.domain.combats.AttackDetails;
import com.rolerolls.domain.combats.AttackService;
import com.rolerolls.domain.creatures.equipments.Equipment;
import com.rolerolls.domain.creatures.equipments.GripType;
import com.rolerolls.domain.creatures.equipments.services.dtos.EquipItemValidationType;
import com.rolerolls.domain.creatures.inventory.Inventory;
import com.rolerolls.domain.effects.EffectInstance;
import com.rolerolls.domain.effects.EffectProcessor;
import com.rolerolls.domain.items.equipables.armors.instances.ArmorInstance;
import com.rolerolls.domain.items.equipables.weapons.instances.WeaponInstance;
import com.rolerolls.domain.items.instances.ItemInstanceRepository;
import com.rolerolls.domain.races.Race;
import com.rolerolls.domain.roles.Role;
import com.rolerolls.domain.skills.CreatureSkills;
import com.rolerolls.shared.Entity;
import com.rolerolls.shared.*;
import com.rolerolls.system.Loh;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import java.util.List;
import java.util.UUID;

@javax.persistence.Entity
@Inheritance(strategy = InheritanceType.SINGLE_TABLE)
@DiscriminatorColumn(name = "CreatureType")
public class Creature extends Entity {

    public Creature() {
        race = new Race();
        role = new Role();
        baseAttributes = new Attributes(8);
        bonusAttributes = new Attributes();
        skills = new CreatureSkills();
        equipment = new Equipment();
        inventory = new Inventory();
        bonuses = new Bonuses();
    }

    @Getter @Setter
    protected String name;

    @Getter @Setter
    @Column(columnDefinition = "BINARY(16)")
    protected UUID ownerId;
    @Getter @Setter
    @Column(columnDefinition = "BINARY(16)")
    protected UUID creatorId;

    public Creature(String name, Race race, Role role, UUID playerId, UUID creatorId) {
        super();
        id = UUID.randomUUID();
        level = 1;
        baseAttributes = new Attributes(8);
        bonusAttributes = new Attributes();
        skills = new CreatureSkills();
        this.name = name;
        this.race = race;
        this.role = role;
        equipment = new Equipment();
        inventory = new Inventory();
        this.ownerId = playerId;
        this.creatorId = creatorId;
        bonuses = new Bonuses();
        setCurrentLife(this.getStatus().getLife());
        setCurrentMoral(this.getStatus().getMoral());
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

    @Transient
    private CreatureStatus statuses;

    public CreatureStatus getStatus() {
        if (this.statuses == null) {
            statuses = new CreatureStatus(this);
        }
        return this.statuses;
    }
    @Getter
    private Integer currentLife;
    @Getter
    private Integer currentMoral;
    public void setCurrentLife(Integer val) {
        currentLife = val;
        currentLife = Integer.min(currentLife, getStatus().getLife());
    }
    public void setCurrentMoral(Integer val) {
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
    protected Integer level = 1;

    @Getter @Setter @ManyToOne
    protected Race race;

    @Getter @Setter @ManyToOne
    protected Role role;
    @Getter @Setter @OneToOne
    protected CreatureSkills skills;
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
        Integer raceBonus = Bonuses.GetInnateBonusLevel(getRace().getBonuses(), property);
        Integer roleBonus = Bonuses.GetInnateBonusLevel(getRole().getBonuses(), property);
        Integer equipmentBonus = equipment.getBonusLevel(property);
        Integer creatureMagicalBonus = Bonuses.GetMagicalBonusLevel(bonuses, property);
        Integer creatureInnateBonus = Bonuses.GetInnateBonusLevel(bonuses, property);
        Integer creatureMoralBonus = Bonuses.GetMoralBonusLevel(bonuses, property);
        return raceBonus + roleBonus + equipmentBonus + creatureMagicalBonus + creatureMoralBonus + creatureInnateBonus;
    }
    public Integer getBonus(String property) {
        Integer raceBonus = Bonuses.GetInnateBonus(getRace().getBonuses(), property);
        Integer roleBonus = Bonuses.GetInnateBonus(getRole().getBonuses(), property);
        Integer equipmentBonus = equipment.getBonus(property);
        Integer creatureMagicalBonus = Bonuses.GetMagicalBonus(bonuses, property);
        Integer creatureInnateBonus = Bonuses.GetInnateBonus(bonuses, property);
        Integer creatureMoralBonus = Bonuses.GetMoralBonus(bonuses, property);
        return raceBonus + roleBonus + equipmentBonus + creatureMagicalBonus + creatureMoralBonus + creatureInnateBonus;
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
            Integer bonuses = getBonusLevel(Properties.Hit);
            return attributeBonus + bonuses;
        }
        return 0;
    }
    public Integer getOffWeaponHitBonus() {
        if (equipment.getOffWeapon() != null) {
            Integer attributeBonus =  getAttributeLevel(equipment.getOffWeapon().getWeaponModel().getBaseWeapon().getHitAttribute());
            Integer bonuses = getBonusLevel(Properties.Hit);
            return attributeBonus + bonuses;
        }
        return 0;
    }
    @Transient
    private WeaponAttributes _mainWeaponAttributes;

    public WeaponAttributes getMainWeaponAttributes() {
        if (_mainWeaponAttributes == null && equipment.getMainWeapon() != null) {
            _mainWeaponAttributes = new WeaponAttributes(
                    equipment.getMainWeaponGripType(),
                    getAttributeLevel(equipment.getMainWeapon().getWeaponModel().getBaseWeapon().getDamageAttribute()),
                    getMainWeaponHitBonus(),
                    equipment.getMainWeapon().getBonus(),
                    equipment.getOffWeaponGripType());
        } else if (_mainWeaponAttributes == null) {
            _mainWeaponAttributes = new WeaponAttributes();
        }
        return _mainWeaponAttributes;
    }
    @Transient
    private WeaponAttributes _offWeaponAttributes;

    public WeaponAttributes getOffWeaponAttributes() {
        if (_offWeaponAttributes == null && equipment.getOffWeapon() != null && equipment.getOffWeaponGripType() != null) {
            _offWeaponAttributes = new WeaponAttributes(
                    equipment.getOffWeaponGripType(),
                    getAttributeLevel(equipment.getOffWeapon().getWeaponModel().getBaseWeapon().getDamageAttribute()),
                    getOffWeaponHitBonus(),
                    equipment.getOffWeapon().getBonus(),
                    equipment.getMainWeaponGripType());
        } else if (_offWeaponAttributes == null){
            _offWeaponAttributes = new WeaponAttributes();
        }
        return _offWeaponAttributes;
    }

    public Integer getAttributePoints(String attr) {
        Integer base = baseAttributes.getAttributePoints(attr);
        Integer attributeBonusPoints = bonusAttributes.getAttributePoints(attr);
        Integer bonus = getBonusLevel(attr);
        return  base + attributeBonusPoints + bonus;
    }
    public Integer getPropertyPoints(String property) {
        if (Attributes.getList().contains(property)){
            return getAttributePoints(property);
        }
        return 0;
    }
    public Integer getPropertyBonus(String property) {
        if (Attributes.getList().contains(property)){
            return getBonusLevel(property);
        }
        return 0;
    }

    public Integer getAttributeLevel(String attr) {
        return Loh.getLevel(getAttributePoints(attr));
    }

    @Transient
    protected Integer totalAttributesInitialPoints;
    public Integer getTotalAttributesInitialPoints(){
        return 8 + 6 + 4 + 2 + 2 + 8*6 + (getRace().isHuman() ? 2 : 0);
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
        for (int levelUp = this.level; levelUp < level ; levelUp++) {
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
    public void levelUp() {
        level++;
        skills.levelUp();
    }
    public Integer getInnateLevelBonus(Integer attributePoints) {
        return 0;
    }
    public Integer getEvasionInnateBonus() {
        return 0;
    }
    public void heal(Integer value) {
        Integer lifeToHeal = getStatus().getLife() - currentLife;
        Integer remainingValue = value;
        if (lifeToHeal > 0) {
            remainingValue -= lifeToHeal;
            setCurrentLife(currentLife + value);
        }
        Integer moralToHeal = getStatus().getMoral() - currentMoral;
        if (remainingValue > 0 && moralToHeal > 0) {
            setCurrentMoral(currentMoral + remainingValue);
        }

        if (currentLife > 0) {
            this.removeEffect(DeathProcessor.getDeathEffect(0));
        }
    }

    public Integer getDamageDefense(DamageType damageType) {
        switch (damageType) {
            case Physical:
                return getStatus().getDefense();
            case Arcane:
                return getStatus().getArcaneDefense();
            case Fire:
                return getStatus().getFireDefense();
            case Ice:
                return getStatus().getIceDefense();
            case Lightning:
                return getStatus().getLife();
            case Poison:
                return getStatus().getPoisonDefense();
            case Necrotic:
                return getStatus().getNecroticDefense();
            case Holy:
                return getStatus().getHolyDefense();
            case Sonic:
                return getStatus().getSonicDefense();
            default:
                return 0;
        }
    }
    public Integer takeDamage(Integer damage, DamageType damageType) {
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

    public Creature processEndOfTurn() {
        for (Bonus bonus : bonuses) {
            bonus.processEndOfTurn();
        }
        this.bonuses.removeIf(b -> b.getRemainingTurns() <= 0 );
        return this;
    }

    public void addBonus(Bonus bonus) {
        this.bonuses.add(bonus);
    }
    public void updateBonus(Bonus bonus) {
        Bonus previousBonus = bonuses.stream().filter(b -> b.getId().equals(bonus.getId())).findFirst().get();
        previousBonus.update(bonus);
    }
    public void removeBonus(Bonus bonus) {
        this.bonuses.removeIf(b -> b.getId().equals(bonus.getId()));
    }

    public void equipArmor(ArmorInstance armorInstance) {
        ArmorInstance removedArmor = equipment.equipArmor(armorInstance);
        getInventory().removeItem(armorInstance);
        getInventory().addItem(removedArmor);
    }
    public void equipMainWeapon(WeaponInstance weaponInstance) throws Exception {
        GripType gripType = GripType.getGripType(GripType.getGripTypeByHandleType(weaponInstance.getWeaponModel().getBaseWeapon().getCategory()), equipment.getOffWeaponGripType());
        if (gripType == null) {
            throw new Exception("That weapon cannot be equiped because of the other hand");
        }
        WeaponInstance removedWeapon = equipment.equipMainWeapon(weaponInstance, gripType);
        getInventory().removeItem(weaponInstance);
        getInventory().addItem(removedWeapon);
    }
    public EquipItemValidationType equipOffhandWeapon(WeaponInstance weaponInstance) {
        GripType gripType = GripType.getGripType(GripType.getGripTypeByHandleType(weaponInstance.getWeaponModel().getBaseWeapon().getCategory()), equipment.getMainWeaponGripType());
        if (gripType == null) {
            return EquipItemValidationType.Incompatibility;
        }
        WeaponInstance removedWeapon = equipment.equipOffWeapon(weaponInstance, gripType);
        getInventory().removeItem(weaponInstance);
        getInventory().addItem(removedWeapon);
        return null;
    }

    public void addAttributeBonusPoint(String attribute) {
        this.bonusAttributes.levelUp(attribute);
    }

}
