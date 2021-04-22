package com.rolerolls.application.campaigns.master.tools.services.items.instantiation;

import com.rolerolls.application.campaigns.master.tools.services.MasterEquipmentService;
import com.rolerolls.domain.creatures.Creature;
import com.rolerolls.domain.creatures.CreatureRepository;
import com.rolerolls.domain.items.templates.ItemTemplateRepository;
import com.rolerolls.domain.items.templates.ItemTemplateType;
import com.rolerolls.domain.items.equipables.weapons.base.BaseWeapon;
import com.rolerolls.domain.items.equipables.weapons.base.BaseWeaponRepository;
import com.rolerolls.domain.items.equipables.weapons.models.WeaponModel;
import com.rolerolls.utils.creatures.CreatureInstantiatorForTestService;
import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.mockito.Mockito;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.junit4.SpringRunner;

import java.util.UUID;

@RunWith(SpringRunner.class )
@SpringBootTest
public class MasterInstantiateItemTests {

    @Autowired
    MasterEquipmentService masterEquipmentService;
    @Autowired
    ItemTemplateRepository itemTemplateRepository;
    @Autowired
    BaseWeaponRepository baseWeaponRepository;
    @Autowired
    CreatureInstantiatorForTestService creatureInstantiatorForTestService;
    @Autowired
    CreatureRepository creatureRepository;

    @Test
    public void TestInstantiateItem() throws Exception {
        WeaponModel itemTemplate = new WeaponModel();
        BaseWeapon baseWeapon = baseWeaponRepository.save(new BaseWeapon());
        itemTemplate.setBaseWeapon(baseWeapon);
        itemTemplate.setItemTemplateType(ItemTemplateType.Weapon);
   /*     itemTemplate = itemTemplateRepository.save(itemTemplate);
        Creature creature = creatureInstantiatorForTestService.Instantiate();*/

        masterEquipmentService.InstantiateItemForCreatureFromCombat(null, UUID.fromString("8eec69b9-8331-4a0e-99e6-c62bdaa6a428"), itemTemplate.getId(), 1, 1);
        // creature = creatureRepository.findById(creature.getId()).get();
        // Assertions.assertThat(creature.getInventory().getItems().stream().map(e -> e.getItemTemplateId()).collect(Collectors.toList()).contains(itemTemplate.getId()));
    }

    @Before
    public void setUp() {
        Creature creature = new Creature();
        creature.setId(UUID.fromString("8eec69b9-8331-4a0e-99e6-c62bdaa6a428"));
        Mockito.when(creatureRepository.findById(creature.getId()).get())
                .thenReturn(creature);
    }
}
