package com.loh.application.items.templates;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.loh.domain.items.templates.ItemTemplateRepository;
import com.loh.domain.universes.UniverseType;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/item-templates",  produces = "application/json; charset=UTF-8") // This means URL's start with /demo (after Application path)
public class ItemTemplateController {

    @Autowired
    private ItemTemplateRepository itemTemplateRepository;

    @GetMapping()
    public @ResponseBody
    Page<ItemTemplateDto> getList(@RequestParam String filter, @RequestParam int skipCount, @RequestParam int maxResultCount, @RequestHeader(value = "universe-type", defaultValue = "LandOfHeroes") UniverseType universeType) throws JsonProcessingException {
        Pageable paged = PageRequest.of(skipCount, maxResultCount);
        if (filter.isEmpty() || filter == null) {
            Page<ItemTemplateDto> list =  itemTemplateRepository.findAll(paged).map(e -> new ItemTemplateDto(e));
            return list;
        } else {
            return itemTemplateRepository.findAll(paged).map(e -> new ItemTemplateDto(e));
        }
    }
}
