package com.rolerolls.domain.creatures.monsters;


import com.rolerolls.shared.BaseCrudController;
import com.rolerolls.shared.BaseCrudResponse;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;

import java.util.UUID;

import static com.rolerolls.authentication.LohUserDetails.currentUserId;

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
    public NewMonsterDto getNew() {
        return new NewMonsterDto();
    }

    @Override
    protected Monster createInputToEntity(NewMonsterDto newMonsterDto) {
        return null;
    }

    @Override
    protected Monster updateInputToEntity(UUID id, Monster monster) {
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
