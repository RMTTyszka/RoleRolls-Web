package com.rolerolls.infrastructure;

import com.rolerolls.authentication.AdminSeeder;
import com.rolerolls.domain.creatures.heroes.HeroSeeder;
import com.rolerolls.domain.creatures.monsters.MonsterSeeder;
import com.rolerolls.domain.items.equipables.armors.ArmorSeeder;
import com.rolerolls.domain.items.equipables.belts.BeltsSeeder;
import com.rolerolls.domain.items.equipables.gloves.GlovesSeeder;
import com.rolerolls.domain.items.equipables.heads.HeadpieceSeeder;
import com.rolerolls.domain.items.equipables.necks.NeckAccessorySeeder;
import com.rolerolls.domain.items.equipables.rings.RingSeeder;
import com.rolerolls.domain.items.equipables.weapons.WeaponSeeder;
import com.rolerolls.domain.races.dummy.DummyRaceSeeder;
import com.rolerolls.domain.races.land.of.heroes.LandOfHeroesHeroRaceSeeder;
import com.rolerolls.domain.races.land.of.heroes.LandOfHeroesMonsterRaceSeeder;
import com.rolerolls.domain.races.the.future.is.out.there.TheFutureIsOutThereHeroRaceSeeder;
import com.rolerolls.domain.roles.dummy.DummyRoleSeeder;
import com.rolerolls.domain.roles.land.of.heroes.LohRoleSeeder;
import com.rolerolls.domain.roles.the.future.is.out.there.heroes.FotRoleSeeder;
import com.rolerolls.domain.shops.ShopSeeder;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.event.ContextRefreshedEvent;
import org.springframework.context.event.EventListener;
import org.springframework.stereotype.Component;
import org.springframework.transaction.annotation.Transactional;

@Component
@Transactional
public class SeederService {

	@Autowired
	private AdminSeeder adminSeeder;
	@Autowired
	private LandOfHeroesHeroRaceSeeder landOfHeroesHeroRaceSeeder;
	@Autowired
	private LandOfHeroesMonsterRaceSeeder landOfHeroesMonsterRaceSeeder;

	@Autowired
	private TheFutureIsOutThereHeroRaceSeeder theFutureIsOutThereHeroRaceSeeder;
	@Autowired
	private DummyRaceSeeder dummyRaceSeeder;
	@Autowired
	private LohRoleSeeder lohRoleSeeder;
	@Autowired
	private FotRoleSeeder fotRoleSeeder;
	@Autowired
	private DummyRoleSeeder dummyRoleSeeder;
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
	@Autowired
	private MonsterSeeder monsterSeeder;
	@Autowired
	private ShopSeeder shopSeeder;

	@EventListener
	public void seed(ContextRefreshedEvent event) throws Exception {
		adminSeeder.seed();

		seedRaces();
		seedRoles();

		armorSeeder.seed();
		weaponSeeder.seed();
		glovesSeeder.seed();
		headpieceSeeder.seed();
		beltsSeeder.seed();
		ringSeeder.seed();
		neckAccessorySeeder.seed();
		heroSeeder.seed();
		monsterSeeder.seed();
		shopSeeder.seed();
	}

	private void seedRaces() {
		landOfHeroesHeroRaceSeeder.seed();
		landOfHeroesMonsterRaceSeeder.seed();
		dummyRaceSeeder.seed();
		theFutureIsOutThereHeroRaceSeeder.seed();
	}
	private void seedRoles() {
		lohRoleSeeder.seed();
		fotRoleSeeder.seed();
		dummyRoleSeeder.seed();
	}

}
