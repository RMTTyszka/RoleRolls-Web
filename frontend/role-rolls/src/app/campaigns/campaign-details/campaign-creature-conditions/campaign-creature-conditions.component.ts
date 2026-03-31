import { Component } from '@angular/core';
import { FormArray, FormControl, FormGroup, ReactiveFormsModule, UntypedFormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NgForOf, NgIf } from '@angular/common';
import { Fieldset } from 'primeng/fieldset';
import { Panel } from 'primeng/panel';
import { InputText } from 'primeng/inputtext';
import { ButtonDirective } from 'primeng/button';
import { InputGroupAddonModule } from 'primeng/inputgroupaddon';
import { Textarea } from 'primeng/textarea';
import { v4 as uuidv4 } from 'uuid';
import { createForm, getAsForm } from '@app/tokens/EditorExtension';
import { Campaign } from '@app/campaigns/models/campaign';
import { CreatureCondition } from '@app/campaigns/models/campaign.template';
import { CampaignsService } from '@app/campaigns/services/campaigns.service';
import { canEditTemplate } from '@app/tokens/utils.funcs';
import { BonusesComponent } from '@app/bonuses/bonuses/bonuses.component';
import { Bonus, Property } from '@app/models/bonuses/bonus';
import { EditorAction, EntityActionData } from '@app/models/EntityActionData';

@Component({
  selector: 'rr-campaign-creature-conditions',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    NgIf,
    NgForOf,
    Fieldset,
    Panel,
    InputText,
    ButtonDirective,
    InputGroupAddonModule,
    Textarea,
    BonusesComponent
  ],
  templateUrl: './campaign-creature-conditions.component.html',
  styleUrl: './campaign-creature-conditions.component.scss'
})
export class CampaignCreatureConditionsComponent {
  public form = new UntypedFormGroup({});
  public creatureConditionForm = new UntypedFormGroup({});
  public campaign!: Campaign;
  public isLoading = true;
  public disabled = false;

  public get creatureConditions(): FormArray<FormGroup> {
    return this.form.get('campaignTemplate.creatureConditions') as unknown as FormArray<FormGroup>;
  }

  public get vitalities(): FormArray<FormGroup> {
    return this.form.get('campaignTemplate.vitalities') as unknown as FormArray<FormGroup>;
  }

