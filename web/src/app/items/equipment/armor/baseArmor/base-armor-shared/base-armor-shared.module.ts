import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {BaseArmorSelectorComponent} from './base-armor-selector/base-armor-selector.component';
import {DropdownModule} from 'primeng/dropdown';
import {ReactiveFormsModule} from '@angular/forms';
import {AutoCompleteModule} from 'primeng/autocomplete';

@NgModule({
    imports: [
        CommonModule,
        DropdownModule,
        ReactiveFormsModule,
        AutoCompleteModule
    ],
    declarations: [BaseArmorSelectorComponent],
    exports: [BaseArmorSelectorComponent]
})
export class BaseArmorSharedModule { }
