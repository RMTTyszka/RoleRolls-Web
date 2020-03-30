package com.loh.creatures.heroes;


import com.loh.creatures.heroes.inventory.NewHeroDto;
import com.loh.shared.BaseCrudResponse;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.jpa.domain.Specification;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import java.util.UUID;

import static org.springframework.data.jpa.domain.Specification.where;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/hero",  produces = "application/json; charset=UTF-8")
public class HeroController {
    @Autowired
    private HeroRepository heroRepository;
    @Autowired
    private HeroService heroService;



    @GetMapping(path="/allFiltered")
    public @ResponseBody
    Iterable<Hero> getAllHeroes(@RequestParam(required = false) String filter) {

        Iterable<Hero> heroes = heroRepository.findAll(where(containsName(filter).and(orderByName())));
        return heroes;
    }
    // This returns a JSON or XML with the users
    @GetMapping(path="/find")
    public @ResponseBody
    Hero getHero(@RequestParam UUID id) throws NoSuchFieldException, SecurityException, IllegalArgumentException, IllegalAccessException {
        // This returns a JSON or XML with the users
        Hero hero = heroRepository.findById(id).get();
        return hero;
    }
    @GetMapping(path="/getNew")
    public @ResponseBody
    NewHeroDto getNewHero() throws NoSuchFieldException, SecurityException, IllegalArgumentException, IllegalAccessException {
        // This returns a JSON or XML with the users
        //	System.out.println(io.swagger.util.Json.pretty(monster));
        NewHeroDto hero = new NewHeroDto();
        return hero;
    }
    @PutMapping(path="/update")
    public @ResponseBody
    BaseCrudResponse<Hero> updateHero(@RequestBody Hero heroDto) {
        Hero hero =  heroService.update(heroDto);
        BaseCrudResponse<Hero> output = new BaseCrudResponse<Hero>(true, "Successfully updated hero", hero);
        return output;
    }

    @PostMapping(path="/create")
    public @ResponseBody
    BaseCrudResponse<Hero> createHero(@RequestBody NewHeroDto heroDto){

        try {
            Hero hero = heroService.create(heroDto.name, heroDto.race, heroDto.role);
            BaseCrudResponse<Hero> output = new BaseCrudResponse<Hero>(true, "Successfully created hero", hero);
            return output;
        } catch (Exception e) {
            return new BaseCrudResponse<Hero>(false, e.getMessage(), null);
        }

    }

    @DeleteMapping(path="/delete")
    public @ResponseBody
    BaseCrudResponse<Hero> deleteHero(@RequestParam UUID id) {

        try {
            heroRepository.deleteById(id);
            return new BaseCrudResponse<Hero>(true, "Successfully deleted hero", null);
        } catch (Exception e) {
            return new BaseCrudResponse<Hero>(false, e.getMessage(), null);
        }

    }
    @DeleteMapping(path="/deleteAllDummies")
    public @ResponseBody
    BaseCrudResponse<Hero> deleteAllDummies() {

        try {
            heroRepository.deleteAllByNameContaining("Dummy");
            return new BaseCrudResponse<Hero>(true, "Successfully deleted heroes", null);
        } catch (Exception e) {
            return new BaseCrudResponse<Hero>(false, e.getMessage(), null);
        }

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
