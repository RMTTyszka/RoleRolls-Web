package com.loh.campaign.dtos;

import com.loh.campaign.Campaign;

import java.util.List;

public class InvitedPlayerOutput {
    public List<Campaign> campaigns;

    public InvitedPlayerOutput(List<Campaign> campaigns) {
        this.campaigns = campaigns;
    }
}
