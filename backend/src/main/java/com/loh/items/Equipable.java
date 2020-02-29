package com.loh.items;

import com.loh.powers.Power;
import com.loh.shared.Bonus;
import com.loh.shared.Bonuses;
import com.loh.shared.IHaveBonuses;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import java.util.List;

@MappedSuperclass
public class Equipable extends Items implements IHaveBonuses {

	@Getter @Setter
	protected String SpecialName;

	@Getter @Setter
	protected EquipableSlot slot;
	
	@ElementCollection
	@CollectionTable()
	@Getter @Setter
	protected List<Bonus> bonuses = new Bonuses();

	@OneToOne
	@Getter @Setter
	protected ItemMaterial material;

	@Getter @Setter
	@ManyToOne
	protected Power power;
}
