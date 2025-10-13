import { ChangeDetectorRef, Component } from '@angular/core';
import { Campaign } from '@app/campaigns/models/campaign';
import { CreatureCategory } from '@app/campaigns/models/CreatureCategory';
import { EditorAction } from '@app/models/EntityActionData';
import {
  AbstractControl,
  FormArray,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  UntypedFormGroup,
  ValidationErrors,
  ValidatorFn
} from '@angular/forms';
import { Creature } from '@app/campaigns/models/creature';
import { SubscriptionManager } from '@app/tokens/subscription-manager';
import { getAsForm, ultraPatchValue } from '@app/tokens/EditorExtension';
import { HttpErrorResponse, HttpStatusCode } from '@angular/common/http';
import { MessageService, ToastMessageOptions } from 'primeng/api';
import { firstValueFrom } from 'rxjs';
import { CampaignsService } from '@app/campaigns/services/campaigns.service';
import { CreatureDetailsService } from '@app/creatures/creature-details.service';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Message } from 'primeng/message';
import { TabPanel, TabView } from 'primeng/tabview';
import { Panel } from 'primeng/panel';
import { NgForOf, NgIf } from '@angular/common';
import { InputNumber } from 'primeng/inputnumber';
import {
  CreatureInventoryComponent
} from '@app/creatures/creature-editor/creature-inventory/creature-inventory.component';
import { InputText } from 'primeng/inputtext';
import { Tooltip } from 'primeng/tooltip';
import { ButtonDirective } from 'primeng/button';
import {
  PropertyByIdSelectorComponent
} from '@app/components/property-by-id-selector/property-by-id-selector.component';
import { PropertyType } from '@app/campaigns/models/propertyType';

@Component({
  selector: 'rr-creature-editor',
  imports: [
    Message,
    TabPanel,
    ReactiveFormsModule,
    TabView,
    Panel,
    NgForOf,
    InputNumber,
    CreatureInventoryComponent,
    InputText,
    Tooltip,
    NgIf,
  ],
  templateUrl: './creature-editor.component.html',
  styleUrl: './creature-editor.component.scss'
})
export class CreatureEditorComponent {

  public get attributes(): FormArray<FormGroup> {
    return this.form.get('attributes') as FormArray<FormGroup>;
  }

  public get skills(): FormArray<FormGroup> {
    return this.form.get('skills') as FormArray<FormGroup>;
  }

  public get vitalities(): FormArray<FormGroup> {
    return this.form.get('vitalities') as FormArray<FormGroup>;
  }

  public get defenses(): FormArray<FormGroup> {
    return this.form.get('defenses') as FormArray<FormGroup>;
  }

  constructor(
    private campaignService: CampaignsService,
    public config: DynamicDialogConfig,
    public dialogRef: DynamicDialogRef,
    public detectChanges: ChangeDetectorRef,
    public messageService: MessageService,
    public creatureDetailsService: CreatureDetailsService,
  ) {
    this.campaign = config.data.campaign;
    this.editorAction = config.data.action;
    this.creatureCategory = config.data.creatureCategory;
    if (this.editorAction === EditorAction.update) {
      this.creatureId = config.data.creatureId;
    }
  }

  public loaded = false;
  public form: UntypedFormGroup;
  public campaign: Campaign;
  // public template: PocketCreature;
  public creature: Creature;
  public editorAction: EditorAction;
  public minorsSkillBySkill = new Map<string, FormArray<FormGroup>>();
  public creatureId: string;
  public creatureCategory: CreatureCategory;

  private subscriptionManager = new SubscriptionManager();

  // attributeSkills mapping removed; iterate directly over skills FormArray in template

  public get totalSkillPoints(): number {
    const raw = this.form?.get('totalSkillsPoints')?.value;
    const source = (raw === '' || raw === null || raw === undefined)
      ? this.creature?.totalSkillsPoints
      : raw;
    const n = Number(source);
    return Number.isFinite(n) ? n : 0;
  }

