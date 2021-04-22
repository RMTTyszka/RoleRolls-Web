package com.rolerolls.application.items.templates;

import com.rolerolls.application.shared.DefaultEntityDto;
import com.rolerolls.domain.items.templates.ItemTemplate;
import com.rolerolls.domain.items.templates.ItemTemplateType;
import lombok.Getter;
import lombok.Setter;

public class ItemTemplateDto extends DefaultEntityDto {
    @Getter
    @Setter
    protected String name;

    @Getter @Setter
    protected String description;

    @Getter @Setter
    protected Integer value;

    @Getter @Setter
    protected ItemTemplateType itemTemplateType;

    public ItemTemplateDto() {
    }

    public ItemTemplateDto(ItemTemplate itemTemplate) {
        id = itemTemplate.getId();
        name = itemTemplate.getName();
        description = itemTemplate.getDescription();
        value = itemTemplate.getValue();
        itemTemplateType = itemTemplate.getItemTemplateType();
    }

}
