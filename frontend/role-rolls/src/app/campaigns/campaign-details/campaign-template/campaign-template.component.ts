import { Component, EventEmitter, Input, Output } from '@angular/core';
import { createForm, getAsForm } from '../../../tokens/EditorExtension';
import { FormArray, FormBuilder, FormControl, FormGroup, ReactiveFormsModule, UntypedFormGroup } from '@angular/forms';
import { Campaign } from '../../models/campaign';
import {
  AttributeTemplateModel,
  DefenseTemplateModel, LifeTemplateModel,
  MinorSkillsTemplateModel, SkillTemplateModel
} from '../../models/creature-template.model';
import { v4 as uuidv4 } from 'uuid';
import { EditorAction } from '../../../models/ModalEntityData';
import { RROption } from '../../../models/RROption';
import { AuthenticationService } from '../../../authentication/services/authentication.service';
import { PocketCampaignsService } from '../../services/pocket-campaigns.service';
import { Entity } from '../../../models/Entity.model';
import { Fieldset } from 'primeng/fieldset';
import { Select } from 'primeng/select';
import { Checkbox } from 'primeng/checkbox';
import { Panel } from 'primeng/panel';
import { NgForOf, NgIf } from '@angular/common';
import { ButtonDirective } from 'primeng/button';
import { InputText } from 'primeng/inputtext';

@Component({
  selector: 'rr-campaign-template',
  standalone: true,
  templateUrl: './campaign-template.component.html',
  imports: [
    ReactiveFormsModule,
    Fieldset,
    Select,
    Checkbox,
    Panel,
    NgIf,
    ButtonDirective,
    InputText,
    NgForOf
  ],
  styleUrl: './campaign-template.component.scss'
})
export class CampaignTemplateComponent {
  @Input() public action = EditorAction.create;
  public actionEnum = EditorAction;
  public form = new UntypedFormGroup({});
  public attributeForm = new FormGroup({});
  public skillForm = new FormGroup({});
  public minorSkillForm = new FormGroup({});
  public defenseForm = new FormGroup({});
  public lifeForm = new FormGroup({});
  public isLoading = true;
  public entity!: Campaign;
  @Input() public entityId!: string;
  public requiredFields = ['name'];
  public skillsMapping = new Map<string, FormArray>();
  public minorsSkillBySkill = new Map<string, FormArray<FormGroup>>();
  public selectedSkillMinorSkills: FormGroup[] = [];
  public disabled: boolean = false;
  @Output() campaignLoaded = new EventEmitter<Campaign>();
  public defaultTemplates: RROption<string>[] = [];
  public get default() {
    return this.entity.creatureTemplate.default;
  }
  public get attributes(): FormArray<FormGroup> {
    return this.form?.get('creatureTemplate.attributes') as FormArray<FormGroup>;
  }

  public get lifes(): FormArray<FormGroup> {
    return this.form?.get('creatureTemplate.lifes') as FormArray<FormGroup>;
  }
  public get skills(): FormArray<FormGroup> {
    return this.form?.get('creatureTemplate.skills') as FormArray<FormGroup>;
  }
  public get defenses(): FormArray<FormGroup> {
    return this.form?.get('creatureTemplate.defenses') as FormArray<FormGroup>;
  }
  public attributeSkills(attributeId: string): FormArray<FormGroup> {
    return this.skillsMapping.get(attributeId);
  }
  public get isUpdate() {
    return this.action === EditorAction.update;
  }

  constructor(
    public service: PocketCampaignsService,
    public formBuilder: FormBuilder,
    public authService: AuthenticationService,
  ) {
    this.defaultTemplates = [
      {
        value: '985C54E0-C742-49BC-A3E0-8DD2D6CE2632',
        label: 'Land of Heroes',
      } as RROption<string>,
      {
        value: '',
        label: 'None',
      } as RROption<string>,
    ] ;
  }
  ngOnInit(): void {
  }


  public addAttribute() {
    const attribute = this.attributeForm.value as AttributeTemplateModel;
    this.service.addAttribute(this.entity.id, attribute)
      .subscribe(() => {
        const formArray = this.attributes;
        const newFormGroup = new FormGroup({});
        createForm(newFormGroup, this.attributeForm.value as Entity);
        formArray.controls.push(newFormGroup);
        this.attributeForm.reset();
        this.attributeForm.get('id').setValue(uuidv4() as never);
        this.skillsMapping.set(attribute.id, new FormArray([]));
      });
  }


  public updateAttribute(attributeControl: FormGroup) {
    const attribute = attributeControl.value as AttributeTemplateModel;
    this.service.updateAttribute(this.entity.id, attribute.id, attribute)
      .subscribe();
  }


