package com.loh.items;

import com.loh.shared.Entity;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.DiscriminatorColumn;
import javax.persistence.Inheritance;
import javax.persistence.InheritanceType;

@javax.persistence.Entity
@Inheritance(strategy = InheritanceType.SINGLE_TABLE)
@DiscriminatorColumn(name = "ItemType")
public class ItemInstance extends Entity {

    @Getter @Setter
    private Integer level;
    @Getter @Setter
    private String name;
    @Getter @Setter
    private double value;

    public Integer getBonus() {
      return level / 2;
    }

    public ItemInstance() {
        level = 1;
    }

    public void levelUp(ItemInstanceRepository itemInstanceRepository) {
        level++;
        itemInstanceRepository.save(this);
    }
    public void levelUpForTest(ItemInstanceRepository itemInstanceRepository) {
        level++;
        itemInstanceRepository.save(this);
    }
}
