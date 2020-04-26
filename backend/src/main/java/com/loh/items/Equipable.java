package com.loh.items;

import com.loh.powers.Power;
import com.loh.shared.Bonus;
import com.loh.shared.Bonuses;
import com.loh.shared.IHaveBonuses;
import lombok.Getter;
import lombok.Setter;
import org.hibernate.annotations.Fetch;

import javax.persistence.*;
import java.util.List;

@MappedSuperclass
public class Equipable extends Item implements IHaveBonuses {

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

	public Equipable() {
		super();
	}
}
