import {Component, ElementRef, Inject, Input, OnInit} from '@angular/core';
import {DataService} from '../data.service';
import {DynamicDialogConfig, DynamicDialogRef} from 'primeng/dynamicdialog';

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
  selector: 'rr-property-picker',
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
    public dialogRef: DynamicDialogRef,
    public config: DynamicDialogConfig,
  ) {
    console.log(config);
    if (config) {
      if (config.data.getAll) {
        this.attributesAvailable = true;
        this.skillsAvailable = true;
        this.propertiesAvailable = true;
      } else {
        this.attributesAvailable = config.data.getAttributes || false;
        this.skillsAvailable = config.data.getSkills || false;
        this.propertiesAvailable = config.data.getProperties || false;
      }
      this.maxAttrsSelected = config.data.maxAttributesSelected || Infinity;
      this.maxSkillsSelected = config.data.maxSkillsSelected || Infinity;
      this.maxPropertiesSelected = config.data.maxPropertiesSelected || Infinity;
      config.data.selectedBonuses.forEach(prop => {
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
