import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {InventoryArmorSelectComponent} from './inventory/inventory-armor-select/inventory-armor-select.component';
import {AutoCompleteModule} from 'primeng/autocomplete';
import {FieldsetModule} from 'primeng/fieldset';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {BaseInventorySelectComponent} from './inventory/base-inventory-select/base-inventory-select.component';
import {InventoryWeaponSelectComponent} from './inventory/inventory-weapon-select/inventory-weapon-select.component';
import {InventoryGlovesSelectComponent} from './inventory/inventory-glove-select/inventory-gloves-select.component';
import {InventoryBeltSelectComponent} from './inventory/inventory-belt-select/inventory-belt-select.component';
import {InventoryHeadpieceSelectComponent} from './inventory/inventory-headpiece-select/inventory-headpiece-select.component';
import {CreatureResistancesComponent} from './creature-resistances/creature-resistances.component';
import {InventoryNeckaccesorySelectComponent} from './inventory/inventory-neckaccesory-select/inventory-neckaccesory-select.component';
import {InventoryRingSelectComponent} from './inventory/inventory-ring-select/inventory-ring-select.component';
import {AttackDetailsComponent} from './attack-details/attack-details.component';
import {PanelModule} from 'primeng/panel';

import {CombatCreatureSelectComponent} from './combat-creature-select/combat-creature-select.component';
import {DropdownModule} from 'primeng/dropdown';
import { CreatureBaseSelectComponent } from './creature-base-select/creature-base-select.component';
import {OverlayPanelModule} from 'primeng/overlaypanel';
import {TooltipModule} from 'primeng/tooltip';
import { CreatureDetailsComponent } from './creature-details/creature-details.component';
import { CreatureEditorComponent } from './creature-editor/creature-editor.component';
import {EquipmentComponent} from './equipment/equipment.component';
import {AccordionModule} from 'primeng/accordion';
import {SharedModule} from '../shared/shared.module';
import {TabViewModule} from 'primeng/tabview';
import {ShopModule} from '../shop/shop.module';
import {RaceSharedModule} from '../races/shared/race-shared.module';
import {RolesSharedModule} from '../roles/roles-shared/roles-shared.module';
import {CreatureStatsComponent} from './creature-stats/creature-stats.component';
import {HeroFundsComponent} from './hero-funds/hero-funds.component';
import {InventoryComponent} from './inventory/inventory/inventory.component';
import { CreatureSkillsComponent } from './creature-skills/creature-skills.component';
import { RollsCardComponent } from './rolls-card/rolls-card.component';
import {InputTextModule} from 'primeng/inputtext';
import { CreatureAttributeDetailsComponent } from './creature-details/creature-attribute-details/creature-attribute-details.component';
import { CreatureInventoryDetailsComponent } from './creature-details/creature-inventory-details/creature-inventory-details.component';
import {SelectButtonModule} from 'primeng/selectbutton';
import { CreatureEquipmentDetailsComponent } from './creature-details/creature-equipment-details/creature-equipment-details.component';
import {ProgressBarModule} from 'primeng/progressbar';


@NgModule({
    declarations: [HeroFundsComponent, CreatureStatsComponent,
        InventoryArmorSelectComponent, BaseInventorySelectComponent, InventoryWeaponSelectComponent,
        InventoryGlovesSelectComponent, InventoryBeltSelectComponent, InventoryHeadpieceSelectComponent,
        CreatureResistancesComponent, InventoryNeckaccesorySelectComponent, InventoryRingSelectComponent,
        AttackDetailsComponent, CombatCreatureSelectComponent, CreatureBaseSelectComponent, CreatureDetailsComponent,
        CreatureEditorComponent, EquipmentComponent, InventoryComponent, CreatureSkillsComponent, RollsCardComponent, CreatureAttributeDetailsComponent, CreatureInventoryDetailsComponent, CreatureEquipmentDetailsComponent],
    exports: [
        HeroFundsComponent, CreatureStatsComponent, EquipmentComponent,
        InventoryArmorSelectComponent, InventoryWeaponSelectComponent,
        InventoryGlovesSelectComponent, InventoryGlovesSelectComponent,
        InventoryBeltSelectComponent, InventoryBeltSelectComponent,
        InventoryHeadpieceSelectComponent, CreatureResistancesComponent,
        InventoryNeckaccesorySelectComponent, InventoryRingSelectComponent,
        AttackDetailsComponent, CombatCreatureSelectComponent, CreatureBaseSelectComponent, CreatureDetailsComponent, CreatureEditorComponent, InventoryComponent
    ],
    imports: [
        CommonModule,
        FieldsetModule,
        AutoCompleteModule,
        ReactiveFormsModule,
        PanelModule,

        DropdownModule,
        FormsModule,
        OverlayPanelModule,
        TooltipModule,
        AccordionModule,
        SharedModule,
        TabViewModule,
        ShopModule,
        RaceSharedModule,
        RolesSharedModule,
        InputTextModule,
        SelectButtonModule,
        ProgressBarModule
    ]
})
export class CreaturesSharedModule { }
