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

    public Integer getBonusLevel(String property) {
        return bonuses.stream().filter(bonus -> bonus.getProperty() == property).map(e -> e.getLevel()).reduce(0 , (a ,b) -> a + b).intValue();
    }
}
