import { Component } from '@angular/core';
import {DialogService, DynamicDialogComponent, DynamicDialogRef} from 'primeng/dynamicdialog';
import {CampaignItemTemplatesService} from 'src/app/pocket-role-rolls/proxy-services/campaign-item-templates.service';
import {
  PocketCampaignDetailsService
} from 'src/app/pocket-role-rolls/campaigns/CampaignInstance/pocket-campaign-bodyshell/pocket-campaign-details.service';
import {PocketCreature} from 'src/app/shared/models/pocket/creatures/pocket-creature';
import {ItemTemplate} from 'src/app/shared/models/items/ItemTemplate';
import {ItemTemplateModel} from 'src/app/shared/models/pocket/itens/ItemTemplateModel';
import {ReactiveFormsModule, UntypedFormControl, UntypedFormGroup, Validators} from '@angular/forms';
import {InputTextModule} from 'primeng/inputtext';
import {ButtonDirective} from 'primeng/button';
import {InstantiateItemInput} from 'src/app/pocket-role-rolls/items/item-instantiator/models/instantiate-item-input';
import { v4 as uuidv4 } from 'uuid';

@Component({
  selector: 'rr-confirm-name-and-level',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    InputTextModule,
    ButtonDirective
  ],
  templateUrl: './confirm-name-and-level.component.html',
  styleUrl: './confirm-name-and-level.component.scss'
})
export class ConfirmNameAndLevelComponent {

  public form: UntypedFormGroup;
  instance: DynamicDialogComponent | undefined;
  private itemTemplate: ItemTemplateModel;
  constructor(public ref: DynamicDialogRef,
              private dialogService: DialogService,
  ) {
    this.instance = this.dialogService.getInstance(this.ref);
  }
  ngOnInit() {
    if (this.instance && this.instance.data) {
      this.itemTemplate = this.instance.data['itemTemplate'] as ItemTemplateModel;
      this.form =  new UntypedFormGroup({
        id: new UntypedFormControl(uuidv4(), Validators.required),
        name: new UntypedFormControl(this.itemTemplate.name, Validators.required),
        templateId: new UntypedFormControl(this.itemTemplate.id, Validators.required),
        level: new UntypedFormControl(this.instance.data['level'] as number, Validators.required),
      });
    }
  }
  add() {
    const itemInput = this.form.value as InstantiateItemInput;
    this.ref.close(itemInput);
  }
  cancel() {
    this.ref.close();
  }
}
