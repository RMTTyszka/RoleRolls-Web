package com.loh.items;

import com.loh.shared.DefaultEntity;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.MappedSuperclass;


@MappedSuperclass
public class Items extends DefaultEntity {

	@Getter @Setter
	protected String name;

	@Getter @Setter
	protected String description;

}
