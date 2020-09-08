package com.loh.campaign.dtos;

import com.loh.campaign.InvitationStatus;
import com.loh.context.Player;

public class CampaignInvitation {
    public Player player;
    public InvitationStatus status;

    public CampaignInvitation(Player player, InvitationStatus status) {
        this.player = player;
        this.status = status;
    }
}
