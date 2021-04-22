package com.rolerolls.application.creatures.heroes;

import com.rolerolls.domain.creatures.heroes.HeroShopService;
import com.rolerolls.application.creatures.heroes.dtos.BuyItemInput;
import com.rolerolls.application.creatures.heroes.dtos.BuyItemOutput;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/HeroShop",  produces = "application/json; charset=UTF-8")// This means URL's start with /demo (after Application path)
public class HeroShopController {

    @Autowired
    private HeroShopService heroShopService;

    @PostMapping(path="/buy")
    public @ResponseBody
    BuyItemOutput buyItem(@RequestBody BuyItemInput input){
       BuyItemOutput output = heroShopService.buy(input.heroId, input.shopId, input.shopItemId, input.quantity);
       return output;
    }
}
