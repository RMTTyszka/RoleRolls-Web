package com.loh.shops;

import com.loh.items.ItemInstance;
import com.loh.shared.Entity;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.MappedSuperclass;
import javax.persistence.OneToOne;

@MappedSuperclass
public class ShopItem extends Entity {
    @Getter
    @Setter
    protected int quantity;
    @Getter
    @Setter
    protected int value;
    @Getter
    @Setter
    protected int stack;

    @Getter
    @Setter
    @OneToOne
    protected ItemInstance item;



}
