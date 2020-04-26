package com.loh.items;

import com.loh.shared.Bonus;
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

    public EquipableInstance() {
        super();
    }
}
