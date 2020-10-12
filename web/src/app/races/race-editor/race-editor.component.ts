import {Component, Inject, Injector, OnInit} from '@angular/core';
import {LegacyBaseCreatorComponent} from '../../shared/base-creator/legacy-base-creator.component';
import {Race} from '../../shared/models/Race.model';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {Bonus} from 'src/app/shared/models/Bonus.model';
import {FormArray} from '@angular/forms';
import {
  IPropertyPickerInput,
  IPropertyPickerOutput,
  PropertyPickerComponent
} from 'src/app/shared/property-picker/property-picker.component';
import {RaceService} from './race.service';
import {PowerSelectorComponent} from 'src/app/powers/powers-shared/power-selector/power-selector.component';
import {DialogService, DynamicDialogConfig, DynamicDialogRef} from 'primeng/dynamicdialog';
import {BaseCreatorComponent} from '../../shared/base-creator/base-creator.component';

@Component({
  selector: 'loh-race-editor',
  templateUrl: './race-editor.component.html',
  styleUrls: ['./race-editor.component.css'],
  providers: [DialogService]
})
export class RaceEditorComponent extends BaseCreatorComponent<Race, Race> implements OnInit {

  powers: FormArray;
  constructor(
    injector: Injector,
    protected dialogRef: DynamicDialogRef,
    protected dialogConfig: DynamicDialogConfig,
    protected service: RaceService
  ) {
    super(injector);
    if (dialogConfig.data) {
      this.getEntity(dialogConfig.data.entityId);
    } else {
      this.getEntity();
    }
   }

  ngOnInit() {
  }

  protected afterGetEntity() {
    super.afterGetEntity();
    this.powers = <FormArray>this.form.get('powers');
  }

  addBonuses() {
    this.dialog.open(PropertyPickerComponent, {
      data: <IPropertyPickerInput> {
        getAll: true,
        maxAttributesSelected: 4,
        maxSkillsSelected: 4,
        maxPropertiesSelected: 4,
        selectedBonuses: this.myBonuses.controls.map(control => control.value.property)
      }
    }).onClose.subscribe((data: IPropertyPickerOutput) => {
      if (!data) {
        return;
      }
      Object.values(data).forEach((set: string[]) => {
        set.forEach(p => {
          if (this.myBonuses.controls.findIndex(c => c.value.property === p) < 0) {
            this.myBonuses.push(this.fb.group(new Bonus(p)));
          }
        });
      });
    });
  }
  removeBonus(index: number) {
    this.myBonuses.removeAt(index);
  }

  selectBonuses() {
    this.dialog.open(PropertyPickerComponent, {}).onClose.subscribe();
  }
  addPower() {
    this.dialog.open(PowerSelectorComponent, {}).onClose.subscribe(power => {
      if (power) {
        this.powers.push(this.fb.group(power));
      }
    });
  }
  removePower(index: number) {
    this.powers.removeAt(index);
  }
  get myBonuses() {
    return <FormArray>this.form.get('bonuses');
  }

}