  public removeAttribute(attributeControl: FormGroup, index: number) {
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
        createForm(newFormGroup, this.skillForm.value as Entity);
        formArray.controls.push(newFormGroup);
        this.skillForm.reset();
        this.skillForm.get('id').setValue(uuidv4() as never);
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
  public removeSkill(attributeControl: FormGroup, skillControl: FormGroup, index: number) {
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
        this.minorSkillForm.get('id').setValue(uuidv4() as never);
        this.minorsSkillBySkill.get(minorSkill.skillTemplateId).controls.push(newFormGroup);
      });
  }
  public updateMinorSkill(skillControl: FormGroup, minorSkillControl: FormGroup) {
    const skill = skillControl.value as SkillTemplateModel;
    const minorSkill = minorSkillControl.value as MinorSkillsTemplateModel;
    this.service.updateMinorSkill(this.entity.id, skill.attributeId, skill.id, minorSkill.id, minorSkill)
      .subscribe(() => {
      });
  }
  public removeMinorSkill(skillControl: FormGroup, minorSkillControl: FormGroup, index: number) {
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
        createForm(newFormGroup, this.lifeForm.value as Entity);
        formArray.controls.push(newFormGroup);
        this.lifeForm.reset();
        this.lifeForm.get('id').setValue(uuidv4() as never);
      });
  }

  public updateLife(lifeControl: FormGroup) {
    const life = lifeControl.value as LifeTemplateModel;
    this.service.updateLife(this.entity.id, life.id, life)
      .subscribe();
  }


  public removeLife(lifeControl: FormGroup, index: number) {
    const life = lifeControl.value as LifeTemplateModel;
    this.service.removeLife(this.entity.id, life.id)
      .subscribe(() => {
        const formArray = this.lifes;
        formArray.removeAt(index);
      });
  }

  public addDefense() {
    const defense = this.defenseForm.value as DefenseTemplateModel;
    defense.formula = '';
    this.service.addDefense(this.entity.id, defense)
      .subscribe(() => {
        const formArray = this.defenses;
        const newFormGroup = new FormGroup({});
        createForm(newFormGroup, this.defenseForm.value as Entity);
        formArray.controls.push(newFormGroup);
        this.defenseForm.reset();
        this.defenseForm.get('id')?.setValue(uuidv4() as never);
      });
  }

  public updateDefense(defenseControl: FormGroup) {
    const defense = defenseControl.value as DefenseTemplateModel;
    this.service.updateDefense(this.entity.id, defense.id, defense)
      .subscribe();
  }


  public removeDefense(defenseControl: FormGroup, index: number) {
    const defense = defenseControl.value as DefenseTemplateModel;
    this.service.removeDefense(this.entity.id, defense.id)
      .subscribe(() => {
        const formArray = this.defenses;
        formArray.removeAt(index);
      });
  }

  loaded(entity: Campaign) {
    this.isLoading = false;
    this.entity = entity;
    this.campaignLoaded.next(this.entity);
    createForm(this.attributeForm, {
      id: null,
      name: null,
    } as AttributeTemplateModel);
    createForm(this.skillForm, {
      id: null,
      name: null,
      minorSkills: []
    } as SkillTemplateModel);
    createForm(this.minorSkillForm, {
      name: null,
      skillTemplateId: null
    } as  MinorSkillsTemplateModel);
    createForm(this.lifeForm, {
      id: null,
      name: null,
      formula: null
    } as LifeTemplateModel);
    createForm(this.defenseForm, {
      id: null,
      name: null,
      formula: null
    } as DefenseTemplateModel);

    this.attributeForm.get('id').setValue(uuidv4() as never);
    this.skillForm.get('id').setValue(uuidv4() as never);
    this.minorSkillForm.get('id').setValue(uuidv4() as never);
    this.lifeForm.get('id').setValue(uuidv4() as never);
    this.defenseForm.get('id').setValue(uuidv4() as never);

    this.buildSkills();
    this.disabled = !this.authService.isMaster(this.entity.masterId) || this.default;
    if (this.disabled) {
      this.form.disable();
      this.attributeForm.disable();
      this.skillForm.disable();
      this.minorSkillForm.disable();
      this.lifeForm.disable();
    }
  }
  private buildSkills() {
    this.entity.creatureTemplate.attributes.forEach(attribute => {
      this.skillsMapping.set(attribute.id, new FormArray<any>([]));
    });
    this.entity.creatureTemplate.skills.forEach(skill => {
      this.skillsMapping.get(skill.attributeId).push(getAsForm(skill));
      this.minorsSkillBySkill.set(skill.id, new FormArray<any>([]));
      const minorSkills = this.minorsSkillBySkill.get(skill.id);
      skill.minorSkills.forEach(minorSkill => {
        minorSkills.push(getAsForm(minorSkill));
      });
    });
  }

}

