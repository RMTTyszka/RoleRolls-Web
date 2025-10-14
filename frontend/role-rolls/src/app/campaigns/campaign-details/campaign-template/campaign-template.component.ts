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
import {AttributeTemplate, DefenseTemplate, VitalityTemplate, SpecificSkillsTemplate, SkillTemplate} from 'app/campaigns/models/campaign.template';
import {LoggerService} from '@services/logger/logger.service';
import {firstValueFrom} from 'rxjs';
import { PropertySelectorComponent } from '@app/components/property-selector/property-selector.component';
import { PropertyType } from '@app/campaigns/models/propertyType';
import {
  PropertyByIdSelectorComponent
} from '@app/components/property-by-id-selector/property-by-id-selector.component';

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
    PropertyByIdSelectorComponent
  ],
  styleUrl: './campaign-template.component.scss'
})
export class CampaignTemplateComponent {
  public form = new UntypedFormGroup({});
  public attributeForm = new FormGroup({});
  public skillForm = new FormGroup({});
  public specificSkillForm = new FormGroup({});
  public defenseForm = new FormGroup({});
  public vitalityForm = new FormGroup({});
  public isLoading = true;
  public campaign!: Campaign;
  public requiredFields = ['name'];
  public skillsMapping = new Map<string, FormArray>();
  public minorsSkillBySkill = new Map<string, FormArray<FormGroup>>();
  public selectedSkillSpecificSkills: FormGroup[] = [];
  public disabled: boolean = false;
  public defaultTemplates: RROption<string>[] = [];
  public get default() {
    return this.campaign.campaignTemplate.default;
  }
  public get attributes(): FormArray<FormGroup> {
    return this.form?.get('campaignTemplate.attributes') as FormArray<FormGroup>;
  }

