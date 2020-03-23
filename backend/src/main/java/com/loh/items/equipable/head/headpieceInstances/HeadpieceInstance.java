package com.loh.items.equipable.head.headpieceInstances;

import com.loh.items.EquipableInstance;
import com.loh.items.equipable.head.headpieceModel.HeadpieceModel;
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
        this.headpieceModel = headpieceModel;
        this.setLevel(level);
        this.setName(headpieceModel.getName());
    }
}
