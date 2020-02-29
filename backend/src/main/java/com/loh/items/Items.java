package com.loh.items;

import com.loh.shared.Entity;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.MappedSuperclass;
import javax.persistence.Transient;


@MappedSuperclass
public class Items extends Entity {

	@Getter @Setter
	protected Integer level = 1;
	
	@Getter @Setter
	protected String name;

	@Getter @Setter
	protected String description;
	@Getter @Setter @Transient
	protected Integer bonus = level / 2;
}
