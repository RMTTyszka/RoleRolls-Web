package com.rolerolls.application.campaigns.dtos;

import com.rolerolls.domain.campaigns.InvitationStatus;
import com.rolerolls.domain.contexts.Player;

public class CampaignInvitation {
    public Player player;
    public InvitationStatus status;

    public CampaignInvitation(Player player, InvitationStatus status) {
        this.player = player;
        this.status = status;
    }
}
