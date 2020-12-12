package com.loh.domain.creatures.monsters;


import com.loh.shared.BaseCrudController;
import com.loh.shared.BaseCrudResponse;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;

import java.util.UUID;

import static com.loh.authentication.LohUserDetails.currentUserId;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/monsters",  produces = "application/json; charset=UTF-8")
public class MonstersController extends BaseCrudController<Monster, NewMonsterDto, Monster, MonsterRepository> {


    @Autowired
    MonsterRepository repository;
    @Autowired
    MonsterService monsterService;

    @Autowired
    public MonstersController(MonsterRepository repository) {
        super(repository);
        this.repository = repository;
    }

    @Override
    public BaseCrudResponse<Monster> add(@RequestBody  NewMonsterDto dto) throws Exception {
        Monster monster = monsterService.create(dto.name, dto.monsterModel, currentUserId());
        BaseCrudResponse<Monster> output = new BaseCrudResponse<>(true, "Monster Successfuly Created", monster);
        return output;
    }

    @Override
    public NewMonsterDto getnew() {
        return new NewMonsterDto();
    }

    @Override
    protected Monster createInputToEntity(NewMonsterDto newMonsterDto) {
        return null;
    }

    @Override
    protected Monster updateInputToEntity(Monster monster) {
        return monster;
    }

    @Override
    public Page<Monster> filteredQuery(String filter, Pageable paged) {
        UUID ownerId = currentUserId();
        return repository.findAllByNameIgnoreCaseContainingAndOwnerId(filter, ownerId, paged);
    }
    @Override
    public Page<Monster> unfilteredQuery(Pageable paged) {
        UUID ownerId = currentUserId();
        return repository.findAllByOwnerId(ownerId, paged);
    }
}
