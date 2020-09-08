package com.loh.campaign;

import com.loh.campaign.dtos.AddPlayerAndHeroToCampaignInput;
import com.loh.campaign.dtos.InvitedPlayerOutput;
import com.loh.context.Player;
import com.loh.context.PlayerRepository;
import com.loh.creatures.heroes.Hero;
import com.loh.creatures.heroes.HeroRepository;
import com.loh.shared.BaseCrudController;
import com.loh.shared.BaseCrudResponse;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.domain.Specification;
import org.springframework.http.HttpStatus;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import java.security.Principal;
import java.util.List;
import java.util.UUID;
import java.util.stream.Collectors;

import static com.loh.authentication.LohUserDetails.userId;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/campaigns",  produces = "application/json; charset=UTF-8")
public class CampaignController extends BaseCrudController<Campaign> {

	@Autowired
	PlayerRepository playerRepository;
	@Autowired
	InvitedPlayerRepository invitedPlayerRepository;
	@Autowired
	HeroRepository heroRepository;

	@Autowired
	CampaignRepository repository;

	public CampaignController(CampaignRepository repository) {
		super(repository);
	}

	@Override
	public Campaign getnew() {
		return new Campaign();
	}

	@Override
	@PostMapping(path="/create")
	public @ResponseBody BaseCrudResponse<Campaign> add(@RequestBody Campaign campaign, Principal principal) {
		UUID userId = userId(principal);
		Player master = playerRepository.findById(userId).get();
		campaign.setMaster(master);
		campaign = repository.save(campaign);

		return new BaseCrudResponse<Campaign>(true, "Campaign successfuly created", campaign);
	}
	@GetMapping(path="/player/invite/get")
	public  @ResponseBody
	InvitedPlayerOutput getInvitations(Principal principal) {
		UUID playerId = userId(principal);
		List<InvitedPlayer> invitations = invitedPlayerRepository.findAllByPlayerIdAndStatus(playerId, InvitationStatus.Sent);
		List<UUID> campaignIds = invitations.stream().map(i -> i.getCampaignId()).collect(Collectors.toList());
		List<Campaign> campaigns = repository.findAllByIdIn(campaignIds);
		return new InvitedPlayerOutput(campaigns);

	}
	@PostMapping(path="/player/invite")
	@ResponseStatus(HttpStatus.OK)
	public void invitePlayer(@RequestBody AddPlayerAndHeroToCampaignInput input) {
		InvitedPlayer invitedPlayer = new InvitedPlayer(input.campaignId, input.playerId);
		invitedPlayerRepository.save(invitedPlayer);
	}	
	@DeleteMapping(path="/player/invite/delete/{id}")
	@ResponseStatus(HttpStatus.OK)
	public void deleteInvitation(@PathVariable UUID id) {
		invitedPlayerRepository.deleteById(id);
	}
	@PostMapping(path="/player/invite/deny/{campaignId}")
	@ResponseStatus(HttpStatus.OK)
	public void denyInvitation(@PathVariable UUID campaignId, Principal principal) {
		UUID playerId = userId(principal);
		InvitedPlayer invitedPlayer = invitedPlayerRepository.findByCampaignIdAndPlayerId(campaignId, playerId);
		invitedPlayer.setStatus(InvitationStatus.Denied);
		invitedPlayerRepository.save(invitedPlayer);
	}
	@PostMapping(path="/player/add/{campaignId}")
	@ResponseStatus(HttpStatus.OK)
	public void addPlayer(@PathVariable UUID campaignId, Principal principal) {
		UUID playerId = userId(principal);
		Campaign campaign = repository.findById(campaignId).get();
		Player player = playerRepository.findById(playerId).get();
		campaign.addPlayer(player);
		repository.save(campaign);
		InvitedPlayer invitedPlayer = invitedPlayerRepository.findByCampaignIdAndPlayerId(campaignId, playerId);
		invitedPlayer.setStatus(InvitationStatus.Accepted);
		invitedPlayerRepository.save(invitedPlayer);

	}
	@GetMapping(path="/heroes/select")
	public @ResponseBody
	Iterable<Hero> getHeroesForSelect(@RequestParam(required = false) String filter, @RequestParam UUID campaignId, @RequestParam int skipCount, @RequestParam int maxResultCount, Principal userDetails) {
		Campaign campaign = repository.findById(campaignId).get();
		List<UUID> playerIds = campaign.getPlayers().stream().map(p -> p.getId()).collect(Collectors.toList());
		List<Hero> heroesForSelect = heroRepository.findAll(fromPlayers(playerIds));
		return heroesForSelect;
	}
	@GetMapping(path="/player/select")
	public @ResponseBody
	Iterable<Player> getPlayersForInvite(@RequestParam(required = false) String filter, @RequestParam UUID campaignId, @RequestParam int skipCount, @RequestParam int maxResultCount) {
		List<InvitedPlayer> invitedPlayers = invitedPlayerRepository.findAllByCampaignId(campaignId);
		List<UUID> alreadyInvitedPlayerIds = invitedPlayers.stream().map(p -> p.getPlayerId()).collect(Collectors.toList());
		Pageable paged = PageRequest.of(skipCount, maxResultCount);
		List<Player> playersForSelect = playerRepository.findAll(ignoringPlayers(alreadyInvitedPlayerIds), paged).getContent();
		return playersForSelect;
	}

	private Specification<Hero> fromPlayers(List<UUID> playerIds) {
		if (playerIds == null || playerIds.isEmpty()) {
			return (hero, cq, cb) -> cb.isNotNull(hero);
		}
		return (hero, cq, cb) -> hero.get("ownerId").in(playerIds);
	}
	private Specification<Player> ignoringPlayers(List<UUID> playerIds) {
		if (playerIds == null || playerIds.isEmpty()) {
			return (player, cq, cb) -> cb.isNotNull(player);
		}
		return (player, cq, cb) -> player.get("id").in(playerIds).not();
	}
}
