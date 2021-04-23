package com.rolerolls.application.campaigns.dtos;

import com.rolerolls.domain.campaigns.Campaign;

import java.util.List;

public class PlayerInvitationsOutput {
    public List<Campaign> campaigns;

    public PlayerInvitationsOutput(List<Campaign> campaigns) {
        this.campaigns = campaigns;
    }
}
