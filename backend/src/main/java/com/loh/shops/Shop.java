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
    private List<ShopWeapon> weapons = new ArrayList<>();
    @Getter
    @Setter
    @OneToMany
    private List<ShopArmor> armors = new ArrayList<>();

    public void addArmor(ShopArmor armor) {
        this.armors.add(armor);
    }
}
