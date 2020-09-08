package com.loh.shared;

import lombok.Getter;
import lombok.Setter;
import org.hibernate.annotations.GenericGenerator;

import javax.persistence.Column;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.MappedSuperclass;
import java.util.UUID;

@MappedSuperclass
public class Entity {

    @Id
    @GeneratedValue(generator="UUIDgenerator")
    @GenericGenerator(name="UUIDgenerator", strategy="org.hibernate.id.UUIDGenerator")
    @Getter
    @Setter
    @Column(columnDefinition = "BINARY(16)")
    protected UUID id;
}