  public get usedSkillPoints(): number {
    if (!this.form) { return 0; }
    const allSkills = (this.form.get('skills') as FormArray<FormGroup>)?.controls ?? [];
    return allSkills
      .flatMap((s: FormGroup) => (s.get('specificSkills') as FormArray)?.controls ?? [])
      .map((c: AbstractControl) => Number(c.get('points').value))
      .reduce((prev: number, cur: number) => prev + cur, 0);
  }

  public get remainingSkillPoints(): number {   const rem = this.totalSkillPoints - this.usedSkillPoints;   return Math.max(0, rem); }

  public getSpecificSkillMax(skill: FormGroup, specificSkill: FormGroup): number {
    const maxPer = Number(this.creature?.maxPointsPerSpecificSkill ?? this.form?.get('maxPointsPerSpecificSkill')?.value ?? 0);
    const current = Number(specificSkill?.get('points')?.value ?? 0);
    const remaining = Math.max(this.remainingSkillPoints, 0);
    return Math.min(maxPer, current + remaining);
  }

  ngOnDestroy(): void {
    this.subscriptionManager.clear();
  }

  async ngOnInit(): Promise<void> {
    if (this.editorAction === EditorAction.create) {
      this.creature = await this.campaignService.instantiateNewCreature(this.campaign.id, this.creatureCategory).toPromise();
    } else {
      this.creature = await this.campaignService.getCreature(this.campaign.id, this.creatureId).toPromise();
    }
    this.form = getAsForm(this.creature);
    this.attributes.controls.forEach(attribute => {
      attribute.get('name').disable();
    });
    this.skills.controls.forEach((skill: FormGroup) => {
      skill.get('name').disable();
      skill.get('pointsLimit').disable();
      skill.get('usedPoints').disable();
      skill.addControl('remainingPoints', new FormControl());
      skill.get('remainingPoints').disable();
      this.setRemainingPoints(skill);
      skill.setValidators(validateSkillValue);
      (skill.get('specificSkills') as FormArray).controls.forEach(specificSkill => {
        specificSkill.get('name').disable();
        // specificSkill.get('points').setValidators([Validators.max(3),  Validators.min(-1)]);
        this.subscriptionManager.add(specificSkill.get('name').value, specificSkill.get('points').valueChanges.subscribe((val) => {
  const v = Number(val ?? 0);

  // Total limit (from form or creature)
  const rawLimit = this.form.get('totalSkillsPoints')?.value;
  const limitSource = (rawLimit === '' || rawLimit === null || rawLimit === undefined)
    ? this.creature.totalSkillsPoints
    : rawLimit;
  const totalLimit = Number(limitSource);

  // Sum used AFTER this change (replace this control's value with v)
  const allSkills = (this.form.get('skills') as FormArray<FormGroup>).controls;
  const totalUsedAfter = allSkills
    .flatMap((s: FormGroup) => (s.get('specificSkills') as FormArray).controls)
    .map((c: AbstractControl) => {
      if (c === specificSkill) {
        return Number.isFinite(v) ? v : 0;
      }
      const n = Number(c.get('points')?.value);
      return Number.isFinite(n) ? n : 0;
    })
    .reduce((prev: number, cur: number) => prev + cur, 0);

  let clamped = v;

  // Reduce the edited value by overflow so remaining never goes below zero
  const overflow = totalUsedAfter - totalLimit;
  if (overflow > 0) {
    clamped = v - overflow;
  }

  // Per-specific caps and minimum
  const maxPer = Number(this.creature?.maxPointsPerSpecificSkill ?? this.form?.get('maxPointsPerSpecificSkill')?.value ?? 0);
  if (clamped > maxPer) { clamped = maxPer; }
  if (clamped < -1) { clamped = -1; }

  if (clamped !== v) {
    specificSkill.get('points').setValue(clamped, { emitEvent: false });
  }

  this.setRemainingPoints(skill as FormGroup);
  skill.get('usedPoints').updateValueAndValidity();
}));
      });
    });
    this.buildSkills();
    (this.form.get('vitalities') as FormArray).disable();
    (this.form.get('defenses') as FormArray).disable();
    this.subscribeToRefreshCreature();
    this.loaded = true;
  }

