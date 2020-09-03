package com.loh.items.equipable.armors.armorInstance;


import com.loh.items.equipable.armors.armorInstance.dtos.InstantiateArmorByModelInput;
import com.loh.shared.BaseCrudResponse;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import java.util.UUID;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/armorInstance",  produces = "application/json; charset=UTF-8")
public class ArmorInstanceController {

    @Autowired
    private ArmorInstanceRepository repository;

    @GetMapping(path="/allPaged")
    public @ResponseBody
    Iterable<ArmorInstance> getAllPaged(@RequestParam String filter, @RequestParam int skipCount, @RequestParam int maxResultCount) {

        Pageable paged = PageRequest.of(skipCount, maxResultCount);
        if (filter.isEmpty() || filter == null) {
            return repository.findAll(paged);
        }
        return repository.findAllByNameIgnoreCaseContaining(filter, paged);
    }
    @GetMapping(path="/allFiltered")
    public @ResponseBody
    Iterable<ArmorInstance> getAllFiltered(@RequestParam String filter) {
        // This returns a JSON or XML with the users
        if (filter.isEmpty() || filter == null) {
            return repository.findAll();
        }
        return repository.findAllByNameIgnoreCaseContaining(filter);
    }
    @GetMapping(path="/find")
    public @ResponseBody
    ArmorInstance getArmor(@RequestParam UUID id) {

        ArmorInstance armor = repository.findById(id).get();

        return armor;

    }
    @GetMapping(path="/getNew")
    public @ResponseBody
    ArmorInstance getNewArmor() {
        return new ArmorInstance();
    }
    @PutMapping(path="/update")
    public @ResponseBody
    BaseCrudResponse<ArmorInstance> updateArmor(@RequestBody ArmorInstance Armor) {

        return saveAndGetArmorBaseCrudResponse(Armor);
    }

    private BaseCrudResponse<ArmorInstance> saveAndGetArmorBaseCrudResponse(ArmorInstance Armor) {
        ArmorInstance updatedArmor = repository.save(Armor);
        BaseCrudResponse response = new BaseCrudResponse<ArmorInstance>();
        response.success = true;
        response.entity = updatedArmor;
        return response;
    }

    @PostMapping(path="/create")
    public @ResponseBody
    BaseCrudResponse<ArmorInstance> addArmor(@RequestBody ArmorInstance Armor) {
        return saveAndGetArmorBaseCrudResponse(Armor);
    }
    @PostMapping(path="/instantiate")
    public @ResponseBody
    BaseCrudResponse<ArmorInstance> instantiate(@RequestBody InstantiateArmorByModelInput input) {
        ArmorInstance armor = new ArmorInstance(input.armorTemplate, input.level);
        return saveAndGetArmorBaseCrudResponse(armor);
    }
    @DeleteMapping(path="/delete")
    public @ResponseBody
    BaseCrudResponse<ArmorInstance> deleteArmor(@RequestParam UUID id) {

        ArmorInstance Armor = repository.findById(id).get();
        BaseCrudResponse response = new BaseCrudResponse();
        repository.deleteById(id);
        response.success = true;
        response.entity = Armor;

        return response;
    }

}
