package com.loh.application.campaigns.dtos;

import com.loh.domain.campaigns.Campaign;

import java.util.List;

public class PlayerInvitationsOutput {
    public List<Campaign> campaigns;

    public PlayerInvitationsOutput(List<Campaign> campaigns) {
        this.campaigns = campaigns;
    }
}
