import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {PlayerSelectModalComponent} from './player-select-modal/player-select-modal.component';
import {TableModule} from 'primeng/table';



@NgModule({
    declarations: [PlayerSelectModalComponent],
    imports: [
        CommonModule,
        TableModule
    ],
    exports: [PlayerSelectModalComponent]
})
export class PlayerSharedModule { }
