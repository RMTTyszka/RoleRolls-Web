import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { EditorAction } from 'src/app/shared/dtos/ModalEntityData';
import { createForm, getAsForm } from 'src/app/shared/EditorExtension';
import { PocketCampaignModel } from 'src/app/shared/models/pocket/campaigns/pocket.campaign.model';
import { AttributeTemplateModel, DefenseTemplateModel, LifeTemplateModel, MinorSkillsTemplateModel, SkillTemplateModel } from 'src/app/shared/models/pocket/creature-templates/creature-template.model';
import { PocketCampaignsService } from '../pocket-campaigns.service';
import { v4 as uuidv4 } from 'uuid';
import { AuthenticationService } from 'src/app/authentication/authentication.service';

@Component({
  selector: 'rr-campaign-creator',
  templateUrl: './campaign-creator.component.html',
  styleUrls: ['./campaign-creator.component.scss']
})
export class CampaignCreatorComponent implements OnInit {
  public action = EditorAction.create;
  public actionEnum = EditorAction;
  public form = new FormGroup({});
  public attributeForm = new FormGroup({});
  public skillForm = new FormGroup({});
  public minorSkillForm = new FormGroup({});
  public defenseForm = new FormGroup({});
  public lifeForm = new FormGroup({});
  public isLoading = true;
  public entity: PocketCampaignModel;
  public entityId: string;
  public requiredFields = ['name'];
  public skillsMapping = new Map<string, FormArray>();
  public minorsSkillBySkill = new Map<string, FormArray>();
  public selectedSkillMinorSkills: FormGroup[] = [];
  public disabled: boolean;

  public get attributes(): FormArray {
    return this.form?.get('creatureTemplate.attributes') as FormArray;
  }

  public get lifes(): FormArray {
    return this.form?.get('creatureTemplate.lifes') as FormArray;
  }
  public get skills(): FormArray {
    return this.form?.get('creatureTemplate.skills') as FormArray;
  }
  public get defenses(): FormArray {
    return this.form?.get('creatureTemplate.defenses') as FormArray;
  }
  public attributeSkills(attributeId: string): FormArray {
    return this.skillsMapping.get(attributeId);
  }
  public get isUpdate() {
    return this.action === EditorAction.update
  }

