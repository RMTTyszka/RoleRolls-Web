package com.loh.campaign;

import org.springframework.data.repository.CrudRepository;

import java.util.List;
import java.util.UUID;

public interface InvitedPlayerRepository extends CrudRepository<InvitedPlayer, UUID> {
    List<InvitedPlayer> findAllByPlayerIdAndStatus(UUID playerId, InvitationStatus invitationStatus);
    List<InvitedPlayer> findAllByCampaignId(UUID campaignId);
    InvitedPlayer findByCampaignIdAndPlayerId(UUID campaignId, UUID playerId);
    Integer deleteAllByCampaignId(UUID campaignId);
    Integer deleteByCampaignIdAndPlayerId(UUID campaignId, UUID playerId);
}
