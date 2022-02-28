import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
// import {RolesSelectModalComponent} from './roles-select-modal/roles-select-modal.component';
import {SharedModule} from 'src/app/shared/shared.module';
import {RoleSelectComponent} from './role-select/role-select.component';
import {AutoCompleteModule} from 'primeng/autocomplete';

@NgModule({
    imports: [
        CommonModule,
        SharedModule,
        AutoCompleteModule
    ],
    exports: [
        RoleSelectComponent
    ],
    declarations: [RoleSelectComponent]
})
export class RolesSharedModule { }
