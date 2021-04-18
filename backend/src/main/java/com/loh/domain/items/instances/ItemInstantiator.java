package com.loh.domain.items.instances;

import com.loh.domain.items.templates.ItemTemplate;
import com.loh.domain.items.equipables.armors.instances.ArmorInstance;
import com.loh.domain.items.equipables.armors.instances.ArmorInstanceRepository;
import com.loh.domain.items.equipables.armors.templates.ArmorTemplate;
import com.loh.domain.items.equipables.weapons.instances.WeaponInstance;
import com.loh.domain.items.equipables.weapons.instances.WeaponInstanceRepository;
import com.loh.domain.items.equipables.weapons.models.WeaponModel;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class ItemInstantiator {

    @Autowired
    ArmorInstanceRepository armorInstanceRepository;
    @Autowired
    WeaponInstanceRepository weaponInstanceRepository;

    public ItemInstance instantiate(ItemTemplate itemTemplate, Integer level, Integer quantity) {
        return instantiate(itemTemplate, level, quantity, false);
    }
    public ItemInstance instantiate(ItemTemplate itemTemplate, Integer level, Integer quantity, boolean autoSave) {
        switch (itemTemplate.getItemTemplateType()) {
            case Weapon:
                WeaponInstance weaponInstance = new WeaponInstance((WeaponModel) itemTemplate, level, quantity);
                if (autoSave) {
                    weaponInstance = weaponInstanceRepository.save(weaponInstance);
                }
                return weaponInstance;
            case Armor:
                ArmorInstance armorInstance = new ArmorInstance((ArmorTemplate) itemTemplate, level, quantity);
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
