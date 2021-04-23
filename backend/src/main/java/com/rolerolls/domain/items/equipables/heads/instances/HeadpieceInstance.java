package com.rolerolls.domain.items.equipables.heads.instances;

import com.rolerolls.domain.items.equipables.instances.EquipableInstance;
import com.rolerolls.domain.items.equipables.heads.models.HeadpieceModel;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.DiscriminatorValue;
import javax.persistence.Entity;
import javax.persistence.ManyToOne;

@Entity
@DiscriminatorValue("Headpiece")
public class HeadpieceInstance extends EquipableInstance {

    @Getter
    @Setter @ManyToOne
    private HeadpieceModel headpieceModel;

    public HeadpieceInstance() {
    }

    public HeadpieceInstance(HeadpieceModel headpieceModel, Integer level) {
        super(headpieceModel);
        this.headpieceModel = headpieceModel;
        this.setLevel(level);
        this.setName(headpieceModel.getName());
    }
}
