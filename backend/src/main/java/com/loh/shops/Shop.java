package com.loh.shops;

import com.loh.shared.DefaultEntity;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.Entity;
import javax.persistence.OneToMany;
import java.util.ArrayList;
import java.util.List;

@Entity
public class Shop extends DefaultEntity {

    @Getter
    @Setter
    @OneToMany
    private List<ShopItem> items = new ArrayList<>();
    @Getter
    @Setter
    private String name;

    public void addItem(ShopItem item) {
        this.items.add(item);
    }
}