  constructor(
    public service: PocketCampaignsService,
    public ref: DynamicDialogRef,
    public formBuilder: FormBuilder,
    public config: DynamicDialogConfig,
    public authService: AuthenticationService,
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
      this.skillsMapping.set(attribute.id, new FormArray([]));
    });
  }


  public updateAttribute(attributeControl: FormGroup) {
    const attribute = attributeControl.value as AttributeTemplateModel;
    this.service.updateAttribute(this.entity.id, attribute.id, attribute)
    .subscribe();
  }


  public removeAttribute(attributeControl: FormControl, index: number) {
    const attribute = attributeControl.value as AttributeTemplateModel;
    this.service.removeAttribute(this.entity.id, attribute.id)
    .subscribe(() => {
      const formArray = this.attributes;
      formArray.removeAt(index);
      this.skillsMapping.delete(attribute.id);
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
      const newSkillForm = new FormGroup({});
      createForm(newSkillForm, skill);
      this.skillsMapping.get(attribute.id).push(newSkillForm);
      this.minorsSkillBySkill.set(skill.id, new FormArray([]));
    });
  }
  public updateSkill(attributeControl: FormGroup, skillControl: FormGroup) {
    const attribute = attributeControl.value as AttributeTemplateModel;
    const skill = skillControl.value as SkillTemplateModel;
   // skill.attributeId = attribute.id;
    this.service.updateSkill(this.entity.id, attribute.id, skill.id, skill)
    .subscribe();
  }
  public removeSkill(attributeControl: FormGroup, skillControl: FormControl, index: number) {
    const skill = skillControl.value as SkillTemplateModel;
    const attribute = attributeControl.value as AttributeTemplateModel;
    this.service.removeSkill(this.entity.id, attribute.id, skill.id)
    .subscribe(() => {
      const formArray = this.skills;
      formArray.removeAt(index);
      this.skillsMapping.get(skill.attributeId).removeAt(index);
    });
  }

  public addMinorSkill(skillForm: FormGroup) {
    const minorSkill = this.minorSkillForm.value as MinorSkillsTemplateModel;
    const skill = skillForm.value as SkillTemplateModel;
    minorSkill.skillTemplateId = skill.id;
    this.service.addMinorSkill(this.entity.id, skill.attributeId, skill.id, minorSkill)
    .subscribe(() => {
      const newFormGroup = new FormGroup({});
      createForm(newFormGroup, minorSkill);
      this.minorSkillForm.reset();
      this.minorSkillForm.get('id').setValue(uuidv4());
      this.minorsSkillBySkill.get(minorSkill.skillTemplateId).controls.push(newFormGroup);
    });
  }
    public updateMinorSkill(skillControl: FormControl, minorSkillControl: FormControl) {
      const skill = skillControl.value as SkillTemplateModel;
      const minorSkill = minorSkillControl.value as MinorSkillsTemplateModel;
      this.service.updateMinorSkill(this.entity.id, skill.attributeId, skill.id, minorSkill.id, minorSkill)
      .subscribe(() => {
      });
  }
  public removeMinorSkill(skillControl: FormControl, minorSkillControl: FormControl, index: number) {
    const skill = skillControl.value as SkillTemplateModel;
    const minorSkill = minorSkillControl.value as MinorSkillsTemplateModel;
    this.service.removeMinorSkill(this.entity.id, skill.attributeId, skill.id, minorSkill.id)
    .subscribe(() => {
      this.minorsSkillBySkill.get(skill.id).removeAt(index);
    });
  }

  public addLife() {
    const life = this.lifeForm.value as LifeTemplateModel;
    life.formula = '';
    this.service.addLife(this.entity.id, life)
    .subscribe(() => {
      const formArray = this.lifes;
      const newFormGroup = new FormGroup({});
      createForm(newFormGroup, this.lifeForm.value);
      formArray.controls.push(newFormGroup);
      this.lifeForm.reset();
      this.lifeForm.get('id').setValue(uuidv4());
    });
  }

  public updateLife(lifeControl: FormGroup) {
    const life = lifeControl.value as LifeTemplateModel;
    this.service.updateLife(this.entity.id, life.id, life)
    .subscribe();
  }


  public removeLife(lifeControl: FormControl, index: number) {
    const life = lifeControl.value as LifeTemplateModel;
    this.service.removeLife(this.entity.id, life.id)
    .subscribe(() => {
      const formArray = this.lifes;
      formArray.removeAt(index);
    });
  }

  public addDefense() {
    const defense = this.lifeForm.value as DefenseTemplateModel;
    defense.formula = '';
    this.service.addDefense(this.entity.id, defense)
    .subscribe(() => {
      const formArray = this.defenses;
      const newFormGroup = new FormGroup({});
      createForm(newFormGroup, this.defenseForm.value);
      formArray.controls.push(newFormGroup);
      this.defenseForm.reset();
      this.defenseForm.get('id').setValue(uuidv4());
    });
  }

  public updateDefense(defenseControl: FormGroup) {
    const defense = defenseControl.value as DefenseTemplateModel;
    this.service.updateDefense(this.entity.id, defense.id, defense)
    .subscribe();
  }


  public removeDefense(defenseControl: FormControl, index: number) {
    const defense = defenseControl.value as LifeTemplateModel;
    this.service.removeDefense(this.entity.id, defense.id)
    .subscribe(() => {
      const formArray = this.defenses;
      formArray.removeAt(index);
    });
  }

  loaded(entity: PocketCampaignModel) {
    this.isLoading = false;
    this.entity = entity;
    createForm(this.attributeForm, new AttributeTemplateModel());
    createForm(this.skillForm, new SkillTemplateModel());
    createForm(this.minorSkillForm, new MinorSkillsTemplateModel());
    createForm(this.lifeForm, new LifeTemplateModel());
    createForm(this.defenseForm, new DefenseTemplateModel());

    this.attributeForm.get('id').setValue(uuidv4());
    this.skillForm.get('id').setValue(uuidv4());
    this.minorSkillForm.get('id').setValue(uuidv4());
    this.lifeForm.get('id').setValue(uuidv4());
    this.buildSkills();
    this.disabled = !this.authService.isMaster(this.entity.masterId);
    if (this.disabled) {
      this.form.disable();
      this.attributeForm.disable();
      this.skillForm.disable();
      this.minorSkillForm.disable();
      this.lifeForm.disable();
    }
  }

  deleted() {
    this.ref.close(true);
  }

  saved() {
    this.ref.close(true);
  }
  private buildSkills() {
    this.entity.creatureTemplate.attributes.forEach(attribute => {
      this.skillsMapping.set(attribute.id, new FormArray([]));
    });
    this.entity.creatureTemplate.skills.forEach(skill => {
      this.skillsMapping.get(skill.attributeId).push(getAsForm(skill));
      this.minorsSkillBySkill.set(skill.id, new FormArray([]));
      const minorSkills = this.minorsSkillBySkill.get(skill.id);
      skill.minorSkills.forEach(minorSkill => {
        minorSkills.push(getAsForm(minorSkill));
      });
    });
  }

}