  public get vitalities(): FormArray<FormGroup> {
    return this.form?.get('campaignTemplate.vitalities') as FormArray<FormGroup>;
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
  init() {
    this.form = getAsForm(this.campaign)
    createForm(this.attributeForm, {
      id: null,
      name: null,
    } as AttributeTemplate);
    createForm(this.skillForm, {
      id: null,
      name: null,
      specificSkillTemplates: []
    } as any);
    createForm(this.specificSkillForm, {
      id: null,
      name: null,
      skillTemplateId: null
    } as  SpecificSkillsTemplate);
    createForm(this.vitalityForm, {
      id: null,
      name: null,
      formula: null
    } as VitalityTemplate);
    createForm(this.defenseForm, {
      id: null,
      name: null,
      formula: null
    } as DefenseTemplate);

    this.attributeForm.get('id').setValue(uuidv4() as never);
    this.skillForm.get('id').setValue(uuidv4() as never);
    this.specificSkillForm.get('id').setValue(uuidv4() as never);
    this.vitalityForm.get('id').setValue(uuidv4() as never);
    this.defenseForm.get('id').setValue(uuidv4() as never);

    this.buildSkills();
    this.disabled = !this.authService.isMaster(this.campaign.masterId) || this.default;
    if (this.disabled) {
      this.form.disable();
      this.attributeForm.disable();
      this.skillForm.disable();
      this.specificSkillForm.disable();
      this.skillForm.valueChanges.subscribe(() => {
        console.log(this.skillForm.disabled)
      })
      this.vitalityForm.disable();
      this.skills.controls.forEach((control) => control.disable());
      this.form.get('name').enable();
    }
  }

  public addAttribute() {
    if (this.disabled) return;
    const attribute = this.attributeForm.value as AttributeTemplate;
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
    const attribute = attributeControl.value as AttributeTemplate;
    this.service.updateAttribute(this.campaign.id, attribute.id, attribute)
      .subscribe();
  }


  public removeAttribute(attributeControl: FormGroup, index: number) {
    if (this.disabled) return;
    const attribute = attributeControl.value as AttributeTemplate;
    this.service.removeAttribute(this.campaign.id, attribute.id)
      .subscribe(() => {
        const formArray = this.attributes;
        formArray.removeAt(index);
        this.skillsMapping.delete(attribute.id);
      });
  }
  public addSkill(attributeForm: FormGroup) { this.addSkillFromHeader(); }
  public addSkillFromHeader() {
    if (this.disabled) return;
    const skill = this.skillForm.value as any;
    const afterAdd = () => {
      const formArray = this.skills;
      const newFormGroup = new FormGroup({});
      createForm(newFormGroup, this.skillForm.value as Entity);
      formArray.controls.push(newFormGroup);
      this.skillForm.reset();
      this.skillForm.get('id').setValue(uuidv4() as never);
      this.minorsSkillBySkill.set(skill.id, new FormArray([]));
    };
    this.service.addAttributelessSkill(this.campaign.id, skill).subscribe(afterAdd);
  }
  public addAttributelessSkill() {
    if (this.disabled) return;
    const skill = this.skillForm.value as SkillTemplate;
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
  public updateSkill(attributeControl: FormGroup, skillControl: FormGroup) { this.updateSkillFlat(skillControl); }
  public updateSkillFlat(skillControl: FormGroup) {
    if (this.disabled) return;
    const skill = skillControl.value as any;
    this.service.updateAttributelessSkill(this.campaign.id, skill).subscribe();
  }
  public updateAttributelessSkill(skillControl: FormGroup) {
    if (this.disabled) return;
    const skill = skillControl.value as SkillTemplate;
    this.service.updateAttributelessSkill(this.campaign.id, skill)
      .subscribe();
  }
  public removeSkill(attributeControl: FormGroup, skillControl: FormGroup, index: number) { this.removeSkillFlat(skillControl, index); }
  public removeSkillFlat(skillControl: FormGroup, index: number) {
    const skill = skillControl.value as any;
    this.service.removeAttributelessSkill(this.campaign.id, skill).subscribe(() => {
      this.skills.removeAt(index);
    });
  }
  public removeAttributelessSkill(skillControl: FormGroup, index: number) {
    const skill = skillControl.value as SkillTemplate;
    this.service.removeAttributelessSkill(this.campaign.id, skill)
      .subscribe(() => {
        const formArray = this.skills;
        formArray.removeAt(index);
        this.skillsMapping.get('attributeless').removeAt(index);
      });
  }

  public addSpecificSkill(skillForm: FormGroup) {
    const specificSkill = this.specificSkillForm.value as SpecificSkillsTemplate;
    const skill = skillForm.value as SkillTemplate;
    specificSkill.skillTemplateId = skill.id;
    this.service.addSpecificSkill(this.campaign.id, skill.id, specificSkill)
      .subscribe(() => {
        const newFormGroup = new FormGroup({});
        createForm(newFormGroup, specificSkill);
        this.specificSkillForm.reset();
        this.specificSkillForm.get('id').setValue(uuidv4() as never);
        this.minorsSkillBySkill.get(specificSkill.skillTemplateId).controls.push(newFormGroup);
      });
  }
  public updateSpecificSkill(skillControl: FormGroup, specificSkillControl: FormGroup) {
    if (this.disabled) return;
    const skill = skillControl.value as SkillTemplate;
    const specificSkill = specificSkillControl.value as SpecificSkillsTemplate;
    this.service.updateSpecificSkill(this.campaign.id, skill.id, specificSkill.id, specificSkill)
      .subscribe(() => {
      });
  }
  public removeSpecificSkill(skillControl: FormGroup, specificSkillControl: FormGroup, index: number) {
    if (this.disabled) return;
    const skill = skillControl.value as SkillTemplate;
    const specificSkill = specificSkillControl.value as SpecificSkillsTemplate;
    this.service.removeSpecificSkill(this.campaign.id, skill.id, specificSkill.id)
      .subscribe(() => {
        this.minorsSkillBySkill.get(skill.id).removeAt(index);
      });
  }

  public addVitality() {
    if (this.disabled) return;
    const vitality = this.vitalityForm.value as VitalityTemplate;
    vitality.formula = '';
    this.service.addVitality(this.campaign.id, vitality)
      .subscribe(() => {
        const formArray = this.vitalities;
        const newFormGroup = new FormGroup({});
        createForm(newFormGroup, this.vitalityForm.value as Entity);
        formArray.controls.push(newFormGroup);
        this.vitalityForm.reset();
        this.vitalityForm.get('id').setValue(uuidv4() as never);
      });
  }

  public updateVitality(vitalityControl: FormGroup) {
    if (this.disabled) return;
    const vitality = vitalityControl.value as VitalityTemplate;
    this.service.updateVitality(this.campaign.id, vitality.id, vitality)
      .subscribe();
  }


  public removeVitality(vitalityControl: FormGroup, index: number) {
    if (this.disabled) return;
    const vitality = vitalityControl.value as VitalityTemplate;
    this.service.removeVitality(this.campaign.id, vitality.id)
      .subscribe(() => {
        const formArray = this.vitalities;
        formArray.removeAt(index);
      });
  }

  public addDefense() {
    if (this.disabled) return;
    const defense = this.defenseForm.value as DefenseTemplate;
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
    const defense = defenseControl.value as DefenseTemplate;
    this.service.updateDefense(this.campaign.id, defense.id, defense)
      .subscribe();
  }


  public removeDefense(defenseControl: FormGroup, index: number) {
    if (this.disabled) return;
    const defense = defenseControl.value as DefenseTemplate;
    this.service.removeDefense(this.campaign.id, defense.id)
      .subscribe(() => {
        const formArray = this.defenses;
        formArray.removeAt(index);
      });
  }

  private buildSkills() {
    // Build minorsSkillBySkill and ensure helper control on each skill form
    this.minorsSkillBySkill = new Map<string, FormArray<FormGroup>>();
    (this.skills as FormArray<FormGroup>).controls.forEach((skillForm: FormGroup) => {
      const skill = skillForm.value as SkillTemplate;
      const specificSkillsArray = new FormArray<FormGroup>([]);
      (skillForm.get('specificSkillTemplates') as FormArray<FormGroup>).controls.forEach(ss => specificSkillsArray.push(ss));
      this.minorsSkillBySkill.set(skill.id, specificSkillsArray);
    });
  }

  async save() {
    await firstValueFrom(this.service.update(this.form.getRawValue()));
  }

  protected readonly PropertyType = PropertyType;
}


