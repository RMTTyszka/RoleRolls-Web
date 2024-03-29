package com.rolerolls.domain.items.equipables.heads.base;

import com.rolerolls.shared.DefaultEntity;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.Entity;

@Entity
public class BaseHeadpiece extends DefaultEntity {

    @Getter
    @Setter
    private String name;

    public BaseHeadpiece() {
    }

    public BaseHeadpiece(String name) {
        this.name = name;
    }
}
