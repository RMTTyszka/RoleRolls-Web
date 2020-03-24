package com.loh.infrastructure;

import com.loh.creatures.heroes.HeroSeeder;
import com.loh.items.equipable.armors.ArmorSeeder;
import com.loh.items.equipable.belts.BeltsSeeder;
import com.loh.items.equipable.gloves.GlovesSeeder;
import com.loh.items.equipable.head.HeadpieceSeeder;
import com.loh.items.equipable.neck.NeckAccessorySeeder;
import com.loh.items.equipable.rings.head.RingSeeder;
import com.loh.items.equipable.weapons.WeaponSeeder;
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
	@Autowired
	private GlovesSeeder glovesSeeder;
	@Autowired
	private BeltsSeeder beltsSeeder;
	@Autowired
	private HeadpieceSeeder headpieceSeeder;
	@Autowired
	private RingSeeder ringSeeder;
	@Autowired
	private NeckAccessorySeeder neckAccessorySeeder;
	@Autowired
	private HeroSeeder heroSeeder;

	@EventListener
	public void seed(ContextRefreshedEvent event) throws Exception {
		raceSeeder.seed();
		armorSeeder.seed();
		weaponSeeder.seed();
		glovesSeeder.seed();
		headpieceSeeder.seed();
		beltsSeeder.seed();
		ringSeeder.seed();
		neckAccessorySeeder.seed();
		roleSeeder.seed();
		heroSeeder.seed();
	}

}
