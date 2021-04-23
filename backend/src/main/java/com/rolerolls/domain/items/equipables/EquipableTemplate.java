package com.rolerolls.domain.items.equipables;

import com.rolerolls.domain.items.ItemMaterial;
import com.rolerolls.domain.items.templates.ItemTemplate;
import com.rolerolls.domain.powers.Power;
import com.rolerolls.shared.Bonus;
import com.rolerolls.shared.Bonuses;
import com.rolerolls.shared.IHaveBonuses;
import lombok.Getter;
import lombok.Setter;
import org.hibernate.annotations.Fetch;

import javax.persistence.*;
import java.util.List;

@MappedSuperclass
public class EquipableTemplate extends ItemTemplate implements IHaveBonuses {

	@Getter @Setter
	protected String specialName;

	@Getter @Setter
	protected EquipableSlot slot;
	
	@ElementCollection(fetch = FetchType.EAGER)
	@CollectionTable()
	@Fetch(org.hibernate.annotations.FetchMode.SUBSELECT)
	@Getter @Setter
	protected List<Bonus> bonuses = new Bonuses();

	@OneToOne
	@Getter @Setter
	protected ItemMaterial material;

	@Getter @Setter
	@ManyToOne
	protected Power power;

	public EquipableTemplate() {
		super();
	}
}
