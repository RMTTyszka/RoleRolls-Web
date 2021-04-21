package com.loh.domain.items.equipables.gloves.instances;

import com.loh.domain.items.equipables.instances.EquipableInstance;
import com.loh.domain.items.equipables.gloves.models.GloveModel;
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

    public GloveInstance() {
    }

    public GloveInstance(GloveModel gloveModel, Integer level) {
        super(gloveModel);
        this.gloveModel = gloveModel;
        this.setLevel(level);
        this.setName(gloveModel.getName());
    }
}
