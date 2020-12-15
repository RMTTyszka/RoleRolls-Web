package com.loh.application.campaigns.dtos;

import com.loh.domain.campaigns.InvitationStatus;
import com.loh.domain.contexts.Player;

public class CampaignInvitation {
    public Player player;
    public InvitationStatus status;

    public CampaignInvitation(Player player, InvitationStatus status) {
        this.player = player;
        this.status = status;
    }
}
