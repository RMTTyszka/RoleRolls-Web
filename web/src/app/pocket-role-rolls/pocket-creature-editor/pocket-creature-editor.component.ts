import {Component, OnInit} from "@angular/core";
import {AbstractControl, FormArray, FormGroup, ValidationErrors, ValidatorFn} from "@angular/forms";
import {PocketCampaignModel} from "../../shared/models/pocket/campaigns/pocket.campaign.model";
import {PocketCreature} from "../../shared/models/pocket/creatures/pocket-creature";
import {EditorAction} from "../../shared/dtos/ModalEntityData";
import {CreatureType} from "../../shared/models/creatures/CreatureType";

@Component({
  selector: 'rr-pocket-creature-editor',
  templateUrl: './pocket-creature-editor.component.html',
  styleUrls: ['./pocket-creature-editor.component.scss']
})
export class PocketCreatureEditorComponent implements OnInit {
  public loaded = false;
  public form: FormGroup;
  public campaign: PocketCampaignModel;
  // public template: PocketCreature;
  public creature: PocketCreature;
  public editorAction: EditorAction;
  public creatureType: CreatureType;
  public skillsMapping = new Map<string, FormArray>();
  public minorsSkillBySkill = new Map<string, FormArray>();
  public creatureId: string;

  public canSave() {
    return this.form ? this.form.valid : false;
  }
  public save() {
    return this.form ? this.form.valid : false;
  }
  ngOnInit(): void {
  }
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
}

const validateSkillValue: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
  const pointsLimit = Number(control.get('pointsLimit').value);
  const usedPoints = Number(control.get('usedPoints').value);
  return usedPoints > pointsLimit ? { invalidPoints: true } : null
}
