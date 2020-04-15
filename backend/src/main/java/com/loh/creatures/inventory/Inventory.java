package com.loh.creatures.inventory;


import com.loh.items.ItemInstance;
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

}
