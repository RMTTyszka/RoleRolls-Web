package com.loh.application.items.equipables.templates;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.loh.domain.items.equipables.EquipableTemplateRepository;
import com.loh.domain.universes.UniverseType;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/equipable-templates",  produces = "application/json; charset=UTF-8") // This means URL's start with /demo (after Application path)
public class EquipableTemplateController {

    @Autowired
    private EquipableTemplateRepository equipableTemplateRepository;

    @GetMapping()
    public @ResponseBody
    Page<EquipableTemplateDto> getList(@RequestParam String filter, @RequestParam int skipCount, @RequestParam int maxResultCount, @RequestHeader(value = "universe-type", defaultValue = "LandOfHeroes") UniverseType universeType) throws JsonProcessingException {
        Pageable paged = PageRequest.of(skipCount, maxResultCount);
        if (filter.isEmpty() || filter == null) {
            Page<EquipableTemplateDto> list =  equipableTemplateRepository.findAll(paged).map(e -> new EquipableTemplateDto(e));
            return list;
        } else {
            return equipableTemplateRepository.findAll(paged).map(e -> new EquipableTemplateDto(e));
        }
    }
}
