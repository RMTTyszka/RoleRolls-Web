package com.rolerolls.shared;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.MappedSuperclass;

@MappedSuperclass
public class DefaultEntity extends Entity {
    @Getter @Setter
    protected boolean systemDefault;
}
