package com.loh.campaign.dtos;

import com.loh.campaign.Campaign;

import java.util.List;

public class PlayerInvitationsOutput {
    public List<Campaign> campaigns;

    public PlayerInvitationsOutput(List<Campaign> campaigns) {
        this.campaigns = campaigns;
    }
}
