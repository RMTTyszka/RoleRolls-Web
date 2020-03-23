package com.loh.items.equipable.gloves.baseGloves;

import com.loh.shared.DefaultEntity;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.Entity;

@Entity
public class BaseGlove extends DefaultEntity {

    @Getter
    @Setter
    private String name;

    public BaseGlove() {
    }

    public BaseGlove(String name) {
        this.name = name;
    }
}
