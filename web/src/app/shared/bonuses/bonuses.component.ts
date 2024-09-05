import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {DataService} from '../data.service';
import {IPropertyPickerInput, IPropertyPickerOutput, PropertyPickerComponent} from '../property-picker/property-picker.component';
import {FormArray, FormBuilder, FormGroup} from '@angular/forms';
import {Bonus} from '../models/Bonus.model';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'rr-bonuses',
  templateUrl: './bonuses.component.html',
  styleUrls: ['./bonuses.component.css']
})
export class BonusesComponent implements OnInit {

  @Input() form: FormGroup;
  @Output() bonusChanged = new EventEmitter<string>();
  bonuses: FormArray;
  constructor(
    private _dataService: DataService,
    private dialog: MatDialog,
    private fb: FormBuilder
  ) { }

  ngOnInit() {
    this.bonuses = this.form.get('bonuses') as FormArray;
    this.bonuses.controls.forEach(b => b.valueChanges.subscribe(() => this.bonusChanged.emit(b.value.property)));
  }

  addBonuses() {
    const dialogRef = this.dialog.open(PropertyPickerComponent, {
      data: <IPropertyPickerInput> {
        getAll: true,
        maxAttributesSelected: 4,
        maxSkillsSelected: 4,
        maxPropertiesSelected: 4,
        selectedBonuses: this.bonuses.controls.map(control => control.value.property)
      }
    });
    dialogRef.afterClosed().subscribe((data: IPropertyPickerOutput) => {
      if (!data) {
        return;
      }
      Object.values(data).forEach((set: string[]) => {
        set.forEach(p => this.bonuses.push(this.fb.group(new Bonus(p))));
      });
      this.bonuses.controls.forEach(b => b.valueChanges.subscribe(() => this.bonusChanged.emit(b.value.property)));
    });
  }
  removeBonus(index: number) {
    const property = this.bonuses.at(index).value.property;
    this.bonuses.removeAt(index);
    this.bonusChanged.emit(property);
  }

  selectBonuses() {
    const dialogRef = this.dialog.open(PropertyPickerComponent);
    dialogRef.afterClosed().subscribe();
  }

}
