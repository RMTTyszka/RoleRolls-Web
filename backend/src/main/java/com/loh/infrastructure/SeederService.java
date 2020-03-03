package com.loh.infrastructure;

import com.loh.items.armors.ArmorSeeder;
import com.loh.items.weapons.WeaponSeeder;
import com.loh.race.RaceSeeder;
import com.loh.role.RoleSeeder;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.event.ContextRefreshedEvent;
import org.springframework.context.event.EventListener;
import org.springframework.stereotype.Component;
@Component
public class SeederService {

	@Autowired
	private RaceSeeder raceSeeder;
	@Autowired
	private RoleSeeder roleSeeder;
	@Autowired
	private ArmorSeeder armorSeeder;
	@Autowired
	private WeaponSeeder weaponSeeder;

	
	@EventListener
	public void seed(ContextRefreshedEvent event) {
		raceSeeder.seed();
		armorSeeder.seed();
		weaponSeeder.seed();
		roleSeeder.seed();
	}

}
