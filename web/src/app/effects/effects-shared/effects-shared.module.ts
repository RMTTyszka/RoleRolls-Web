import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EffectIconComponent } from './effect-icon/effect-icon.component';



@NgModule({
    declarations: [EffectIconComponent],
    exports: [
        EffectIconComponent
    ],
    imports: [
        CommonModule
    ]
})
export class EffectsSharedModule { }
