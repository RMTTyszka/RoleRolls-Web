package com.loh.shops;

import com.loh.items.ItemTemplate;
import com.loh.shared.Entity;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.DiscriminatorColumn;
import javax.persistence.Inheritance;
import javax.persistence.InheritanceType;
import javax.persistence.OneToOne;

@javax.persistence.Entity
@Inheritance(strategy = InheritanceType.SINGLE_TABLE)
@DiscriminatorColumn(name = "ItemType")
public class ShopItem extends Entity {
    @Getter
    @Setter
    protected int quantity;
    @Getter
    @Setter
    protected int value;
    @Getter
    @Setter
    protected int level;
    @Getter
    @Setter
    protected int stack;

    @Getter
    @Setter
    @OneToOne
    protected ItemTemplate item;

    public void removeQuantity(Integer quantity) {
        this.quantity -= quantity;
    }



}
