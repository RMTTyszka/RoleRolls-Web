package com.loh.combat;

import com.loh.shared.BaseCrudController;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import java.lang.reflect.InvocationTargetException;
import java.util.UUID;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/combat",  produces = "application/json; charset=UTF-8")
public class CombatController extends BaseCrudController<Combat> {
	@Autowired
	public CombatController(CombatRepository repository) {
		super(repository);
	}

	@Autowired
	private CombatService combatService;

	@GetMapping(path="/fullAttack")
	public @ResponseBody CombatActionDto getFullAttack(@RequestParam UUID attackerId, @RequestParam UUID targetId) throws NoSuchFieldException, SecurityException, IllegalAccessException, NoSuchMethodException, InvocationTargetException {
		AttackDetails attackDetails = combatService.processFullAttack(attackerId, targetId);
		CombatActionDto dto = new CombatActionDto(attackDetails, 0, 0, 0);
		return dto;
	}
	@GetMapping(path="/getAttackRoll")
	public @ResponseBody CombatActionDto getAttackRoll(@RequestParam UUID attackerId, @RequestParam UUID targetId, @RequestParam boolean isFullAttack) throws NoSuchFieldException, SecurityException, IllegalAccessException, NoSuchMethodException, InvocationTargetException {
		AttackDetails attackDetails = combatService.processFullAttack(attackerId, targetId);
		CombatActionDto dto = new CombatActionDto(attackDetails, 0, 0, 0);

		return dto;
	}
	@GetMapping(path="/getInitiative")
	public @ResponseBody CombatActionDto getInitiative(@RequestParam UUID attackerId, @RequestParam UUID targetId, @RequestParam boolean isFullAttack) throws NoSuchFieldException, SecurityException, IllegalAccessException, NoSuchMethodException, InvocationTargetException {
		AttackDetails attackDetails = combatService.processFullAttack(attackerId, targetId);
		CombatActionDto dto = new CombatActionDto(attackDetails, 0, 0, 0);

		return dto;
	}

	@PostMapping(path="/startCombat")
	public @ResponseBody Combat startCombat(@RequestBody Combat combat) throws NoSuchFieldException, SecurityException, IllegalAccessException, NoSuchMethodException, InvocationTargetException {
		Combat startedCombat = combatService.startCombat(combat);
		return startedCombat;
	}

	@PostMapping(path="/addHero")
	public @ResponseBody Initiative addHero(@RequestBody AddHeroToCombatInput input) throws NoSuchFieldException, SecurityException, IllegalAccessException, NoSuchMethodException, InvocationTargetException {
		Combat combat = repository.findById(input.combatId).get();
		Initiative initiative = combat.addHero(input.hero, combatService);
		repository.save(combat);
		return initiative;
	}
	@PostMapping(path="/addMonster")
	public @ResponseBody Initiative addMonster(@RequestBody AddMonsterToCombatInput input) throws NoSuchFieldException, SecurityException, IllegalAccessException, NoSuchMethodException, InvocationTargetException {
		Combat combat = repository.findById(input.combatId).get();
		Initiative initiative = combat.addMonster(input.monster, combatService);
		repository.save(combat);
		return initiative;
	}
	
	
}
