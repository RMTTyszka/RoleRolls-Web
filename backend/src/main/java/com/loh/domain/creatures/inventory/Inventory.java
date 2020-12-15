package com.loh.domain.creatures.inventory;


import com.loh.domain.items.instances.ItemInstance;
import com.loh.shared.Entity;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.OneToMany;
import java.util.ArrayList;
import java.util.List;

@javax.persistence.Entity
public class Inventory extends Entity {

    @Getter @Setter @OneToMany
    private List<ItemInstance> items = new ArrayList<>();

    public void addItem(ItemInstance item) {
        items.add(item);
    }
    public void removeItem(ItemInstance itemToRemove) {
        items.stream().filter(item -> item.getId() != itemToRemove.getId());
    }
    public void removeFunds(Integer quantity) {
       this.cash1 -= quantity;
    }

    @Getter
    @Setter
    private double cash1;
    @Getter
    @Setter
    private double cash2;
    @Getter
    @Setter
    private double cash3;

}
