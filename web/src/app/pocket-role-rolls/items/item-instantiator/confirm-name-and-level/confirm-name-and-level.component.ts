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

@Component({
  selector: 'rr-confirm-name-and-level',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    InputTextModule
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
        name: new UntypedFormControl([this.itemTemplate.name, Validators.required]),
        level: new UntypedFormControl([this.instance.data['level'] as number, Validators.required]),
      });
    }
  }
  ngOnDestroy() {
    if (this.ref) {
      this.ref.close();
    }
  }
}
