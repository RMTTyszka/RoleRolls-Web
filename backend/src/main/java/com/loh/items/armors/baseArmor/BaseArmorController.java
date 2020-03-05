package com.loh.items.armors.baseArmor;


import com.loh.shared.BaseCrudResponse;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import java.util.UUID;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/baseArmors",  produces = "application/json; charset=UTF-8")
public class BaseArmorController {

    @Autowired
    private BaseArmorRepository repository;


    @GetMapping(path="/allPaged")
    public @ResponseBody
    Iterable<BaseArmor> getAllPaged(@RequestParam String filter, @RequestParam int skipCount, @RequestParam int maxResultCount) {

        Pageable paged = PageRequest.of(skipCount, maxResultCount);
        if (filter.isEmpty() || filter == null) {
            return repository.findAll(paged);
        }
        return repository.findAllByNameIgnoreCaseContaining(filter, paged);
    }
    @GetMapping(path="/allFiltered")
    public @ResponseBody
    Iterable<BaseArmor> getAllFiltered(@RequestParam String filter) {
        // This returns a JSON or XML with the users
        if (filter.isEmpty() || filter == null) {
            return repository.findAll();
        }
        return repository.findAllByNameIgnoreCaseContaining(filter);
    }
    @GetMapping(path="/find")
    public @ResponseBody
    BaseArmor get(@RequestParam UUID id) {

        BaseArmor armor = repository.findById(id).get();

        return armor;

    }
    @GetMapping(path="/getNew")
    public @ResponseBody BaseArmor getNew() {
        return new BaseArmor();
    }
    @PutMapping(path="/update")
    public @ResponseBody
    BaseCrudResponse<BaseArmor> update(@RequestBody BaseArmor baseArmor) {

        return saveAndGetBaseArmorBaseCrudResponse(baseArmor);
    }

    private BaseCrudResponse<BaseArmor> saveAndGetBaseArmorBaseCrudResponse(BaseArmor baseArmor) {
        BaseArmor updatedBaseArmor = repository.save(baseArmor);
        BaseCrudResponse response = new BaseCrudResponse<BaseArmor>();
        response.success = true;
        response.entity = updatedBaseArmor;
        return response;
    }

    @PostMapping(path="/create")
    public @ResponseBody
    BaseCrudResponse<BaseArmor> create(@RequestBody BaseArmor baseArmor) {

        return saveAndGetBaseArmorBaseCrudResponse(baseArmor);
    }
    @DeleteMapping(path="/delete")
    public @ResponseBody
    BaseCrudResponse<BaseArmor> delete(@RequestParam UUID id) {

        BaseArmor baseArmor = repository.findById(id).get();
        BaseCrudResponse response = new BaseCrudResponse();
        if (!baseArmor.isSystemDefault()){
            repository.deleteById(id);
            response.success = true;
            response.entity = baseArmor;
        } else {
            response.success = false;
            response.message = "cannot delete default base armorModel";
            response.entity = baseArmor;
        }

        return response;
    }

}
