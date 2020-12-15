package com.loh.application.creatures.heroes;


import com.loh.application.creatures.heroes.dtos.AddItemsInput;
import com.loh.application.creatures.heroes.dtos.HeroDto;
import com.loh.application.creatures.heroes.dtos.NewHeroDto;
import com.loh.application.creatures.mappers.HeroMapper;
import com.loh.domain.creatures.heroes.Hero;
import com.loh.domain.creatures.heroes.HeroRepository;
import com.loh.domain.creatures.heroes.HeroService;
import com.loh.domain.items.instances.ItemInstance;
import com.loh.shared.BaseCrudController;
import com.loh.shared.BaseCrudResponse;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.domain.Specification;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import java.util.UUID;

import static com.loh.authentication.LohUserDetails.currentUserId;
import static org.springframework.data.jpa.domain.Specification.where;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/heroes",  produces = "application/json; charset=UTF-8")
public class HeroController extends BaseCrudController<Hero, NewHeroDto, HeroDto, HeroRepository> {
    @Autowired
    private HeroRepository heroRepository;
    @Autowired
    private HeroService heroService;
    @Autowired
    private HeroMapper mapper;

    public HeroController(HeroRepository repository) {
        super(repository);
        this.repository = repository;
    }

    @Override
    public @ResponseBody
    Page<Hero> getList(@RequestParam(required = false) String filter, @RequestParam int skipCount, @RequestParam int maxResultCount) {
        Pageable paged = PageRequest.of(skipCount, maxResultCount);
        UUID userId = currentUserId();
        Page<Hero> heroes = heroRepository.findAll(
                where((fromPlayer(userId).or(fromCreator(userId))).and(containsName(filter).and(orderByName()))), paged);
        return heroes;
    }

    @Override
    public BaseCrudResponse<Hero> update(@RequestBody HeroDto heroDto) {
        Hero hero = updateInputToEntity(heroDto);
        hero =  heroService.update(hero);
        BaseCrudResponse<Hero> output = new BaseCrudResponse<Hero>(true, "Successfully updated hero", hero);
        return output;
    }

    @Override
    public @ResponseBody
    BaseCrudResponse<Hero> add(@RequestBody NewHeroDto heroDto){

        try {
            Hero hero = heroService.create(heroDto.name, heroDto.race, heroDto.role, heroDto.level, heroDto.ownerId, currentUserId());
            BaseCrudResponse<Hero> output = new BaseCrudResponse<Hero>(true, "Successfully created hero", hero);
            return output;
        } catch (Exception e) {
            System.out.println(e.getStackTrace());
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
    @PutMapping(path="/addItems")
    public @ResponseBody
    BaseCrudResponse addItems(@RequestBody AddItemsInput input) {
        Hero hero = heroRepository.findById(input.heroId).get();

        for (ItemInstance item : input.items) {
            hero.getInventory().addItem(item);
        }
        heroRepository.save(hero);

        return new BaseCrudResponse(true, "");
    }

    static Specification<Hero> containsName(String name) {
        if (name.isEmpty()) {
            return (newHero, cq, cb) -> cb.isNotNull(newHero);
        }
        return (newHero, cq, cb) -> cb.like(newHero.get("name"), "%" + name + "%");
    }
    static Specification<Hero> fromPlayer(UUID ownerId) {
        if (ownerId == null) {
            return (newHero, cq, cb) -> cb.isNotNull(newHero);
        }
        return (newHero, cq, cb) -> cb.equal(newHero.get("ownerId"), ownerId);
    }
    static Specification<Hero> fromCreator(UUID ownerId) {
        if (ownerId == null) {
            return (newHero, cq, cb) -> cb.isNotNull(newHero);
        }
        return (newHero, cq, cb) -> cb.equal(newHero.get("creatorId"), ownerId);
    }
    static Specification<Hero> orderByName() {
        return (root, criteriaQuery, criteriaBuilder) -> {
            criteriaQuery.orderBy(criteriaBuilder.asc(root.get("name")));
            return criteriaBuilder.isNotNull(root);
        };
    }    static Specification<Hero> test(String teste) {
        return (newHero, cq, cb) -> {
            return cb.isNotNull(newHero);
        };
    }

    @Override
    public NewHeroDto getnew() {
        return new NewHeroDto();
    }

    @Override
    protected Hero createInputToEntity(NewHeroDto newHeroDto) {
        return null;
    }

    @Override
    protected Hero updateInputToEntity(HeroDto hero) {
        return mapper.map(hero);
    }
}
