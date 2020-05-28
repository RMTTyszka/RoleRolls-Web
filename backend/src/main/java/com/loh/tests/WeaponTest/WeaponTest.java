package com.loh.tests.WeaponTest;

import com.loh.combat.AttackDetails;
import com.loh.combat.AttackService;
import com.loh.creatures.heroes.DefaultHeroes;
import com.loh.creatures.heroes.Hero;
import com.loh.creatures.heroes.HeroRepository;
import com.loh.items.equipable.armors.armorCategories.ArmorCategory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import javax.transaction.Transactional;
import java.util.ArrayList;
import java.util.List;

@Service
@Transactional
public class WeaponTest {

    @Autowired
    private HeroRepository heroRepository;
    @Autowired
    private WeaponTestResultRepository weaponTestResultRepository;
    @Autowired
    private AttackService attackService;

    Long startTime;
    Long endTime;

    private final Integer NumberOfAttacks = 100;
    public void test() {
        List<WeaponTestResult> resultsToSave = new ArrayList<>();
        weaponTestResultRepository.deleteAllByArmorCategory(ArmorCategory.Light);
        weaponTestResultRepository.deleteAllByArmorCategory(ArmorCategory.Medium);
        weaponTestResultRepository.deleteAllByArmorCategory(ArmorCategory.Heavy);
        for (int level = 1; level < 21; level++) {
            System.out.println("Level " + level);
            List<Hero> heroes = new ArrayList<>();
            Hero oneLightHero = heroRepository.findByName(DefaultHeroes.getNameWithLevel(DefaultHeroes.OneLightWeapon, level));
            Hero oneMediumHero = heroRepository.findByName(DefaultHeroes.getNameWithLevel(DefaultHeroes.OneMediumWeapon, level));
            Hero oneHeavyHero = heroRepository.findByName(DefaultHeroes.getNameWithLevel(DefaultHeroes.OneHeavyWeapon, level));
            Hero twoLightsHero = heroRepository.findByName(DefaultHeroes.getNameWithLevel(DefaultHeroes.TwoLightWeapons, level));
            Hero twoMediumHero = heroRepository.findByName(DefaultHeroes.getNameWithLevel(DefaultHeroes.TwoMediumWeapons, level));

            heroes.add(oneLightHero);
            heroes.add(oneMediumHero);
            heroes.add(oneHeavyHero);
            heroes.add(twoLightsHero);
            heroes.add(twoMediumHero);
            Hero targetLight = heroRepository.findByName(DefaultHeroes.getNameWithLevel(DefaultHeroes.LightArmor, level));
            Hero targetMedium= heroRepository.findByName(DefaultHeroes.getNameWithLevel(DefaultHeroes.MediumArmor, level));
            Hero targetHeavy = heroRepository.findByName(DefaultHeroes.getNameWithLevel(DefaultHeroes.HeavyArmor, level));

            for (Hero hero: heroes
            ) {
                List<Integer> lightHits = new ArrayList<>();
                List<Integer> mediumHits = new ArrayList<>();
                List<Integer> heavyHits = new ArrayList<>();

                WeaponTestResult lightArmorTestResult = new WeaponTestResult(hero.getEquipment().getMainWeaponGripType(), targetLight.getEquipment().getArmor().getArmorModel().getBaseArmor().getCategory(), level);
                WeaponTestResult mediumArmorTestResult = new WeaponTestResult(hero.getEquipment().getMainWeaponGripType(), targetMedium.getEquipment().getArmor().getArmorModel().getBaseArmor().getCategory(), level);
                WeaponTestResult heavyArmorTestResult = new WeaponTestResult(hero.getEquipment().getMainWeaponGripType(), targetHeavy.getEquipment().getArmor().getArmorModel().getBaseArmor().getCategory(), level);
                startTime = System.nanoTime();
                for (int attackNumber = 0; attackNumber < NumberOfAttacks; attackNumber++) {
                    performAttack(hero, targetLight, lightHits, lightArmorTestResult);
                    performAttack(hero, targetMedium, mediumHits, mediumArmorTestResult);
                    performAttack(hero, targetHeavy, heavyHits, heavyArmorTestResult);
                }
                endTime = System.nanoTime();
               // System.out.println((double)(endTime - startTime)  / 1_000_000_000);
                lightArmorTestResult.setDamage(NumberOfAttacks);
                mediumArmorTestResult.setDamage(NumberOfAttacks);
                heavyArmorTestResult.setDamage(NumberOfAttacks);
                lightArmorTestResult.setHitsPercentage();
                mediumArmorTestResult.setHitsPercentage();
                heavyArmorTestResult.setHitsPercentage();
                resultsToSave.add(lightArmorTestResult);
                resultsToSave.add(mediumArmorTestResult);
                resultsToSave.add(heavyArmorTestResult);
                //System.out.println(mediumHits.stream().reduce(0, (a,b) -> a + b).doubleValue()/ mediumHits.size() / hero.getAttributeLevel(Attributes.Agility)  + " " + hero.getAttributeLevel(Attributes.Agility) + " " + mediumArmorTestResult.getDamage());
                //System.out.println(oneLightWeaponLightArmorTestResult.getDamage());
                //System.out.println(lightHits.stream().reduce(0, (a,b) -> a + b).doubleValue()/ lightHits.size() / oneLightHero.getAttributeLevel(Attributes.Agility)  + " " + oneLightHero.getAttributeLevel(Attributes.Agility) + " " + oneLightWeaponLightArmorTestResult.getDamage());
                //System.out.println(heavyHits.stream().reduce(0, (a,b) -> a + b).doubleValue()/ heavyHits.size() / oneLightHero.getAttributeLevel(Attributes.Agility)  + " " + oneLightHero.getAttributeLevel(Attributes.Agility) + " " + oneLightWeaponLightArmorTestResult.getDamage());
                //System.out.println(oneLightWeaponLightArmorTestResult.damages.size());
            }

        }
        weaponTestResultRepository.saveAll(resultsToSave);
        System.out.println("Done");
    }

    private void performAttack(Hero oneLightHero, Hero targetLight, List<Integer> hits, WeaponTestResult weaponTestResult) {
        AttackDetails attackDetails = attackService.fullAttack(oneLightHero, targetLight);
        hits.add(attackDetails.getMainWeaponAttackResult().getHits());
        if (attackDetails.getMainWeaponAttackResult().getHits() > 0) {
            weaponTestResult.damages.add(attackDetails.getMainWeaponAttackResult().getTotalDamage());
            weaponTestResult.setHits(attackDetails.getMainWeaponAttackResult().getHits());
            weaponTestResult.setNumberOfAttacks(attackDetails.getMainWeaponAttackResult().getNumberOfAttacks());
        }
        if (attackDetails.getOffWeaponAttackResult() != null && attackDetails.getOffWeaponAttackResult().getHits() != null && attackDetails.getOffWeaponAttackResult().getHits() > 0) {
            weaponTestResult.damages.add(attackDetails.getOffWeaponAttackResult().getTotalDamage());
            weaponTestResult.setHits(weaponTestResult.getHits() + attackDetails.getOffWeaponAttackResult().getHits());
            weaponTestResult.setNumberOfAttacks(weaponTestResult.getNumberOfAttacks() + attackDetails.getOffWeaponAttackResult().getNumberOfAttacks());
        }
    }

}
