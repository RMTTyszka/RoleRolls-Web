package com.rolerolls.domain.items.equipables.belts.base;

import com.rolerolls.shared.DefaultEntity;
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
