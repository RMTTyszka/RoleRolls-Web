package com.rolerolls.domain.items.equipables.instances;

import com.rolerolls.domain.items.equipables.EquipableTemplate;
import com.rolerolls.domain.items.instances.ItemInstance;
import com.rolerolls.shared.Bonus;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.CollectionTable;
import javax.persistence.ElementCollection;
import javax.persistence.MappedSuperclass;
import java.util.ArrayList;
import java.util.List;

@MappedSuperclass
public class EquipableInstance extends ItemInstance {

    @ElementCollection
    @CollectionTable()
    @Getter
    @Setter
    private List<Bonus> bonuses = new ArrayList<>();
    @Getter @Setter
    private boolean removable;

    public boolean isEquipable() {
        return true;
    }

    public EquipableInstance() {
        super();
        removable = true;
    }
    public EquipableInstance(EquipableTemplate template) {
        super(template);
        removable = true;
    }
}
