package com.loh.domain.campaigns.rolls;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.Entity;
import java.util.UUID;

@Entity
public class CampaignRollHistoric extends com.loh.shared.Entity {
    @Getter @Setter
    private UUID creatureId;
    @Getter @Setter
    private String creatureName;
    private String property;
    @Getter @Setter
    private boolean success;
    @Getter @Setter
    private String rolls;
    @Getter @Setter
    private Integer bonusDice;
    @Getter @Setter
    private Integer numberOfRolls;
    @Getter @Setter
    private Integer rollSuccesses;
    @Getter @Setter
    private Integer successes;
    @Getter @Setter
    private Integer criticalSuccesses;
    @Getter @Setter
    private Integer criticalFailures;
    @Getter @Setter
    private Integer difficulty;
    @Getter @Setter
    private Integer complexity;

    public CampaignRollHistoric() {
    }
}
