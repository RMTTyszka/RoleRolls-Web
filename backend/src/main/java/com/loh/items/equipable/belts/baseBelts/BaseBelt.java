package com.loh.items.equipable.belts.baseBelts;

import com.loh.shared.DefaultEntity;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.Entity;

@Entity
public class BaseBelt extends DefaultEntity {

    @Getter
    @Setter
    private String name;

    public BaseBelt() {
    }

    public BaseBelt(String name) {
        this.name = name;
    }
}
