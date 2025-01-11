import { Component, ViewEncapsulation } from '@angular/core';
import { EditorAction } from "../../models/ModalEntityData";
import { Checkbox } from "primeng/checkbox";
import { Fieldset } from "primeng/fieldset";
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from "@angular/forms";
import { InputText } from "primeng/inputtext";
import { NgIf } from "@angular/common";
import { Select } from "primeng/select";
import { RROption } from '../../models/RROption';
import {v4 as uuidv4} from 'uuid';
import { getAsForm } from '../../tokens/EditorExtension';
import { CampaignCreatorControls } from './campaign-creator-form-controls';
import { AuthenticationService } from '../../authentication/services/authentication.service';
import { ButtonDirective } from 'primeng/button';
import { Toolbar } from 'primeng/toolbar';

@Component({
  selector: 'rr-campaign-creator',
  imports: [
    Checkbox,
    FormsModule,
    InputText,
    ReactiveFormsModule,
    Select,
    ButtonDirective,
    Toolbar
  ],
  templateUrl: './campaign-creator.component.html',
  styleUrl: './campaign-creator.component.scss',
  encapsulation: ViewEncapsulation.None,
})
export class CampaignCreatorComponent {

  public formControls = new CampaignCreatorControls();
  public defaultTemplates = [
    {
      value: '985C54E0-C742-49BC-A3E0-8DD2D6CE2632',
      label: 'Land of Heroes',
    } as RROption<string>,
    {
      value: '',
      label: 'None',
    } as RROption<string>,
  ];
  public form = getAsForm(this.formControls, [this.formControls.id, this.formControls.name]);
  constructor(private readonly authenticationService: AuthenticationService) {
    this.form.get(this.formControls.name).setValue('New Campaign by ' + this.authenticationService.userName);
    this.form.get(this.formControls.campaignTemplateId).setValue(this.defaultTemplates[1].value);
  }
}
