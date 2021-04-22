package com.rolerolls.application.items.equipables.armors.models;


import com.rolerolls.domain.items.equipables.armors.templates.ArmorTemplate;
import com.rolerolls.domain.items.equipables.armors.templates.ArmorTemplateRepository;
import com.rolerolls.shared.BaseCrudResponse;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import java.util.UUID;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/armorModels",  produces = "application/json; charset=UTF-8")
public class ArmorModelController {

    @Autowired
    private ArmorTemplateRepository repository;

    @GetMapping(path="/allPaged")
    public @ResponseBody
    Iterable<ArmorTemplate> getAllPaged(@RequestParam String filter, @RequestParam int skipCount, @RequestParam int maxResultCount) {

        Pageable paged = PageRequest.of(skipCount, maxResultCount);
        if (filter.isEmpty() || filter == null) {
            return repository.findAll(paged);
        }
        return repository.findAllByNameIgnoreCaseContaining(filter, paged);
    }
    @GetMapping(path="/allFiltered")
    public @ResponseBody
    Iterable<ArmorTemplate> getAllFiltered(@RequestParam String filter) {
        // This returns a JSON or XML with the users
        if (filter.isEmpty() || filter == null) {
            return repository.findAll();
        }
        return repository.findAllByNameIgnoreCaseContaining(filter);
    }
    @GetMapping(path="/find")
    public @ResponseBody
    ArmorTemplate getArmor(@RequestParam UUID id) {

        ArmorTemplate armor = repository.findById(id).get();

        return armor;

    }
    @GetMapping(path="/getNew")
    public @ResponseBody
    ArmorTemplate getNewArmor() {
        return new ArmorTemplate();
    }
    @PutMapping(path="/update")
    public @ResponseBody
    BaseCrudResponse<ArmorTemplate> updateArmor(@RequestBody ArmorTemplate Armor) {

        return saveAndGetArmorBaseCrudResponse(Armor);
    }

    private BaseCrudResponse<ArmorTemplate> saveAndGetArmorBaseCrudResponse(ArmorTemplate Armor) {
        ArmorTemplate updatedArmor = repository.save(Armor);
        BaseCrudResponse response = new BaseCrudResponse<ArmorTemplate>();
        response.success = true;
        response.entity = updatedArmor;
        return response;
    }

    @PostMapping(path="/create")
    public @ResponseBody
    BaseCrudResponse<ArmorTemplate> addArmor(@RequestBody ArmorTemplate Armor) {

        return saveAndGetArmorBaseCrudResponse(Armor);
    }
    @DeleteMapping(path="/delete")
    public @ResponseBody
    BaseCrudResponse<ArmorTemplate> deleteArmor(@RequestParam UUID id) {

        ArmorTemplate Armor = repository.findById(id).get();
        BaseCrudResponse response = new BaseCrudResponse();
        repository.deleteById(id);
        response.success = true;
        response.entity = Armor;

        return response;
    }

}
