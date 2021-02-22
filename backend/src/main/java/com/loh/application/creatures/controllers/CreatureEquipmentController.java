package com.loh.application.creatures.controllers;

import com.loh.application.creatures.CreaturesService;
import com.loh.application.creatures.dtos.CreatureDto;
import com.loh.application.creatures.dtos.CreatureEquipInput;
import com.loh.domain.creatures.Creature;
import com.loh.domain.creatures.CreatureRepository;
import com.loh.domain.creatures.equipments.CreatureEquipmentService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import java.util.UUID;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(produces = "application/json; charset=UTF-8")
public class CreatureEquipmentController {

    @Autowired
    CreatureEquipmentService equipmentService;

    @Autowired
    CreaturesService creaturesService;

    @Autowired
    CreatureRepository creatureRepository;

    @PutMapping(path="creatures/{id}/equipment/armor")
    public @ResponseBody
    CreatureDto equipArmor(@PathVariable UUID id, @RequestBody CreatureEquipInput input) {
        Creature creature = creatureRepository.findById(id).get();
        creature = equipmentService.equipArmor(creature, input.itemId);
        CreatureDto creatureDto = creaturesService.update(creature);
        return creatureDto;
    }
    @PutMapping(path="creatures/{id}/equipment/main-weapon")
    public @ResponseBody
    CreatureDto equipMainWeapon(@PathVariable UUID id, @RequestBody CreatureEquipInput input) throws Exception {
        Creature creature = creatureRepository.findById(id).get();
        creature = equipmentService.equipMainWeapon(creature, input.itemId);
        CreatureDto creatureDto = creaturesService.update(creature);
        return creatureDto;
    }
    @PutMapping(path="creatures/{id}/equipment/off-weapon")
    public @ResponseBody
    CreatureDto equipOffWeapon(@PathVariable UUID id, @RequestBody CreatureEquipInput input) throws Exception {
        Creature creature = creatureRepository.findById(id).get();
        creature = equipmentService.equipOffWeapon(creature, input.itemId);
        CreatureDto creatureDto = creaturesService.update(creature);
        return creatureDto;
    }
}
