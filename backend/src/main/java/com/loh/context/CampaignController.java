package com.loh.context;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/campaigns",  produces = "application/json; charset=UTF-8")
public class CampaignController {

	@Autowired
	private CampaignRepository campaignRepository;
	
	@GetMapping(path="/all")
	public @ResponseBody Iterable<Campaign> getAllCampaigns() {
		
		// This returns a JSON or XML with the users
		return campaignRepository.findAll();
	}
}
