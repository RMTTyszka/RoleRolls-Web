package com.loh.application.items.equipables.templates;

import com.loh.application.items.ItemTemplateDto;
import com.loh.domain.items.ItemMaterial;
import com.loh.domain.items.equipables.EquipableSlot;
import com.loh.domain.items.equipables.EquipableTemplate;
import com.loh.domain.powers.Power;
import com.loh.shared.Bonus;
import com.loh.shared.Bonuses;
import lombok.Getter;
import lombok.Setter;

import java.util.List;

public class EquipableTemplateDto extends ItemTemplateDto {
    @Getter
    @Setter
    protected String specialName;

    @Getter @Setter
    protected EquipableSlot slot;

    @Getter @Setter
    protected List<Bonus> bonuses = new Bonuses();

    @Getter @Setter
    protected ItemMaterial material;

    @Getter @Setter
    protected Power power;

    public EquipableTemplateDto() {
    }

    public EquipableTemplateDto(EquipableTemplate template) {
        super(template);
        specialName = template.getSpecialName();
        slot = template.getSlot();
        bonuses = template.getBonuses();
        material = template.getMaterial();
        power = template.getPower();
    }
}
