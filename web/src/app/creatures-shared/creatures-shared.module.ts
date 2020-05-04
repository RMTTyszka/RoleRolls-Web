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
import {FlexLayoutModule} from '@angular/flex-layout';
import {CombatCreatureSelectComponent} from './combat-creature-select/combat-creature-select.component';
import {DropdownModule} from 'primeng/dropdown';
import { CreatureBaseSelectComponent } from './creature-base-select/creature-base-select.component';
import {OverlayPanelModule} from 'primeng/overlaypanel';
import {TooltipModule} from 'primeng/tooltip';


@NgModule({
  declarations: [InventoryArmorSelectComponent, BaseInventorySelectComponent, InventoryWeaponSelectComponent, InventoryGlovesSelectComponent, InventoryBeltSelectComponent, InventoryHeadpieceSelectComponent, CreatureResistancesComponent, InventoryNeckaccesorySelectComponent, InventoryRingSelectComponent, AttackDetailsComponent, CombatCreatureSelectComponent, CreatureBaseSelectComponent],
    exports: [
        InventoryArmorSelectComponent, InventoryWeaponSelectComponent, InventoryGlovesSelectComponent, InventoryGlovesSelectComponent, InventoryBeltSelectComponent, InventoryBeltSelectComponent, InventoryHeadpieceSelectComponent, CreatureResistancesComponent, InventoryNeckaccesorySelectComponent, InventoryRingSelectComponent, AttackDetailsComponent, CombatCreatureSelectComponent, CreatureBaseSelectComponent
    ],
  imports: [
    CommonModule,
    FieldsetModule,
    AutoCompleteModule,
    ReactiveFormsModule,
    PanelModule,
    FlexLayoutModule,
    DropdownModule,
    FormsModule,
    OverlayPanelModule,
    TooltipModule

  ]
})
export class CreaturesSharedModule { }
