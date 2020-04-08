package com.loh.combat;

import com.loh.creatures.Creature;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.Embeddable;
import javax.persistence.OneToOne;

@Embeddable
public class Initiative implements Comparable<Initiative> {
    @Getter
    @Setter
    @OneToOne
    private Creature creature;
    @Getter
    @Setter
    private Integer value;
    @Getter
    @Setter
    private boolean acted;

    @Override
    public int compareTo(Initiative initiative) {
        if (value == null || initiative.value == null) {
            return 0;
        }
        return value.compareTo(initiative.value);
    }
}
