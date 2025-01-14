import { Component, EventEmitter, Input, Output } from '@angular/core';
import { createForm, getAsForm } from '../../../tokens/EditorExtension';
import { FormArray, FormBuilder, FormControl, FormGroup, ReactiveFormsModule, UntypedFormGroup } from '@angular/forms';
import { Campaign } from '../../models/campaign';
import { v4 as uuidv4 } from 'uuid';
import { RROption } from '../../../models/RROption';
import { AuthenticationService } from '../../../authentication/services/authentication.service';
import { CampaignsService } from '../../services/campaigns.service';
import { Entity } from '../../../models/Entity.model';
import { Fieldset } from 'primeng/fieldset';
import { Panel } from 'primeng/panel';
import { NgForOf, NgIf } from '@angular/common';
import { ButtonDirective } from 'primeng/button';
import { InputText } from 'primeng/inputtext';
import {ActivatedRoute} from '@angular/router';
import {AttributeTemplateModel, DefenseTemplateModel, LifeTemplateModel, MinorSkillsTemplateModel, SkillTemplateModel} from '../../models/campaign-template.model';
import {LoggerService} from '@services/logger/logger.service';
import {firstValueFrom} from 'rxjs';
import { PropertySelectorComponent } from '@app/components/property-selector/property-selector.component';
import { PropertyType } from '@app/campaigns/models/propertyType';

@Component({
  selector: ' rr-campaign-template',
  standalone: true,
  templateUrl: './campaign-template.component.html',
  imports: [
    ReactiveFormsModule,
    Fieldset,
    Panel,
    NgIf,
    ButtonDirective,
    InputText,
    NgForOf,
    PropertySelectorComponent
  ],
  styleUrl: './campaign-template.component.scss'
})
export class CampaignTemplateComponent {
  public form = new UntypedFormGroup({});
  public attributeForm = new FormGroup({});
  public skillForm = new FormGroup({});
  public minorSkillForm = new FormGroup({});
  public defenseForm = new FormGroup({});
  public lifeForm = new FormGroup({});
  public isLoading = true;
  public campaign!: Campaign;
  public requiredFields = ['name'];
  public skillsMapping = new Map<string, FormArray>();
  public minorsSkillBySkill = new Map<string, FormArray<FormGroup>>();
  public selectedSkillMinorSkills: FormGroup[] = [];
  public disabled: boolean = false;
  public defaultTemplates: RROption<string>[] = [];
  public get default() {
    return this.campaign.campaignTemplate.default;
  }
  public get attributes(): FormArray<FormGroup> {
    return this.form?.get('campaignTemplate.attributes') as FormArray<FormGroup>;
  }

  public get lifes(): FormArray<FormGroup> {
    return this.form?.get('campaignTemplate.lifes') as FormArray<FormGroup>;
  }
  public get skills(): FormArray<FormGroup> {
    return this.form?.get('campaignTemplate.skills') as FormArray<FormGroup>;
  }
  public get defenses(): FormArray<FormGroup> {
    return this.form?.get('campaignTemplate.defenses') as FormArray<FormGroup>;
  }
  public attributeSkills(attributeId: string): FormArray<FormGroup> {
    return this.skillsMapping.get(attributeId);
  }

  constructor(
    public service: CampaignsService,
    private route: ActivatedRoute,
    public authService: AuthenticationService,
    public loggerService: LoggerService,
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
    this.route.data.subscribe(data => {
      this.campaign = data['campaign'];
      this.init();
      this.isLoading = false;
    });
  }


  public addAttribute() {
    if (this.disabled) return;
    const attribute = this.attributeForm.value as AttributeTemplateModel;
    this.service.addAttribute(this.campaign.id, attribute)
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
    if (this.disabled) return;
    const attribute = attributeControl.value as AttributeTemplateModel;
    this.service.updateAttribute(this.campaign.id, attribute.id, attribute)
      .subscribe();
  }


