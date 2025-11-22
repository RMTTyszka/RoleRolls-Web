import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {MatSortModule} from '@angular/material/sort';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MatIconModule} from '@angular/material/icon';
import {PropertyPickerComponent} from './property-picker/property-picker.component';
import {MatBadgeModule} from '@angular/material/badge';
import {BonusesComponent} from './bonuses/bonuses.component';
import {EditorModalComponent} from './UI/editor-modal/editor-modal.component';
import {AccordionModule} from 'primeng/accordion';
import {ButtonModule} from 'primeng/button';
import {ClickStopPropagationDirective} from './directives/click-stop-propagation.directive';
import {CmGridComponent} from './components/cm-grid/cm-grid.component';
import {TableModule} from 'primeng/table';
import {CmEditorComponent} from './components/cm-editor/cm-editor.component';
import {ToastModule} from 'primeng/toast';
import {AttributeSelectComponent} from './attribute-select/attribute-select.component';
import {DropdownModule} from 'primeng/dropdown';
import {RRGridComponent} from './components/rr-grid/r-r-grid.component';
import { RrSelectFieldComponent } from './components/rr-select-field/rr-select-field.component';
import { RrSelectModalComponent } from './components/rr-select-modal/rr-select-modal.component';
import { PanelModule } from 'primeng/panel';
import { TooltipModule } from 'primeng/tooltip';
import { CascadeSelectModule } from 'primeng/cascadeselect';
import { OverlayPanelModule } from 'primeng/overlaypanel';
import {ClipboardModule} from '@angular/cdk/clipboard';
import { DialogModule } from 'primeng/dialog';
import { InputNumberModule } from 'primeng/inputnumber';
import {MatButtonToggleModule} from "@angular/material/button-toggle";
import {MatTab, MatTabGroup} from "@angular/material/tabs";
import { InputGroupAddonModule } from 'primeng/inputgroupaddon';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatSortModule,
    MatIconModule,
    MatButtonToggleModule,
    MatBadgeModule,
    ClipboardModule,
    // PrimeNg
    TableModule,
    ToastModule,
    DropdownModule,
    ButtonModule,
    CascadeSelectModule,
    DialogModule,
    InputNumberModule,
    InputGroupAddonModule,
    PanelModule,
    TooltipModule,
    MatTabGroup,
    MatTab
  ],
    declarations: [
        PropertyPickerComponent,
        BonusesComponent,
        EditorModalComponent,
        ClickStopPropagationDirective,
        CmGridComponent,
        RRGridComponent,
        CmEditorComponent,
        AttributeSelectComponent,
        RrSelectFieldComponent,
        RrSelectModalComponent,
    ],
    exports: [
        FormsModule,
        ReactiveFormsModule,
        MatSortModule,
        MatIconModule,
        MatButtonToggleModule,
        MatBadgeModule,
        AccordionModule,
        ButtonModule,
        ClipboardModule,
        // PrimeNg
        TooltipModule,
        TableModule,
        ToastModule,
        DropdownModule,
        BonusesComponent,
        EditorModalComponent,
        PropertyPickerComponent,
        ClickStopPropagationDirective,
        CmGridComponent,
        CmEditorComponent,
        AttributeSelectComponent,
        RRGridComponent,
        RrSelectFieldComponent,
        PanelModule,
        CascadeSelectModule,
        OverlayPanelModule,
        DialogModule,
        InputNumberModule,
        InputGroupAddonModule,
    ],
    providers: []
})
export class SharedModule {
 }
