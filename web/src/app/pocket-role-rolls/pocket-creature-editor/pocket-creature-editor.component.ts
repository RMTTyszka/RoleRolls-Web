import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { FormArray, FormGroup, FormControl, ValidatorFn, ValidationErrors, AbstractControl, Validators } from '@angular/forms';
import { DropdownItem } from 'primeng/dropdown';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { map, tap } from 'rxjs/operators';
import { CampaignsService } from 'src/app/campaign/campaigns.service';
import { EditorAction } from 'src/app/shared/dtos/ModalEntityData';
import { createForm, getAsForm } from 'src/app/shared/EditorExtension';
import { CreatureType } from 'src/app/shared/models/creatures/CreatureType';
import { PocketCampaignModel } from 'src/app/shared/models/pocket/campaigns/pocket.campaign.model';
import { CreatureTemplateModel } from 'src/app/shared/models/pocket/creature-templates/creature-template.model';
import { PocketCreature, PocketSkillProficience } from 'src/app/shared/models/pocket/creatures/pocket-creature';
import { PocketCampaignsService } from '../campaigns/pocket-campaigns.service';
import { v4 as uuidv4 } from 'uuid';
import { SubscriptionManager } from '../../shared/utils/subscription-manager';

@Component({
  selector: 'rr-pocket-creature-editor',
  templateUrl: './pocket-creature-editor.component.html',
  styleUrls: ['./pocket-creature-editor.component.scss']
})
export class PocketCreatureEditorComponent implements OnInit {


  public loaded = false;
  public form: FormGroup;
  public campaign: PocketCampaignModel;
  public template: PocketCreature;
  public creature: PocketCreature;
  public editorAction: EditorAction;
  public skillsMapping = new Map<string, FormArray>();
  public minorsSkillBySkill = new Map<string, FormArray>();

  public get attributes(): FormArray {
    return this.form.get('attributes') as FormArray;
  }

  public get skills(): FormArray {
    return this.form.get('skills') as FormArray;
  }

  public get lifes(): FormArray {
    return this.form.get('lifes') as FormArray;
  }
  public attributeSkills(attributeId: string): FormArray {
    return this.skillsMapping.get(attributeId);
  }

  private subscriptionManager = new SubscriptionManager();
  constructor(
    private campaignService: PocketCampaignsService,
    public config: DynamicDialogConfig,
    public dialogRef: DynamicDialogRef,
    public detectChanges: ChangeDetectorRef,

  ) {
    this.campaign = config.data.campaign;
    this.editorAction = config.data.action;
   }

  ngOnInit(): void {
    this.campaignService.getCreatureTemplate(this.campaign.creatureTemplateId)
    .pipe(map((template => {
      const creature = template as unknown as PocketCreature;
      if (this.editorAction === EditorAction.create) {
        creature.attributes.forEach(attribute => {
          attribute.value = 0;
          attribute.attributeTemplateId = attribute.id;
          attribute.id = uuidv4();
        });
        creature.skills.forEach(skill => {
          skill.value = 0;
          skill.usedPoints = 0;
          skill.skillTemplateId = skill.id;
          skill.id = uuidv4();
          skill.attributeId = creature.attributes.find(a => a.attributeTemplateId === skill.attributeId).id;
          skill.minorSkills.forEach(minorSkill => {
            minorSkill.points = 0;
            minorSkill.minorSkillTemplateId = minorSkill.id;
            minorSkill.id = uuidv4();
            minorSkill.skillId = skill.id;
          });
        });
        creature.name = '';
      }
      this.creature = creature;
      return creature;
    }))).subscribe((template: PocketCreature) => {
      this.template = template;
      this.form = getAsForm(this.creature);
      this.attributes.controls.forEach(attribute => {
        attribute.get('name').disable();
      });
      this.skills.controls.forEach((skill: FormGroup) => {
        skill.get('name').disable();
        skill.get('pointsLimit').disable();
        skill.get('usedPoints').disable();
        skill.addControl('remainingPoints', new FormControl())
        this.setRemainingPoints(skill);
        skill.setValidators(validateSkillValue);
        (skill.get('minorSkills') as FormArray).controls.forEach(minorSkill => {
          minorSkill.get('name').disable();
          minorSkill.get('points').setValidators([Validators.max(3),  Validators.min(-1)]);
          this.subscriptionManager.add(minorSkill.get('name').value, minorSkill.get('points').valueChanges.subscribe(() => {
            this.setRemainingPoints(skill);
            skill.get('usedPoints').updateValueAndValidity();
          }));
        });
      });
      this.buildSkills();
      this.loaded = true;
    });
  }
  public print() {
    console.log(this.form)
  }
  public canSave() {
    return this.form ? this.form.valid : false;
  }
  public save() {
    const creature = this.form.getRawValue() as PocketCreature;
    creature.creatureType = CreatureType.Hero;
    this.campaignService.createCreature(this.campaign.id, creature).subscribe(() => {
      this.dialogRef.close();
    });
  }
  private setRemainingPoints(skill: FormGroup): void {
    const value = (skill.get('minorSkills') as FormArray).controls
    .map((control: FormControl) => Number(control.get('points').value))
    .reduce((previous : number, current: number) => current + previous, 0)
    const maxPoints = skill.get('pointsLimit').value;
    skill.get('remainingPoints').setValue(maxPoints - value);
  }
  private buildSkills() {
    this.template.attributes.forEach(attribute => {
      this.skillsMapping.set(attribute.id, new FormArray([]));
    });
    this.template.skills.forEach(skill => {
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

}

const validateSkillValue: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
  const pointsLimit = Number(control.get('pointsLimit').value);
  const usedPoints = Number(control.get('usedPoints').value);
  return usedPoints > pointsLimit ? { invalidPoints: true } : null
}
