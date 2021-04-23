package com.rolerolls.application.combats;

import com.rolerolls.application.combats.dtos.*;
import com.rolerolls.domain.combats.Combat;
import com.rolerolls.domain.combats.CombatRepository;
import com.rolerolls.domain.combats.CombatService;
import com.rolerolls.domain.creatures.heroes.Hero;
import com.rolerolls.domain.creatures.heroes.HeroRepository;
import com.rolerolls.domain.creatures.monsters.Monster;
import com.rolerolls.domain.creatures.monsters.MonsterRepository;
import com.rolerolls.shared.LegacyBaseCrudController;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import java.io.IOException;
import java.lang.reflect.InvocationTargetException;
import java.util.UUID;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/combats",  produces = "application/json; charset=UTF-8")
public class CombatController extends LegacyBaseCrudController<Combat> {
	@Autowired
	public CombatController(CombatRepository repository) {
		super(repository);
	}
	@Autowired
	private CombatService combatService;
	@Autowired
	private CombatRepository repository;

	@Autowired
	private HeroRepository heroRepository;
	@Autowired
	private MonsterRepository monsterRepository;

	@PostMapping(path="/{combatId}/attack")
	public @ResponseBody CombatActionDto getFullAttack(@PathVariable UUID combatId, @RequestBody AttackInput input) throws NoSuchFieldException, SecurityException, IllegalAccessException, NoSuchMethodException, InvocationTargetException, IOException, ClassNotFoundException {
		if (input.attackType == AttackType.fullAttack) {
			CombatActionDto combatResult = combatService.processFullAttack(combatId, input.attackerId, input.targetId);
			return combatResult;
		} else {
			CombatActionDto combatResult = combatService.processFullAttack(combatId, input.attackerId, input.targetId);
			return combatResult;
		}
	}
/*	@GetMapping(path="/getAttackRoll")
	public @ResponseBody CombatActionDto getAttackRoll(@RequestParam UUID attackerId, @RequestParam UUID targetId, @RequestParam boolean isFullAttack) throws NoSuchFieldException, SecurityException, IllegalAccessException, NoSuchMethodException, InvocationTargetException {
		AttackDetails attackDetails = combatService.processFullAttack(attackerId, targetId);
		CombatActionDto dto = new CombatActionDto(attackDetails, 0, 0, 0);

		return dto;
	}*/
/*	@GetMapping(path="/getInitiative")
	public @ResponseBody CombatActionDto getInitiative(@RequestParam UUID attackerId, @RequestParam UUID targetId, @RequestParam boolean isFullAttack) throws NoSuchFieldException, SecurityException, IllegalAccessException, NoSuchMethodException, InvocationTargetException {
		AttackDetails attackDetails = combatService.processFullAttack(attackerId, targetId);
		CombatActionDto dto = new CombatActionDto(attackDetails, 0, 0, 0);

		return dto;
	}*/

	@PostMapping(path="/{combatId}/startCombat")
	public @ResponseBody Combat startCombat(@PathVariable UUID combatId) throws NoSuchFieldException, SecurityException, IllegalAccessException, NoSuchMethodException, InvocationTargetException {
		Combat startedCombat = repository.findById(combatId).get();
		startedCombat = combatService.startCombat(startedCombat);
		return startedCombat;
	}

	@PostMapping(path="/{combatId}/hero")
	public @ResponseBody Combat addHero(@PathVariable UUID combatId, @RequestBody AddOrRemoveCreatureToCombatInput input) throws NoSuchFieldException, SecurityException, IllegalAccessException, NoSuchMethodException, InvocationTargetException {
		Combat combat = repository.findById(combatId).get();
		Hero hero = heroRepository.findById(input.creatureId).get();
		combat.addHero(hero, combatService);
		repository.save(combat);
		return combat;
	}
	@PostMapping(path="/{combatId}/monster")
	public @ResponseBody Combat addMonster(@PathVariable UUID combatId, @RequestBody AddOrRemoveCreatureToCombatInput input) throws NoSuchFieldException, SecurityException, IllegalAccessException, NoSuchMethodException, InvocationTargetException {
		Combat combat = repository.findById(combatId).get();
		Monster monster = monsterRepository.findById(input.creatureId).get();
		combat.addMonster(monster, combatService);
		repository.save(combat);
		return combat;
	}
	@DeleteMapping(path="/{combatId}/hero")
	public @ResponseBody Combat removeHero(@PathVariable UUID combatId, @RequestParam UUID creatureId) throws NoSuchFieldException, SecurityException, IllegalAccessException, NoSuchMethodException, InvocationTargetException {
		Combat combat = repository.findById(combatId).get();
		combat.removeHero(creatureId);
		repository.save(combat);
		return combat;
	}
	@DeleteMapping(path="/{combatId}/monster")
	public @ResponseBody Combat removeMonster(@PathVariable UUID combatId, @RequestParam UUID creatureId) throws NoSuchFieldException, SecurityException, IllegalAccessException, NoSuchMethodException, InvocationTargetException {
		Combat combat = repository.findById(combatId).get();
		combat.removeMonster(creatureId);
		repository.save(combat);
		return combat;
	}
	@PostMapping(path="/{combatId}/endTurn")
	public @ResponseBody Combat endTurn(@PathVariable UUID combatId, @RequestBody FinishTurnInput input) throws NoSuchFieldException, SecurityException, IllegalAccessException, NoSuchMethodException, InvocationTargetException {
		Combat combat = combatService.endTurn(combatId, input.creatureId);
		return combat;
	}

	@Override
	public Combat getnew() {
		return null;
	}
}
