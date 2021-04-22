package com.rolerolls.domain.powers;

import javax.persistence.Embeddable;
import javax.persistence.ManyToOne;

@Embeddable
public class PowerInstance {
	
	
	@ManyToOne 
	private Power power;
	
	private Integer totalUses;
	
	public PowerInstance() {}


    public Integer getTotalUses() {
		return totalUses;
	}

	public void setTotalUses(Integer totalUses) {
		this.totalUses = totalUses;
	}

	public Integer getTimesUsed() {
		return timesUsed;
	}

	public void setTimesUsed(Integer timesUsed) {
		this.timesUsed = timesUsed;
	}

	public Power getPower() {
		return power;
	}


	public void setPower(Power power) {
		this.power = power;
	}

	private Integer timesUsed;
}
