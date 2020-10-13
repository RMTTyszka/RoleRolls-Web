package com.loh.shared;

public class LevelDetails {

	public Integer expToNextLevel;
	
	public Integer maxAttributesBonusPoints;
	
	public Integer totalAttributesBonusPoints;
	
	public Integer totalInitialAttributes;
	
	public Integer maxInitialAttributesPoints;
	
	public Integer maxSkillPoints;
	
	public Integer totalSkillPoints;
	
	
	public LevelDetails(Integer level) {
		
		expToNextLevel = calculateExpToNextLevel(level);
		maxAttributesBonusPoints = level - 1;
		totalAttributesBonusPoints = (level - 1) * 2;
		maxInitialAttributesPoints =  8 + 6;
		totalInitialAttributes = 8 + 6 + 4 + 2 + 2 + 8*6;
		totalSkillPoints = (level + 2) * 6;
		maxSkillPoints = level/2 + 4;
		
	}
		
	  
	public static Integer calculateExpToNextLevel(Integer level) {
		if (level > 1) {
			return calculateExpToNextLevel(level - 1) + 
              5 * x(level);
		} else {
			return 500;
		}
	}
	public static Integer x (Integer level) {
	    if (level > 1) {
	        return x(level - 1) + 50 * (level - 1);
	    } else {
	        return 100;
	    }
	}
	
	
	
}
