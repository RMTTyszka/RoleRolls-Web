import {Component, ElementRef, Inject, Input, OnInit} from '@angular/core';
import {DataService} from '../data.service';
import {MAT_DIALOG_DATA, MatDialog, MatDialogRef} from '@angular/material/dialog';

export interface IPropertyPickerInput {
  getAttributes: boolean;
  getSkills: boolean;
  getProperties: boolean;
  getAll: boolean;
  maxAttributesSelected: number;
  maxSkillsSelected: number;
  maxPropertiesSelected: number;
  selectedBonuses: string[];
}
export interface IPropertyPickerOutput {
  attributes: string[];
  skills: string[];
  properties: string[];
}

@Component({
  selector: 'loh-property-picker',
  templateUrl: './property-picker.component.html',
  styleUrls: ['./property-picker.component.css']
})
export class PropertyPickerComponent implements OnInit {

  @Input() maxAttrsSelected = Infinity;
  @Input() maxSkillsSelected = Infinity;
  @Input() maxPropertiesSelected = Infinity;

  @Input() attributesAvailable = true;
  @Input() skillsAvailable = true;
  @Input() propertiesAvailable = true;

  attrsSelected: string[] = [];
  skillsSelected: string[] = [];
  propertiesSelected: string[] = [];
  constructor(
    public service: DataService,
    public dialogRef: MatDialogRef<PropertyPickerComponent>,
    public dialog: MatDialog,
    @Inject(MAT_DIALOG_DATA) data: IPropertyPickerInput
  ) {
    console.log(data);
    if (data) {
      if (data.getAll) {
        this.attributesAvailable = true;
        this.skillsAvailable = true;
        this.propertiesAvailable = true;
      } else {
        this.attributesAvailable = data.getAttributes || false;
        this.skillsAvailable = data.getSkills || false;
        this.propertiesAvailable = data.getProperties || false;
      }
      this.maxAttrsSelected = data.maxAttributesSelected || Infinity;
      this.maxSkillsSelected = data.maxSkillsSelected || Infinity;
      this.maxPropertiesSelected = data.maxPropertiesSelected || Infinity;
      data.selectedBonuses.forEach(prop => {
        if (this.attributes.indexOf(prop) >= 0) {this.attrsSelected.push(prop); }
        if (this.skills.indexOf(prop) >= 0) {this.skillsSelected.push(prop); }
      });
    }
   }

  ngOnInit() {
  }

  get attributes() {
    return this.service.attributes;
  }
  get skills() {
    return this.service.skills;
  }

  buttonToggled(ele: ElementRef) {
  }

  save() {
    this.dialogRef.close({
      attributes: this.attrsSelected,
      skills: this.skillsSelected,
      properties: this.propertiesSelected
    });
  }
  cancel() {
    this.dialogRef.close();
  }

}
