package com.rolerolls.application.items;

import com.rolerolls.domain.items.templates.ItemTemplate;
import com.rolerolls.domain.items.equipables.armors.templates.ArmorTemplate;
import com.rolerolls.domain.items.instances.ItemInstanceRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import javax.json.Json;
import javax.json.JsonObject;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/ItemInstances",  produces = "application/json; charset=UTF-8") // This means URL's start with /demo (after Application path)
public class ItemInstanceController {


    @Autowired
    private ItemInstanceRepository itemInstanceRepository;

    @PostMapping()
    public @ResponseBody
    JsonObject instantiate(@RequestBody ItemTemplate itemTemplate) {
        // This returns a JSON or XML with the users
        ArmorTemplate armorTemplate = (ArmorTemplate) itemTemplate;

        return Json.createObjectBuilder()
                .add("text", "race saved with success").build();
    }
}
