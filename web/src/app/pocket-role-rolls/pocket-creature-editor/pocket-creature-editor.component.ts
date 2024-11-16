import {Component, OnInit, ChangeDetectorRef, Signal, signal} from '@angular/core';
import {
  FormArray,
  FormGroup,
  FormControl,
  ValidatorFn,
  ValidationErrors,
  AbstractControl,
  Validators,
  UntypedFormGroup, UntypedFormArray
} from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { EditorAction } from 'src/app/shared/dtos/ModalEntityData';
import {createForm, getAsForm, ultraPatchValue} from 'src/app/shared/EditorExtension';
import { CreatureType } from 'src/app/shared/models/creatures/CreatureType';
import { PocketCampaignModel } from 'src/app/shared/models/pocket/campaigns/pocket.campaign.model';
import { PocketCreature, PocketSkillProficience } from 'src/app/shared/models/pocket/creatures/pocket-creature';
import { PocketCampaignsService } from '../campaigns/pocket-campaigns.service';
import { SubscriptionManager } from '../../shared/utils/subscription-manager';
import {HttpErrorResponse, HttpStatusCode} from '@angular/common/http';
import {Message, MessageService} from 'primeng/api';
import {CreatureDetailsService} from './creature-details.service';
import {firstValueFrom} from 'rxjs';


@Component({
  selector: 'rr-pocket-creature-editor',
  templateUrl: './pocket-creature-editor.component.html',
  styleUrls: ['./pocket-creature-editor.component.scss'],
  providers: [MessageService]
})
export class PocketCreatureEditorComponent implements OnInit {

  public get attributes(): UntypedFormArray {
    return this.form.get('attributes') as UntypedFormArray;
  }

  public get skills(): UntypedFormArray {
    return this.form.get('skills') as UntypedFormArray;
  }

  public get lifes(): UntypedFormArray {
    return this.form.get('lifes') as UntypedFormArray;
  }
  public get defenses(): UntypedFormArray {
    return this.form.get('defenses') as UntypedFormArray;
  }
  constructor(
    private campaignService: PocketCampaignsService,
    public config: DynamicDialogConfig,
    public dialogRef: DynamicDialogRef,
    public detectChanges: ChangeDetectorRef,
    public messageService: MessageService,
    public creatureDetailsService: CreatureDetailsService,

  ) {
    this.campaign = config.data.campaign;
    this.editorAction = config.data.action;
    this.creatureType = config.data.creatureType;
    if (this.editorAction === EditorAction.update) {
      this.creatureId = config.data.creatureId;
    }
   }

  public loaded = false;
  public form: UntypedFormGroup;
  public campaign: PocketCampaignModel;
  // public template: PocketCreature;
  public creature: PocketCreature;
  public editorAction: EditorAction;
  public skillsMapping = new Map<string, UntypedFormArray>();
  public minorsSkillBySkill = new Map<string, UntypedFormArray>();
  public creatureId: string;
  public creatureType: CreatureType;

  private subscriptionManager = new SubscriptionManager();
  public attributeSkills(attributeId: string): UntypedFormArray {
    return this.skillsMapping.get(attributeId);
  }
  async ngOnInit(): Promise<void> {
    if (this.editorAction === EditorAction.create) {
      this.creature = await this.campaignService.instantiateNewCreature(this.campaign.id, this.creatureType).toPromise();
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
      (skill.get('minorSkills') as FormArray).controls.forEach(minorSkill => {
        minorSkill.get('name').disable();
        // minorSkill.get('points').setValidators([Validators.max(3),  Validators.min(-1)]);
        this.subscriptionManager.add(minorSkill.get('name').value, minorSkill.get('points').valueChanges.subscribe(() => {
          this.setRemainingPoints(skill);
          skill.get('usedPoints').updateValueAndValidity();
        }));
      });
    });
    this.buildSkills();
    (this.form.get('lifes') as FormArray).disable();
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
    const creature = this.form.getRawValue() as PocketCreature;
    creature.creatureType = CreatureType.Hero;
    const saveAction = this.editorAction === EditorAction.create ? this.campaignService.createCreature(this.campaign.id, creature)
    : this.campaignService.updateCreature(this.campaign.id, creature);
    saveAction.subscribe(() => {
      this.dialogRef.close();
    }, (error: HttpErrorResponse) => {
      if (error.status === HttpStatusCode.UnprocessableEntity) {
        this.messageService.add({
          severity: 'error',
          summary: `The ${error.error.invalidProperty} has invalid points`,
          detail: 'Allocated points greater than available points.'
        } as Message);
      }
    });
  }
  private setRemainingPoints(skill: FormGroup): void {
    const value = (skill.get('minorSkills') as FormArray).controls
    .map((control: FormControl) => Number(control.get('points').value))
    .reduce((previous: number, current: number) => current + previous, 0);
    const maxPoints = skill.get('pointsLimit').value;
    skill.get('remainingPoints').setValue(maxPoints - value);
  }
  private buildSkills() {
    this.creature.attributes.forEach(attribute => {
      this.skillsMapping.set(attribute.id, new FormArray([]));
    });
    this.creature.skills.forEach(skill => {
      const skillForm = (this.form.get('skills') as FormArray).controls.filter(control => control.get('skillTemplateId').value == skill.skillTemplateId)[0];
      this.skillsMapping.get(skill.attributeId).push(skillForm);
      this.minorsSkillBySkill.set(skill.id, new FormArray([]));
      const minorSkills = this.minorsSkillBySkill.get(skill.id);
      skill.minorSkills.forEach(minorSkill => {
        const minorSkillForm = (skillForm.get('minorSkills') as FormArray).controls.filter(control => control.get('minorSkillTemplateId').value == minorSkill.minorSkillTemplateId)[0];
        minorSkills.push(minorSkillForm);
      });
    });
  }

  private subscribeToRefreshCreature() {
    this.subscriptionManager.add('refreshCreature', this.creatureDetailsService.refreshCreature.subscribe(async () => {
      this.creature = await firstValueFrom(this.campaignService.getCreature(this.campaign.id, this.creatureId));
      ultraPatchValue(this.form, this.creature);
    }));
  }
}

const validateSkillValue: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
  const pointsLimit = Number(control.get('pointsLimit').value);
  const usedPoints = Number(control.get('usedPoints').value);
  return usedPoints > pointsLimit ? { invalidPoints: true } : null;
};
