package com.loh.domain.campaigns;

import com.loh.shared.Entity;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.Column;
import java.util.UUID;

@javax.persistence.Entity
public class InvitedPlayer extends Entity {

    @Getter @Setter
    @Column(columnDefinition = "BINARY(16)")
    private UUID playerId;
    @Getter @Setter
    @Column(columnDefinition = "BINARY(16)")
    private UUID campaignId;

    @Getter @Setter
    private InvitationStatus status;

    public InvitedPlayer() {
    }

    public InvitedPlayer(UUID campaignId, UUID playerId) {
        this.campaignId = campaignId;
        this.playerId = playerId;
        this.status = InvitationStatus.Sent;
    }

}
