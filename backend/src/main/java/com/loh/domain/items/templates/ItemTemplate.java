package com.loh.domain.items.templates;

import com.loh.shared.DefaultEntity;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.DiscriminatorColumn;
import javax.persistence.Inheritance;
import javax.persistence.InheritanceType;

@javax.persistence.Entity
@Inheritance(strategy = InheritanceType.SINGLE_TABLE)
@DiscriminatorColumn(name = "ItemType")
public class ItemTemplate extends DefaultEntity {

	@Getter @Setter
	protected String name;

	@Getter @Setter
	protected String description;

	@Getter @Setter
	protected Integer value;

	@Getter @Setter
	protected ItemTemplateType itemTemplateType;

}
