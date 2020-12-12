package com.loh.application.items.equipables.armors.models;


import com.loh.domain.items.equipables.armors.models.ArmorModel;
import com.loh.domain.items.equipables.armors.models.ArmorModelRepository;
import com.loh.shared.BaseCrudResponse;
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
    private ArmorModelRepository repository;

    @GetMapping(path="/allPaged")
    public @ResponseBody
    Iterable<ArmorModel> getAllPaged(@RequestParam String filter, @RequestParam int skipCount, @RequestParam int maxResultCount) {

        Pageable paged = PageRequest.of(skipCount, maxResultCount);
        if (filter.isEmpty() || filter == null) {
            return repository.findAll(paged);
        }
        return repository.findAllByNameIgnoreCaseContaining(filter, paged);
    }
    @GetMapping(path="/allFiltered")
    public @ResponseBody
    Iterable<ArmorModel> getAllFiltered(@RequestParam String filter) {
        // This returns a JSON or XML with the users
        if (filter.isEmpty() || filter == null) {
            return repository.findAll();
        }
        return repository.findAllByNameIgnoreCaseContaining(filter);
    }
    @GetMapping(path="/find")
    public @ResponseBody
    ArmorModel getArmor(@RequestParam UUID id) {

        ArmorModel armor = repository.findById(id).get();

        return armor;

    }
    @GetMapping(path="/getNew")
    public @ResponseBody
    ArmorModel getNewArmor() {
        return new ArmorModel();
    }
    @PutMapping(path="/update")
    public @ResponseBody
    BaseCrudResponse<ArmorModel> updateArmor(@RequestBody ArmorModel Armor) {

        return saveAndGetArmorBaseCrudResponse(Armor);
    }

    private BaseCrudResponse<ArmorModel> saveAndGetArmorBaseCrudResponse(ArmorModel Armor) {
        ArmorModel updatedArmor = repository.save(Armor);
        BaseCrudResponse response = new BaseCrudResponse<ArmorModel>();
        response.success = true;
        response.entity = updatedArmor;
        return response;
    }

    @PostMapping(path="/create")
    public @ResponseBody
    BaseCrudResponse<ArmorModel> addArmor(@RequestBody ArmorModel Armor) {

        return saveAndGetArmorBaseCrudResponse(Armor);
    }
    @DeleteMapping(path="/delete")
    public @ResponseBody
    BaseCrudResponse<ArmorModel> deleteArmor(@RequestParam UUID id) {

        ArmorModel Armor = repository.findById(id).get();
        BaseCrudResponse response = new BaseCrudResponse();
        repository.deleteById(id);
        response.success = true;
        response.entity = Armor;

        return response;
    }

}
