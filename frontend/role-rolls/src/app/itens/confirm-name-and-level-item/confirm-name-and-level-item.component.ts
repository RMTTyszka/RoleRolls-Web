import { Component } from '@angular/core';
import { ReactiveFormsModule, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { InputText } from 'primeng/inputtext';
import { ButtonDirective } from 'primeng/button';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { InstantiateItemInput } from '@app/models/itens/instances/instantiate-item-input';
import { ItemTemplateModel } from '@app/models/itens/ItemTemplateModel';
import { InputGroupAddonModule } from 'primeng/inputgroupaddon';

@Component({
  selector: 'rr-confirm-name-and-level-item',
  imports: [
    ReactiveFormsModule,
    InputText,
    ButtonDirective,
    InputGroupAddonModule
  ],
  templateUrl: './confirm-name-and-level-item.component.html',
  styleUrl: './confirm-name-and-level-item.component.scss'
})
export class ConfirmNameAndLevelItemComponent {

  public form: UntypedFormGroup;
  private itemTemplate: ItemTemplateModel;
  constructor(public ref: DynamicDialogRef,
              public config: DynamicDialogConfig
  ) {
  }
  ngOnInit() {
    if (this.config?.data) {
      this.itemTemplate = this.config.data['itemTemplate'] as ItemTemplateModel;
      this.form =  new UntypedFormGroup({
        id: new UntypedFormControl(crypto.randomUUID(), Validators.required),
        name: new UntypedFormControl(this.itemTemplate.name, Validators.required),
        templateId: new UntypedFormControl(this.itemTemplate.id, Validators.required),
        level: new UntypedFormControl(this.config.data['level'] as number, Validators.required),
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
