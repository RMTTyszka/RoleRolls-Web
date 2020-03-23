package com.loh.items.equipable.gloves.gloveInstances;

import com.loh.items.EquipableInstance;
import com.loh.items.equipable.gloves.gloveModels.GloveModel;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.DiscriminatorValue;
import javax.persistence.Entity;
import javax.persistence.ManyToOne;

@Entity
@DiscriminatorValue("Glove")
public class GloveInstance extends EquipableInstance {

    @Getter
    @Setter @ManyToOne
    private GloveModel gloveModel;
}
