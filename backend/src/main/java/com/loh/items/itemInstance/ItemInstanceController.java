package com.loh.items.itemInstance;

import com.loh.items.ItemTemplate;
import com.loh.items.equipable.armors.armorModel.ArmorModel;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import javax.json.Json;
import javax.json.JsonObject;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/ItemInstance",  produces = "application/json; charset=UTF-8") // This means URL's start with /demo (after Application path)
public class ItemInstanceController {


    @Autowired
    private ItemInstanceRepository itemInstanceRepository;

    @PostMapping(path="/create")
    public @ResponseBody
    JsonObject instantiate(@RequestBody ItemTemplate itemTemplate) {
        // This returns a JSON or XML with the users
        ArmorModel armorModel = (ArmorModel) itemTemplate;

        return Json.createObjectBuilder()
                .add("text", "race saved with success").build();
    }
}
