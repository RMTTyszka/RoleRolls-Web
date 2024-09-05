import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {MatLegacyTableModule as MatTableModule} from '@angular/material/legacy-table';
import {MatLegacyPaginatorModule as MatPaginatorModule} from '@angular/material/legacy-paginator';
import {MatSortModule} from '@angular/material/sort';
import {MatLegacyChipsModule as MatChipsModule} from '@angular/material/legacy-chips';
import {FlexLayoutModule} from '@angular/flex-layout';
import {MatLegacyCardModule as MatCardModule} from '@angular/material/legacy-card';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MatLegacyInputModule as MatInputModule} from '@angular/material/legacy-input';
import {MatLegacyDialogModule as MatDialogModule} from '@angular/material/legacy-dialog';
import {MatLegacyTooltipModule as MatTooltipModule} from '@angular/material/legacy-tooltip';
import {MatLegacyTabsModule as MatTabsModule} from '@angular/material/legacy-tabs';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonToggleModule} from '@angular/material/button-toggle';
import {PropertyPickerComponent} from './property-picker/property-picker.component';
import {MatBadgeModule} from '@angular/material/badge';
import {MatLegacyButtonModule as MatButtonModule} from '@angular/material/legacy-button';
import {BonusesComponent} from './bonuses/bonuses.component';
import {MatLegacyListModule as MatListModule} from '@angular/material/legacy-list';
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
