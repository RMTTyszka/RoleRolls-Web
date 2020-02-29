package com.loh.shared;

import javax.persistence.Embeddable;
import java.util.Arrays;
import java.util.List;

@Embeddable
public class OldAttributes {
	public Integer strength;
	public Integer agility;
	public Integer vitality;
	public Integer wisdom;
	public Integer intuition;
	public Integer charisma;
	
	public Integer strengthBonusPoints;
	public Integer agilityBonusPoints;
	public Integer vitalityBonusPoints;
	public Integer wisdomBonusPoints;
	public Integer intuitionBonusPoints;
	public Integer charismaBonusPoints;
	
	public static List<String> getList() {
		return Arrays.asList("strength", "agility", "vitality", "wisdom","intuition","charisma");
		
	}


}