  public removeAttribute(attributeControl: FormGroup, index: number) {
    if (this.disabled) return;
    const attribute = attributeControl.value as AttributeTemplateModel;
    this.service.removeAttribute(this.campaign.id, attribute.id)
      .subscribe(() => {
        const formArray = this.attributes;
        formArray.removeAt(index);
        this.skillsMapping.delete(attribute.id);
      });
  }
  public addSkill(attributeForm: FormGroup) {
    if (this.disabled) return;
    const attribute = attributeForm.value as AttributeTemplateModel;
    const skill = this.skillForm.value as SkillTemplateModel;
    skill.attributeId = attribute.id;
    this.service.addSkill(this.campaign.id, attribute.id, skill)
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
  public addAttributelessSkill() {
    if (this.disabled) return;
    const skill = this.skillForm.value as SkillTemplateModel;
    this.service.addAttributelessSkill(this.campaign.id, skill)
      .subscribe(() => {
        const formArray = this.skills;
        const newFormGroup = new FormGroup({});
        createForm(newFormGroup, this.skillForm.value as Entity);
        formArray.controls.push(newFormGroup);
        this.skillForm.reset();
        this.skillForm.get('id').setValue(uuidv4() as never);
        const newSkillForm = new FormGroup({});
        createForm(newSkillForm, skill);
        this.skillsMapping.get('attributeless').push(newSkillForm);
        this.minorsSkillBySkill.set(skill.id, new FormArray([]));
      });
  }
  public updateSkill(attributeControl: FormGroup, skillControl: FormGroup) {
    if (this.disabled) return;
    const attribute = attributeControl.value as AttributeTemplateModel;
    const skill = skillControl.value as SkillTemplateModel;
    // skill.attributeId = attribute.id;
    this.service.updateSkill(this.campaign.id, attribute.id, skill.id, skill)
      .subscribe();
  }
  public updateAttributelessSkill(skillControl: FormGroup) {
    if (this.disabled) return;
    const skill = skillControl.value as SkillTemplateModel;
    this.service.updateAttributelessSkill(this.campaign.id, skill)
      .subscribe();
  }
  public removeSkill(attributeControl: FormGroup, skillControl: FormGroup, index: number) {
    const skill = skillControl.value as SkillTemplateModel;
    const attribute = attributeControl.value as AttributeTemplateModel;
    this.service.removeSkill(this.campaign.id, attribute.id, skill.id)
      .subscribe(() => {
        const formArray = this.skills;
        formArray.removeAt(index);
        this.skillsMapping.get(skill.attributeId).removeAt(index);
      });
  }
  public removeAttributelessSkill(skillControl: FormGroup, index: number) {
    const skill = skillControl.value as SkillTemplateModel;
    this.service.removeAttributelessSkill(this.campaign.id, skill)
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
    this.service.addMinorSkill(this.campaign.id, skill.attributeId, skill.id, minorSkill)
      .subscribe(() => {
        const newFormGroup = new FormGroup({});
        createForm(newFormGroup, minorSkill);
        this.minorSkillForm.reset();
        this.minorSkillForm.get('id').setValue(uuidv4() as never);
        this.minorsSkillBySkill.get(minorSkill.skillTemplateId).controls.push(newFormGroup);
      });
  }
  public updateMinorSkill(skillControl: FormGroup, minorSkillControl: FormGroup) {
    if (this.disabled) return;
    const skill = skillControl.value as SkillTemplateModel;
    const minorSkill = minorSkillControl.value as MinorSkillsTemplateModel;
    this.service.updateMinorSkill(this.campaign.id, skill.attributeId, skill.id, minorSkill.id, minorSkill)
      .subscribe(() => {
      });
  }
  public removeMinorSkill(skillControl: FormGroup, minorSkillControl: FormGroup, index: number) {
    if (this.disabled) return;
    const skill = skillControl.value as SkillTemplateModel;
    const minorSkill = minorSkillControl.value as MinorSkillsTemplateModel;
    this.service.removeMinorSkill(this.campaign.id, skill.attributeId, skill.id, minorSkill.id)
      .subscribe(() => {
        this.minorsSkillBySkill.get(skill.id).removeAt(index);
      });
  }

  public addLife() {
    if (this.disabled) return;
    const life = this.lifeForm.value as LifeTemplateModel;
    life.formula = '';
    this.service.addLife(this.campaign.id, life)
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
    if (this.disabled) return;
    const life = lifeControl.value as LifeTemplateModel;
    this.service.updateLife(this.campaign.id, life.id, life)
      .subscribe();
  }


  public removeLife(lifeControl: FormGroup, index: number) {
    if (this.disabled) return;
    const life = lifeControl.value as LifeTemplateModel;
    this.service.removeLife(this.campaign.id, life.id)
      .subscribe(() => {
        const formArray = this.lifes;
        formArray.removeAt(index);
      });
  }

  public addDefense() {
    if (this.disabled) return;
    const defense = this.defenseForm.value as DefenseTemplateModel;
    defense.formula = '';
    this.service.addDefense(this.campaign.id, defense)
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
    if (this.disabled) return;
    const defense = defenseControl.value as DefenseTemplateModel;
    this.service.updateDefense(this.campaign.id, defense.id, defense)
      .subscribe();
  }


  public removeDefense(defenseControl: FormGroup, index: number) {
    if (this.disabled) return;
    const defense = defenseControl.value as DefenseTemplateModel;
    this.service.removeDefense(this.campaign.id, defense.id)
      .subscribe(() => {
        const formArray = this.defenses;
        formArray.removeAt(index);
      });
  }

  init() {
    this.form = getAsForm(this.campaign)
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
      id: null,
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
    this.disabled = !this.authService.isMaster(this.campaign.masterId) || this.default;
    if (this.disabled) {
      this.form.disable();
      this.attributeForm.disable();
      this.skillForm.disable();
      this.minorSkillForm.disable();
      this.skillForm.valueChanges.subscribe(() => {
        console.log(this.skillForm.disabled)
      })
      this.lifeForm.disable();
      this.form.get('name').enable();
    }
  }
  private buildSkills() {
    this.campaign.campaignTemplate.attributes.forEach((attribute: AttributeTemplateModel) => {
      this.skillsMapping.set(attribute.id, new FormArray<any>([]));
    });
    this.skillsMapping.set('attributeless', new FormArray<any>([]));
    this.campaign.campaignTemplate.skills.forEach((skill: SkillTemplateModel) => {
      this.skillsMapping.get(skill.attributeId).push(getAsForm(skill));
      this.minorsSkillBySkill.set(skill.id, new FormArray<any>([]));
      const minorSkills = this.minorsSkillBySkill.get(skill.id);
      skill.minorSkills.forEach((minorSkill: MinorSkillsTemplateModel) => {
        minorSkills.push(getAsForm(minorSkill));
      });
    });
    this.campaign.campaignTemplate.attributelessSkills.forEach((skill: SkillTemplateModel) => {
      this.skillsMapping.get('attributeless').push(getAsForm(skill));
      this.minorsSkillBySkill.set(skill.id, new FormArray<any>([]));
      const minorSkills = this.minorsSkillBySkill.get(skill.id);
      skill.minorSkills.forEach((minorSkill: MinorSkillsTemplateModel) => {
        minorSkills.push(getAsForm(minorSkill));
      });
    });
  }

  async save() {
    await firstValueFrom(this.service.update(this.form.getRawValue()));
  }

  protected readonly PropertyType = PropertyType;
}