  public print() {
   // console.log(this.form);
  }

  public canSave() {
    return this.form ? this.form.valid : false;
  }

  public save() {
    const creature = this.form.getRawValue() as Creature;
    creature.category = this.creatureCategory;
    if (this.creatureCategory === CreatureCategory.Enemy) {
      creature.isTemplate = true;
    }
    const saveAction = this.editorAction === EditorAction.create ? this.campaignService.createCreature(this.campaign.id, creature)
      : this.campaignService.updateCreature(this.campaign.id, creature);
    saveAction.subscribe(() => {
      this.dialogRef.close(true);
    }, (error: HttpErrorResponse) => {
      if (error.status === HttpStatusCode.UnprocessableEntity) {
        this.messageService.add({
          severity: 'error',
          summary: `The ${error.error.invalidProperty} has invalid points`,
          detail: 'Allocated points greater than available points.'
        } as ToastMessageOptions);
      }
    });
  }

  private setRemainingPoints(skill: FormGroup): void {
    // Global remaining points across all skills
    const allSkills = (this.form.get('skills') as FormArray<FormGroup>).controls;
    const totalUsed = allSkills
      .flatMap((s: FormGroup) => (s.get('specificSkills') as FormArray).controls)
      .map((c: AbstractControl) => {
        const v = c.get('points')?.value;
        const n = Number(v);
        return Number.isFinite(n) ? n : 0;
      })
      .reduce((prev: number, cur: number) => prev + cur, 0);
    const rawLimit = this.form.get('totalSkillsPoints')?.value;
    const limitSource = (rawLimit === '' || rawLimit === null || rawLimit === undefined)
      ? this.creature.totalSkillsPoints
      : rawLimit;
    const totalLimit = Number(limitSource);
    const remaining = totalLimit - totalUsed;
    skill.get('remainingPoints').setValue(remaining);
  }

  private buildSkills() {
    // Build minorsSkillBySkill map for quick access in template
    this.minorsSkillBySkill = new Map<string, FormArray<FormGroup>>();
    this.creature.skills.forEach(skill => {
      const skillForm = (this.form.get('skills') as FormArray<FormGroup>).controls
        .find(control => control.get('skillTemplateId').value === skill.skillTemplateId);
      if (!skillForm) { return; }
      const specificSkillsArray = new FormArray<FormGroup>([]);
      (skillForm.get('specificSkills') as FormArray<FormGroup>).controls.forEach(specificSkillForm => {
        specificSkillsArray.push(specificSkillForm);
      });
      this.minorsSkillBySkill.set(skill.id, specificSkillsArray);
    });
  }

  private subscribeToRefreshCreature() {
    this.subscriptionManager.add('refreshCreature', this.creatureDetailsService.refreshCreature.subscribe(async () => {
      this.creature = await firstValueFrom(this.campaignService.getCreature(this.campaign.id, this.creatureId));
      ultraPatchValue(this.form, this.creature, {
        entityName: 'creature'
      });
    }));
  }

  
  public incrementSpecific(skill: FormGroup, specificSkill: FormGroup): void {
    const ctrl = specificSkill.get('points');
    const current = Number(ctrl?.value ?? 0);
    const proposed = current + 1;
    ctrl?.setValue(proposed);
  }

  public decrementSpecific(skill: FormGroup, specificSkill: FormGroup): void {
    const ctrl = specificSkill.get('points');
    const current = Number(ctrl?.value ?? 0);
    const proposed = current - 1;
    ctrl?.setValue(proposed);
  }  protected readonly PropertyType = PropertyType;
}
const validateSkillValue: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
  // Validation now handled at creature-level server-side. Always valid here.
  return null;
};



