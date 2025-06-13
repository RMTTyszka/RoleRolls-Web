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
    ButtonDirective,
    PropertyByIdSelectorComponent
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
  public skillsMapping = new Map<string, FormArray<FormGroup>>();
  public minorsSkillBySkill = new Map<string, FormArray<FormGroup>>();
  public creatureId: string;
  public creatureCategory: CreatureCategory;

  private subscriptionManager = new SubscriptionManager();

  public attributeSkills(attributeId: string): FormArray<FormGroup> {
    return this.skillsMapping.get(attributeId);
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
        this.subscriptionManager.add(specificSkill.get('name').value, specificSkill.get('points').valueChanges.subscribe(() => {
          this.setRemainingPoints(skill);
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
    console.log(this.form);
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
    const value = (skill.get('specificSkills') as FormArray).controls
      .map((control: AbstractControl) => Number(control.get('points').value))
      .reduce((previous: number, current: number) => current + previous, 0);
    const maxPoints = skill.get('pointsLimit').value;
    skill.get('remainingPoints').setValue(maxPoints - value);
  }

  private buildSkills() {
    this.creature.attributes.forEach(attribute => {
      this.skillsMapping.set(attribute.id, new FormArray([]));
    });
    this.skillsMapping.set('attributelessSkills', new FormArray([]));
    this.creature.skills.forEach(skill => {
      const skillForm = (this.form.get('skills') as FormArray<FormGroup>).controls
        .filter(control => control.get('skillTemplateId').value === skill.skillTemplateId)[0];
      if (skill.attributeId) {
        this.skillsMapping.get(skill.attributeId).push(skillForm);
      } else {
        this.skillsMapping.get('attributelessSkills').push(skillForm);
      }
      this.minorsSkillBySkill.set(skill.id, new FormArray([]));
      const specificSkills = this.minorsSkillBySkill.get(skill.id);
      skill.specificSkills.forEach(specificSkill => {
        const specificSkillForm = (skillForm.get('specificSkills') as FormArray<FormGroup>).controls
          .filter(control => control.get('specificSkillTemplateId').value == specificSkill.specificSkillTemplateId)[0];
        specificSkills.push(specificSkillForm);
      });
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

  protected readonly PropertyType = PropertyType;
}
const validateSkillValue: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
  const pointsLimit = Number(control.get('pointsLimit').value);
  const usedPoints = Number(control.get('usedPoints').value);
  return usedPoints > pointsLimit ? { invalidPoints: true } : null;
};
