package com.loh.domain.campaigns.rolls;

import lombok.Getter;
import lombok.Setter;
import org.springframework.data.annotation.CreatedDate;
import org.springframework.data.jpa.domain.support.AuditingEntityListener;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.EntityListeners;
import java.sql.Timestamp;
import java.util.UUID;

@Entity
@EntityListeners(AuditingEntityListener.class)
public class CampaignRollHistoric extends com.loh.shared.Entity {
    @Getter @Setter    @Column(columnDefinition = "BINARY(16)")

    private UUID campaignId;
    @Getter @Setter    @Column(columnDefinition = "BINARY(16)")

    private UUID creatureId;
    @Getter @Setter
    private String creatureName;
    @Getter @Setter
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
    @CreatedDate
    private Timestamp creationTime;
    public CampaignRollHistoric() {
    }
}
