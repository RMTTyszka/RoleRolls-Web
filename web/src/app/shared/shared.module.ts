import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {MatTableModule} from '@angular/material/table';
import {MatPaginatorModule} from '@angular/material/paginator';
import {MatSortModule} from '@angular/material/sort';
import {MatChipsModule} from '@angular/material/chips';
import {FlexLayoutModule} from '@angular/flex-layout';
import {MatCardModule} from '@angular/material/card';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MatInputModule} from '@angular/material/input';
import {MatDialogModule} from '@angular/material/dialog';
import {MatTooltipModule} from '@angular/material/tooltip';
import {MatTabsModule} from '@angular/material/tabs';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonToggleModule} from '@angular/material/button-toggle';
import {PropertyPickerComponent} from './property-picker/property-picker.component';
import {MatBadgeModule} from '@angular/material/badge';
import {MatButtonModule} from '@angular/material/button';
import {BonusesComponent} from './bonuses/bonuses.component';
import {MatListModule} from '@angular/material/list';
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

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        FlexLayoutModule,
        MatTableModule,
        MatPaginatorModule,
        MatSortModule,
        MatCardModule,
        MatInputModule,
        MatDialogModule,
        MatTooltipModule,
        MatTabsModule,
        MatIconModule,
        MatChipsModule,
        MatButtonToggleModule,
        MatBadgeModule,
        MatButtonModule,
        ClipboardModule,
        // PrimeNg
        TableModule,
        ToastModule,
        DropdownModule,
        ButtonModule,
        CascadeSelectModule,
        DialogModule,
        InputNumberModule,
        PanelModule,
        TooltipModule
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
        FlexLayoutModule,
        MatTableModule,
        MatPaginatorModule,
        MatSortModule,
        MatCardModule,
        MatInputModule,
        MatDialogModule,
        MatTooltipModule,
        MatTabsModule,
        MatIconModule,
        MatChipsModule,
        MatButtonToggleModule,
        MatBadgeModule,
        MatButtonModule,
        MatListModule,
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
    ],
    providers: []
})
export class SharedModule {
 }
