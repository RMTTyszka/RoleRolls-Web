import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {EquipmentRoutingModule} from './equipment-routing.module';
import {EquipmentComponent} from './equipment.component';
import {TableModule} from 'primeng/table';
import {ReactiveFormsModule} from '@angular/forms';
import {DropdownModule} from 'primeng/dropdown';
import {BaseArmorModule} from './armor/baseArmor/base-armor.module';
import {ArmorModule} from './armor/armors/armor.module';
import {WeaponsModule} from './weapons/weapons.module';
import { SelectModule } from 'primeng/select';

@NgModule({
  imports: [
    CommonModule,
    EquipmentRoutingModule,
    TableModule,
    ReactiveFormsModule,
    SelectModule,
    BaseArmorModule,
    ArmorModule,
    WeaponsModule
  ],
  exports: [
  ],
  declarations: [EquipmentComponent]
})
export class EquipmentModule { }
