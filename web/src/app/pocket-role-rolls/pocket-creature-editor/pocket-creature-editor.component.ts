import { Component, OnInit } from '@angular/core';
import { FormArray, FormGroup } from '@angular/forms';
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

@Component({
  selector: 'rr-pocket-creature-editor',
  templateUrl: './pocket-creature-editor.component.html',
  styleUrls: ['./pocket-creature-editor.component.scss']
})
export class PocketCreatureEditorComponent implements OnInit {

  public loaded = false;
  public form: FormGroup;
  public campaign: PocketCampaignModel;
  public template: CreatureTemplateModel;
  public creature: PocketCreature;
  public editorAction: EditorAction;
  public skillsMapping = new Map<string, FormArray>();
  public minorsSkillBySkill = new Map<string, FormArray>();
  public minorSkillProficiences = [
    {
      name: 'Crap',
      value: PocketSkillProficience.Crap
    },
    {
      name: 'Bad',
      value: PocketSkillProficience.Bad
    },
    {
      name: 'Normal',
      value: PocketSkillProficience.Normal
    },
    {
      name: 'Good',
      value: PocketSkillProficience.Good
    },
    {
      name: 'Expert',
      value: PocketSkillProficience.Expert
    },
  ];

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
  constructor(
    private campaignService: PocketCampaignsService,
    public config: DynamicDialogConfig,
    public dialogRef: DynamicDialogRef,

  ) {
    this.campaign = config.data.campaign;
    this.editorAction = config.data.action;
   }

  ngOnInit(): void {
    this.campaignService.getCreatureTemplate(this.campaign.creatureTemplateId)
    .pipe(tap((template => {
      const creature = template as unknown as PocketCreature;
      if (this.editorAction === EditorAction.create) {
        creature.attributes.forEach(attribute => {
          attribute.value = 0;
        });
        creature.skills.forEach(skill => {
          skill.value = 0;
          skill.minorSkills.forEach(minorSkill => {
            minorSkill.skillProficience = PocketSkillProficience.Normal;
          });
        });
        creature.name = '';
      }
      this.creature = creature;
      return template;
    }))).subscribe((template: CreatureTemplateModel) => {
      this.template = template;
      this.form = getAsForm(this.creature);
      this.attributes.controls.forEach(attribute => {
        attribute.get('name').disable();
      });
      this.skills.controls.forEach(skill => {
        skill.get('name').disable();
        (skill.get('minorSkills') as FormArray).controls.forEach(minorSkill => {
          minorSkill.get('name').disable();
        });
      });
      this.buildSkills();
      this.loaded = true;
    });
  }
  public save() {
    const creature = this.form.value as PocketCreature;
    creature.creatureType = CreatureType.Hero;
    this.campaignService.createCreature(this.campaign.id, creature).subscribe(() => {
      this.dialogRef.close();
    });
  }

  private buildSkills() {
    this.template.attributes.forEach(attribute => {
      this.skillsMapping.set(attribute.id, new FormArray([]));
    });
    this.template.skills.forEach(skill => {
      this.skillsMapping.get(skill.attributeId).push(getAsForm(skill));
      this.minorsSkillBySkill.set(skill.id, new FormArray([]));
      const minorSkills = this.minorsSkillBySkill.get(skill.id);
      skill.minorSkills.forEach(minorSkill => {
        minorSkills.push(getAsForm(minorSkill));
      });
    });
  }

}
