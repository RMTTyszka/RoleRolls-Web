package com.rolerolls.domain.items.equipables.necks.instances;

import com.rolerolls.domain.items.equipables.instances.EquipableInstance;
import com.rolerolls.domain.items.equipables.necks.models.NeckAcessoryModel;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.DiscriminatorValue;
import javax.persistence.Entity;
import javax.persistence.ManyToOne;

@Entity
@DiscriminatorValue("NeckAccessory")
public class NeckAccessoryInstance extends EquipableInstance {

    @Getter
    @Setter @ManyToOne
    private NeckAcessoryModel neckAcessoryModel;

    public Integer getManaBonus() {
        return getLevel() / 5;
    };

    public NeckAccessoryInstance() {
    }

    public NeckAccessoryInstance(NeckAcessoryModel neckAcessoryModel, Integer level) {
        super(neckAcessoryModel);
        this.neckAcessoryModel = neckAcessoryModel;
        this.setLevel(level);
        this.setName(neckAcessoryModel.getName());
    }
}
