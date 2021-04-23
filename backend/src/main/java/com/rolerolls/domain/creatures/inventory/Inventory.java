package com.rolerolls.domain.creatures.inventory;


import com.rolerolls.domain.items.instances.ItemInstance;
import com.rolerolls.shared.Entity;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.OneToMany;
import java.util.ArrayList;
import java.util.List;
import java.util.UUID;
import java.util.stream.Collectors;

@javax.persistence.Entity
public class Inventory extends Entity {

    @Getter @Setter @OneToMany
    private List<ItemInstance> items = new ArrayList<>();

    public ItemInstance getItem(UUID itemId) {
        return items.stream().filter(e -> e.getId().equals(itemId)).findFirst().get();
    }

    public void addItem(ItemInstance item) {
        items.add(item);
    }
    public void removeItem(ItemInstance itemToRemove, Integer stackSize) {
        if (itemToRemove.getQuantity() > 1) {
            itemToRemove.removeQuantity(stackSize);
        } else {
            items = items.stream().filter(item -> item.getId() != itemToRemove.getId()).collect(Collectors.toList());
        }
    }
    public void removeItem(ItemInstance itemToRemove) {
        removeItem(itemToRemove, 1);
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
