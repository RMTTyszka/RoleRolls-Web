package com.rolerolls.application.items.equipables.templates;

import com.rolerolls.application.items.templates.ItemTemplateDto;
import com.rolerolls.domain.items.ItemMaterial;
import com.rolerolls.domain.items.equipables.EquipableSlot;
import com.rolerolls.domain.items.equipables.EquipableTemplate;
import com.rolerolls.domain.powers.Power;
import com.rolerolls.shared.Bonus;
import com.rolerolls.shared.Bonuses;
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