  constructor(
    private readonly service: CampaignsService,
    private readonly route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.campaign = data['campaign'] as Campaign;
      this.init();
      this.isLoading = false;
    });
  }

  private init(): void {
    this.campaign.campaignTemplate.creatureConditions = (this.campaign.campaignTemplate.creatureConditions ?? [])
      .map(condition => ({
        ...condition,
        bonuses: condition.bonuses ?? []
      }));

    this.form = getAsForm(this.campaign);
    this.normalizeBonusesControls();
    this.creatureConditionForm = new UntypedFormGroup({});
    createForm(this.creatureConditionForm, {
      id: uuidv4(),
      name: null,
      description: null,
      bonuses: []
    } as CreatureCondition);
    this.ensureBonusesControl(this.creatureConditionForm as unknown as FormGroup);

    this.disabled = !canEditTemplate(this.campaign);
    if (this.disabled) {
      this.form.disable();
      this.creatureConditionForm.disable();
    }
  }

  public addCreatureCondition(): void {
    if (this.disabled) return;
    const creatureCondition = this.normalizeCreatureConditionForPersistence(
      this.creatureConditionForm.value as CreatureCondition);
    this.service.addCreatureCondition(this.campaign.id, creatureCondition)
      .subscribe(() => {
        const newFormGroup = new FormGroup({});
        createForm(newFormGroup, creatureCondition);
        this.ensureBonusesControl(newFormGroup);
        this.creatureConditions.push(newFormGroup);
        this.campaign.campaignTemplate.creatureConditions = [
          ...(this.campaign.campaignTemplate.creatureConditions ?? []),
          creatureCondition
        ];
        this.creatureConditionForm.reset();
        this.creatureConditionForm.get('id')?.setValue(uuidv4() as never);
      });
  }

  public updateCreatureCondition(conditionControl: FormGroup): void {
    if (this.disabled) return;
    const creatureCondition = this.normalizeCreatureConditionForPersistence(
      conditionControl.value as CreatureCondition);
    this.service.updateCreatureCondition(this.campaign.id, creatureCondition.id, creatureCondition)
      .subscribe(() => {
        this.campaign.campaignTemplate.creatureConditions = (this.campaign.campaignTemplate.creatureConditions ?? [])
          .map(condition => condition.id === creatureCondition.id ? creatureCondition : condition);
      });
  }

  public removeCreatureCondition(conditionControl: FormGroup, index: number): void {
    if (this.disabled) return;
    const creatureCondition = conditionControl.value as CreatureCondition;
    this.service.removeCreatureCondition(this.campaign.id, creatureCondition.id)
      .subscribe(() => {
        this.creatureConditions.removeAt(index);
        this.campaign.campaignTemplate.creatureConditions =
          (this.campaign.campaignTemplate.creatureConditions ?? [])
            .filter(condition => condition.id !== creatureCondition.id);
        this.campaign.campaignTemplate.vitalities = (this.campaign.campaignTemplate.vitalities ?? []).map(vitality => ({
          ...vitality,
          conditionAtThirtyPercent: this.clearConditionReference(vitality.conditionAtThirtyPercent, creatureCondition.id),
          conditionAtZero: this.clearConditionReference(vitality.conditionAtZero, creatureCondition.id)
        }));

        this.vitalities.controls.forEach(vitalityControl => {
          const currentThirty = vitalityControl.get('conditionAtThirtyPercent')?.value as Property | null;
          if (currentThirty?.id === creatureCondition.id) {
            vitalityControl.get('conditionAtThirtyPercent')?.setValue(null);
          }

          const currentZero = vitalityControl.get('conditionAtZero')?.value as Property | null;
          if (currentZero?.id === creatureCondition.id) {
            vitalityControl.get('conditionAtZero')?.setValue(null);
          }
        });
      });
  }

  public onCreatureConditionBonusUpdated(conditionControl: FormGroup, action: EntityActionData<Bonus>): void {
    const bonuses = [...((conditionControl.get('bonuses')?.value as Bonus[]) ?? [])];
    switch (action.action) {
      case EditorAction.create:
        bonuses.push(action.entity);
        break;
      case EditorAction.update: {
        const index = bonuses.findIndex(bonus => bonus.id === action.entity.id);
        if (index >= 0) {
          bonuses[index] = action.entity;
        }
        break;
      }
      case EditorAction.delete:
        this.setBonusesValue(conditionControl, bonuses.filter(bonus => bonus.id !== action.entity.id));
        this.updateCreatureCondition(conditionControl);
        return;
    }

    this.setBonusesValue(conditionControl, bonuses);
    this.updateCreatureCondition(conditionControl);
  }

  private normalizeBonusesControls(): void {
    this.creatureConditions?.controls?.forEach(control => this.ensureBonusesControl(control));
  }

  private ensureBonusesControl(group: FormGroup | null | undefined): void {
    if (!group) {
      return;
    }

    const control = group.get('bonuses');
    const value = control instanceof FormArray ? control.value : control?.value;
    if (control) {
      group.removeControl('bonuses');
    }

    group.addControl('bonuses', new FormControl<Bonus[]>(Array.isArray(value) ? value : []));
  }

  private setBonusesValue(group: FormGroup, bonuses: Bonus[]): void {
    const bonusesControl = group.get('bonuses');
    if (bonusesControl instanceof FormControl) {
      bonusesControl.setValue(bonuses ?? []);
      return;
    }

    group.setControl('bonuses', new FormControl<Bonus[]>(bonuses ?? []));
  }

  private normalizeCreatureConditionForPersistence(creatureCondition: CreatureCondition): CreatureCondition {
    return {
      ...creatureCondition,
      name: creatureCondition.name?.trim() ?? '',
      description: creatureCondition.description?.trim() ?? '',
      bonuses: creatureCondition.bonuses ?? []
    };
  }

  private clearConditionReference(property: Property | null | undefined, conditionId: string): Property | null {
    if (!property || property.id !== conditionId) {
      return property ?? null;
    }

    return null;
  }
}
