package com.loh.domain.items.instances;

import com.loh.domain.items.ItemTemplate;
import com.loh.domain.items.equipables.armors.instances.ArmorInstance;
import com.loh.domain.items.equipables.armors.instances.ArmorInstanceRepository;
import com.loh.domain.items.equipables.armors.models.ArmorModel;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class ItemInstantiator {

    @Autowired
    ArmorInstanceRepository armorInstanceRepository;

    public ItemInstance instantiate(ItemTemplate itemTemplate, Integer level) {
        return instantiate(itemTemplate, level, false);
    }
    public ItemInstance instantiate(ItemTemplate itemTemplate, Integer level, boolean autoSave) {
        switch (itemTemplate.getItemTemplateType()) {
            case Weapon:
                break;
            case Armor:
                ArmorInstance armorInstance = new ArmorInstance((ArmorModel) itemTemplate, level);
                if (autoSave) {
                    armorInstance = armorInstanceRepository.save(armorInstance);
                }
                return armorInstance;
            case Glove:
                break;
            case Arms:
                break;
            case Ring:
                break;
            case Neck:
                break;
            case Boot:
                break;
            case Belt:
                break;
            case Head:
                break;
            default:
                throw new IndexOutOfBoundsException();
        }
        return null;
    }

}
