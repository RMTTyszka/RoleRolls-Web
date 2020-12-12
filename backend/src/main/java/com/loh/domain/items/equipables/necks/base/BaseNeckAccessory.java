package com.loh.domain.items.equipables.necks.base;

import com.loh.shared.DefaultEntity;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.Entity;

@Entity
public class BaseNeckAccessory extends DefaultEntity {

    @Getter
    @Setter
    private String name;

    public BaseNeckAccessory() {
    }

    public BaseNeckAccessory(String name) {
        this.name = name;
    }
}
