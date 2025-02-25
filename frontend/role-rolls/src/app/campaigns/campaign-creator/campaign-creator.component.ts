import { Component, ViewEncapsulation } from '@angular/core';
import { EditorAction } from "../../models/EntityActionData";
import { Checkbox } from "primeng/checkbox";
import { Fieldset } from "primeng/fieldset";
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from "@angular/forms";
import { InputText } from "primeng/inputtext";
import { NgIf } from "@angular/common";
import { Select } from "primeng/select";
import { RROption } from '../../models/RROption';
import {v4 as uuidv4} from 'uuid';
import { getAsForm, ultraPatchValue } from '../../tokens/EditorExtension';
import { CampaignCreatorControls } from './campaign-creator-form-controls';
import { AuthenticationService } from '../../authentication/services/authentication.service';
import { ButtonDirective } from 'primeng/button';
import { Toolbar } from 'primeng/toolbar';
import { CampaignsService } from '../services/campaigns.service';
import { firstValueFrom } from 'rxjs';
import { Router } from '@angular/router';
import { Campaign } from '../models/campaign';

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
      value: '',
      label: 'None',
    } as RROption<string>,
    {
      value: '985C54E0-C742-49BC-A3E0-8DD2D6CE2632',
      label: 'Land of Heroes',
    } as RROption<string>,

  ];
  public form = getAsForm(this.formControls, true, [this.formControls.id, this.formControls.name]);
  constructor(private readonly authenticationService: AuthenticationService,
              private readonly campaignsService: CampaignsService,
              private readonly router: Router,
              ) {
    ultraPatchValue(this.form, {
      id: uuidv4(),
      campaignTemplateId: this.defaultTemplates[0].value,
      copy: false,
      name: 'New Campaign by ' + this.authenticationService.userName,
      masterId: this.authenticationService.userId,
    } as Campaign)
  }


  public async save() {
    const campaign = this.form.value;
    await firstValueFrom(this.campaignsService.create(campaign));
    this.router.navigate(['/campaigns/' + this.form.value.id])
  }
}
