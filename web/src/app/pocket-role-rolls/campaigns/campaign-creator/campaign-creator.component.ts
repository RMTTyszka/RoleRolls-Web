import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { EditorAction } from 'src/app/shared/dtos/ModalEntityData';
import { createForm } from 'src/app/shared/EditorExtension';
import { PocketCampaignModel } from 'src/app/shared/models/pocket/campaigns/pocket.campaign.model';
import { AttributeTemplateModel, SkillTemplateModel } from 'src/app/shared/models/pocket/creature-templates/creature-template.model';
import { PocketCampaignsService } from '../pocket-campaigns.service';
import { v4 as uuidv4 } from 'uuid';

@Component({
  selector: 'rr-campaign-creator',
  templateUrl: './campaign-creator.component.html',
  styleUrls: ['./campaign-creator.component.scss']
})
export class CampaignCreatorComponent implements OnInit {
  public action = EditorAction.create;
  public form = new FormGroup({});
  public attributeForm = new FormGroup({});
  public skillForm = new FormGroup({});
  public isLoading = true;
  public entity: PocketCampaignModel;
  public entityId: string;
  public requiredFields = ['name'];
  public skillsMapping = new Map<string, FormArray>();

  public get attributes(): FormArray {
    return this.form?.get('creatureTemplate.attributes') as FormArray;
  }
  public get skills(): FormArray {
    return this.form?.get('creatureTemplate.skills') as FormArray;
  }
  public attributeSkills(attributeId: string): FormArray {
    return this.skillsMapping.get(attributeId);
  }

  constructor(
    public service: PocketCampaignsService,
    public ref: DynamicDialogRef,
    public formBuilder: FormBuilder,
    public config: DynamicDialogConfig,
  ) {
    this.action = config.data.action;
    this.entityId = config.data.entityId;
   }

  ngOnInit(): void {
  }


  public addAttribute() {
    const attribute = this.attributeForm.value as AttributeTemplateModel;
    this.service.addAttribute(this.entity.id, attribute)
    .subscribe(() => {
      const formArray = this.attributes;
      const newFormGroup = new FormGroup({});
      createForm(newFormGroup, this.attributeForm.value);
      formArray.controls.push(newFormGroup);
      this.attributeForm.reset();
      this.attributeForm.get('id').setValue(uuidv4());
    });
  }

  public addSkill(attributeForm: FormGroup) {
    const attribute = attributeForm.value as AttributeTemplateModel;
    const skill = this.skillForm.value as SkillTemplateModel;
    skill.attributeId = attribute.id;
    this.service.addSkill(this.entity.id, attribute.id, skill)
    .subscribe(() => {
      const formArray = this.skills;
      const newFormGroup = new FormGroup({});
      createForm(newFormGroup, this.skillForm.value);
      formArray.controls.push(newFormGroup);
      this.skillForm.reset();
      this.skillForm.get('id').setValue(uuidv4());
    });
  }

  public updateAttribute(attributeControl: FormGroup) {
    const attribute = attributeControl.value as AttributeTemplateModel;
    this.service.updateAttribute(this.entity.id, attribute.id, attribute)
    .subscribe();
  }

  public updateSkill(attributeControl: FormGroup, skillControl: FormGroup) {
    const attribute = attributeControl.value as AttributeTemplateModel;
    const skill = skillControl.value as SkillTemplateModel;
   // skill.attributeId = attribute.id;
    this.service.updateSkill(this.entity.id, attribute.id, skill.id, skill)
    .subscribe();
  }
  public removeAttribute(attributeControl: FormControl, index: number) {
    const attribute = attributeControl.value as AttributeTemplateModel;
    this.service.removeAttribute(this.entity.id, attribute.id)
    .subscribe(() => {
      const formArray = this.attributes;
      formArray.removeAt(index);
    });
  }
  public removeSkill(attributeControl: FormGroup, skillControl: FormControl, index: number) {
    const skill = skillControl.value as SkillTemplateModel;
    const attribute = attributeControl.value as AttributeTemplateModel;
    this.service.removeSkill(this.entity.id, attribute.id, skill.id)
    .subscribe(() => {
      const formArray = this.skills;
      formArray.removeAt(index);
    });
  }
  loaded(entity: PocketCampaignModel) {
    this.isLoading = false;
    this.entity = entity;
    createForm(this.attributeForm, new AttributeTemplateModel());
    createForm(this.skillForm, new SkillTemplateModel());
    this.attributeForm.get('id').setValue(uuidv4());
    this.skillForm.get('id').setValue(uuidv4());
    this.buildSkills();
  }

  deleted() {
    this.ref.close(true);
  }

  saved() {
    this.ref.close(true);
  }
  private buildSkills() {
    const skillByAttributeArray = this.form.get('creatureTemplate.skillsByAttribute') as FormGroup;
    Object.entries(skillByAttributeArray.controls).forEach((skillByAttribute: [string, FormArray]) => {
      this.skillsMapping.set(skillByAttribute[0], skillByAttribute[1]);
    });
  }

}

