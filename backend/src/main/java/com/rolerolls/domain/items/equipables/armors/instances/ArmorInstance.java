package com.rolerolls.domain.items.equipables.armors.instances;

import com.rolerolls.domain.items.equipables.instances.EquipableInstance;
import com.rolerolls.domain.items.equipables.armors.templates.ArmorTemplate;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.DiscriminatorValue;
import javax.persistence.ManyToOne;
import javax.persistence.Transient;

@javax.persistence.Entity
@DiscriminatorValue("Armor")
public class ArmorInstance extends EquipableInstance {

    @Getter @Setter @ManyToOne
    private ArmorTemplate armorTemplate;
    @Transient
    public boolean isArmor = true;

    public Integer getDefense() {
        return armorTemplate.getDefense(getBonus());
    }
    public Integer getEvasion() {
        return armorTemplate.getEvasion() + getBonus();
    }
    public Integer getDodge() {
        return armorTemplate != null ? armorTemplate.getDodge() : 0;
    }

    public ArmorInstance() {
        super();
        armorTemplate = new ArmorTemplate();
    }

    public ArmorInstance(ArmorTemplate armorTemplate, Integer level, Integer quantity) {
        super(armorTemplate);
        this.armorTemplate = armorTemplate;
        this.setName(armorTemplate.getName());
        this.setQuantity(quantity);
        this.setLevel(level);
    }
}
