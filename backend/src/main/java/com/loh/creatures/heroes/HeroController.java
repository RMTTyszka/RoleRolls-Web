package com.loh.creatures.heroes;


import com.loh.creatures.heroes.equipment.EquipmentRepository;
import com.loh.creatures.heroes.equipment.GripType;
import com.loh.creatures.heroes.inventory.InventoryRepository;
import com.loh.items.armors.ArmorInstance;
import com.loh.items.armors.ArmorInstanceRepository;
import com.loh.items.armors.armorTypes.ArmorType;
import com.loh.items.weapons.weaponCategory.WeaponType;
import com.loh.items.weapons.weaponInstance.WeaponInstance;
import com.loh.items.weapons.weaponInstance.WeaponInstanceRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.jpa.domain.Specification;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import javax.json.Json;
import javax.json.JsonObject;
import java.util.UUID;

import static org.springframework.data.jpa.domain.Specification.where;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/hero",  produces = "application/json; charset=UTF-8")
public class HeroController {
    @Autowired
    private HeroRepository heroRepository;

    @Autowired
    private ArmorInstanceRepository armorInstanceRepository;
    @Autowired
    private WeaponInstanceRepository weaponInstanceRepository;
    @Autowired
    private EquipmentRepository equipmentRepository;
    @Autowired
    private InventoryRepository inventoryRepository;

    @GetMapping(path="/allFiltered")
    public @ResponseBody
    Iterable<Hero> getAllHeroes(@RequestParam(required = false) String filter) {

        Iterable<Hero> heroes = heroRepository.findAll(where(containsName(filter).and(orderByName())));
        for (Hero hero : heroes) {
            System.out.println(hero.getAttributeModifier("strength"));
        }
        return heroes;
    }
    // This returns a JSON or XML with the users
    @GetMapping(path="/find")
    public @ResponseBody
    Hero getHero(@RequestParam UUID id) throws NoSuchFieldException, SecurityException, IllegalArgumentException, IllegalAccessException {
        // This returns a JSON or XML with the users
        Hero hero = heroRepository.findById(id).get();
        WeaponInstance weapon = new WeaponInstance();
        weapon.setName("Minha arma");
        hero.getInventory().addItem(weapon);
        System.out.println(hero.getDefense());
        System.out.println(hero.getEvasion());
        return hero;
    }
    @GetMapping(path="/getNew")
    public @ResponseBody
    Hero getNewHero() throws NoSuchFieldException, SecurityException, IllegalArgumentException, IllegalAccessException {
        // This returns a JSON or XML with the users
        //	System.out.println(io.swagger.util.Json.pretty(monster));
        Hero hero = new Hero();
        ArmorInstance armor = armorInstanceRepository.findByArmorModel_BaseArmor_Category_ArmorType(ArmorType.None);
        WeaponInstance weapon = weaponInstanceRepository.findByWeaponModel_BaseWeapon_Category_WeaponType(WeaponType.None);
        hero.getEquipment().setArmor(armor);
        try {
            hero.getEquipment().equipMainWeapon(weapon, GripType.OneMediumWeapon);
        }
        catch (Exception e) {
            e.printStackTrace();
        }
        return hero;
    }
    @PutMapping(path="/update")
    public @ResponseBody
    Hero updateHero(@RequestBody Hero heroDto) {

        armorInstanceRepository.save(heroDto.getEquipment().getArmor());
        equipmentRepository.save(heroDto.getEquipment());
        return heroRepository.save(heroDto);
    }

    @PostMapping(path="/create")
    public @ResponseBody
    Hero createHero(@RequestBody Hero heroDto) {


        heroDto.getEquipment().setArmor(armorInstanceRepository.save(heroDto.getEquipment().getArmor()));
        heroDto.setEquipment(equipmentRepository.save(heroDto.getEquipment()));
        heroDto.setInventory(inventoryRepository.save(heroDto.getInventory()));
        return heroRepository.save(heroDto);
    }

    @DeleteMapping(path="/delete")
    public @ResponseBody
    JsonObject deleteHero(@RequestParam UUID id) {

        heroRepository.deleteById(id);

        return Json.createObjectBuilder()
                .add("text", "hero deleted with success").build();
    }

    static Specification<Hero> containsName(String name) {
        if (name.isEmpty()) {
            return (newHero, cq, cb) -> cb.isNotNull(newHero);
        }
        return (newHero, cq, cb) -> cb.like(newHero.get("name"), "%" + name + "%");
    }
    static Specification<Hero> orderByName() {
        return (newHero, cq, cb) -> {
            cq.orderBy(cb.asc(newHero.get("name")));
            return cb.isNotNull(newHero);
        };
    }    static Specification<Hero> test(String teste) {
        return (newHero, cq, cb) -> {
            return cb.isNotNull(newHero);
        };
    }
}
