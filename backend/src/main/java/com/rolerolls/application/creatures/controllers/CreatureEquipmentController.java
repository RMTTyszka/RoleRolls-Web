package com.rolerolls.application.creatures.controllers;

import com.rolerolls.application.creatures.CreaturesService;
import com.rolerolls.application.creatures.dtos.CreatureDto;
import com.rolerolls.application.creatures.dtos.CreatureEquipInput;
import com.rolerolls.domain.creatures.Creature;
import com.rolerolls.domain.creatures.CreatureRepository;
import com.rolerolls.domain.creatures.equipments.services.CreatureEquipmentService;
import com.rolerolls.domain.creatures.equipments.services.dtos.EquipItemValidationType;
import com.rolerolls.shared.ValidationResult;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
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
    ResponseEntity<CreatureDto> equipOffWeapon(@PathVariable UUID id, @RequestBody CreatureEquipInput input) throws Exception {
        Creature creature = creatureRepository.findById(id).get();
        ValidationResult<Creature, EquipItemValidationType> result = equipmentService.equipOffWeapon(creature, input.itemId);
        if (result.isSuccess()) {
            CreatureDto creatureDto = creaturesService.update(result.getOutput());
            return new ResponseEntity<CreatureDto>(creatureDto, HttpStatus.OK);
        } else {
            return new ResponseEntity(result.getErrorType(), HttpStatus.UNPROCESSABLE_ENTITY);
        }
    }
}
