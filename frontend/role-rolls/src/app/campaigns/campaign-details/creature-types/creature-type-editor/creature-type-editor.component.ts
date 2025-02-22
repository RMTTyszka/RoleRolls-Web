import {Component, input} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule} from '@angular/forms';
import {DynamicDialogConfig} from 'primeng/dynamicdialog';
import {Campaign} from '@app/campaigns/models/campaign';
import {CreatureType} from '@app/models/creatureTypes/creature-type';
import {EditorAction} from '@app/models/ModalEntityData';
import {createForm, getAsForm} from '@app/tokens/EditorExtension';
import {v4 as uuid} from 'uuid';
import {Fieldset} from 'primeng/fieldset';
import {InputText} from 'primeng/inputtext';
import {Textarea} from 'primeng/textarea';
import {NgIf} from '@angular/common';
import {canEdit} from '@app/tokens/utils.funcs';

@Component({
  selector: 'rr-creature-type-editor',
  imports: [
    ReactiveFormsModule,
    Fieldset,
    InputText,
    Textarea,
    NgIf
  ],
  templateUrl: './creature-type-editor.component.html',
  styleUrl: './creature-type-editor.component.scss'
})
export class CreatureTypeEditorComponent {
  public loaded = false;
  public form: FormGroup;
  public get creatureTypeTitle() {
    return this.campaign.campaignTemplate.creatureTypeTitle;
  }
  private campaign: Campaign;
  public creatureType: CreatureType;
  private action: EditorAction = EditorAction.create;
  constructor(private dialogConfig: DynamicDialogConfig) {
    this.campaign = dialogConfig.data.campaign as Campaign;
    this.creatureType = dialogConfig.data.creatureType as CreatureType;
    if (this.creatureType) {
      this.action = EditorAction.update;
    } else {
      this.action = EditorAction.create;
      this.creatureType = {
        id: uuid(),
        name: '',
        bonuses: [],
        description: '',
      };
    }
    this.form = getAsForm(this.creatureType);
    if (!canEdit(this.campaign)) {
      this.form.disable();
    }
    this.loaded = true;
  }
}
