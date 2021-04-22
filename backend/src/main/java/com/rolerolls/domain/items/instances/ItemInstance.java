package com.rolerolls.domain.items.instances;

import com.rolerolls.domain.items.templates.ItemTemplate;
import com.rolerolls.shared.Entity;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.Column;
import javax.persistence.DiscriminatorColumn;
import javax.persistence.Inheritance;
import javax.persistence.InheritanceType;
import java.util.UUID;

@javax.persistence.Entity
@Inheritance(strategy = InheritanceType.SINGLE_TABLE)
@DiscriminatorColumn(name = "ItemType")
public class ItemInstance extends Entity {

    @Getter @Setter
    private Integer level;
    @Getter @Setter
    private String name;
    @Getter @Setter
    private double value;
    @Getter @Setter
    private Integer quantity;
    @Getter
    @Setter
    @Column(columnDefinition = "BINARY(16)")
    protected UUID itemTemplateId;



    public void addQuantity(Integer quantity) {
        this.quantity += quantity;
    }
    public void removeQuantity(Integer quantity) {
        this.quantity -= quantity;
        if (this.quantity < 0) {
            this.quantity = 0;
        }
    }

    public Integer getBonus() {
      return level / 2;
    }

    public ItemInstance() {
        level = 1;
    }

    public ItemInstance(ItemTemplate template) {
        super();
        itemTemplateId = template.getId();
    }

    public void levelUp(ItemInstanceRepository itemInstanceRepository) {
        level++;
        itemInstanceRepository.save(this);
    }
    public void levelUpForTest(ItemInstanceRepository itemInstanceRepository) {
        level++;
        itemInstanceRepository.save(this);
    }
}
