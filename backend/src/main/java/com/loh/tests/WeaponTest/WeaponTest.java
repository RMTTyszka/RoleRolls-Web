package com.loh.tests.WeaponTest;

import com.loh.combat.AttackDetails;
import com.loh.combat.AttackService;
import com.loh.creatures.DefaultHeroes;
import com.loh.creatures.heroes.Hero;
import com.loh.creatures.heroes.HeroRepository;
import com.loh.items.armors.armorCategories.ArmorCategory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import javax.transaction.Transactional;

@Service
@Transactional
public class WeaponTest {

    @Autowired
    private HeroRepository heroRepository;
    @Autowired
    private WeaponTestResultRepository weaponTestResultRepository;
    @Autowired
    private AttackService attackService;


    private final Integer NumberOfAttacks = 1000;
    public void test() {
        weaponTestResultRepository.deleteAllByArmorCategory(ArmorCategory.Light);
        for (int level = 1; level < 21; level++) {

            Hero oneLightHero = heroRepository.findByName(DefaultHeroes.getNameWithLevel(DefaultHeroes.OneLightWeapon, level));
            Hero oneMediumHero = heroRepository.findByName(DefaultHeroes.getNameWithLevel(DefaultHeroes.OneMediumWeapon, level));
            Hero oneHeavyHero = heroRepository.findByName(DefaultHeroes.getNameWithLevel(DefaultHeroes.OneHeavyWeapon, level));
            Hero twoLightsHero = heroRepository.findByName(DefaultHeroes.getNameWithLevel(DefaultHeroes.TwoLightWeapons, level));
            Hero twoMediumHero = heroRepository.findByName(DefaultHeroes.getNameWithLevel(DefaultHeroes.TwoMediumWeapons, level));
            Hero targetLight = heroRepository.findByName(DefaultHeroes.getNameWithLevel(DefaultHeroes.LightArmor, level));
            Hero targetMedium= heroRepository.findByName(DefaultHeroes.getNameWithLevel(DefaultHeroes.MediumArmor, level));
            Hero targetHeavy = heroRepository.findByName(DefaultHeroes.getNameWithLevel(DefaultHeroes.HeavyArmor, level));
            if (level == 6) {
                System.out.println("s");
            }
            WeaponTestResult oneLightWeaponLightArmorTestResult = new WeaponTestResult(oneLightHero.getEquipment().getMainWeaponGripType(), targetLight.getEquipment().getArmor().getArmorModel().getBaseArmor().getCategory(), level);
            for (int numberOfAttacks = 0; numberOfAttacks < NumberOfAttacks; numberOfAttacks++) {
                AttackDetails attackDetails = attackService.fullAttack(oneLightHero, targetLight);
                if (attackDetails.getMainWeaponHits() > 0) {
                    oneLightWeaponLightArmorTestResult.damages.add(attackDetails.getMainWeaponDamages().stream().reduce(0, (a, b) -> a + b).intValue());
                }
            }
            oneLightWeaponLightArmorTestResult.setDamage(oneLightWeaponLightArmorTestResult.damages.stream().reduce(0, (a, b) -> a + b).intValue() / NumberOfAttacks);
            weaponTestResultRepository.save(oneLightWeaponLightArmorTestResult);
            System.out.println(oneLightWeaponLightArmorTestResult.getDamage());
            //System.out.println(oneLightWeaponLightArmorTestResult.damages.size());

        }
    }

}
