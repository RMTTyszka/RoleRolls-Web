import { Component } from '@angular/core';
import { ReactiveFormsModule, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { InputText } from 'primeng/inputtext';
import { ButtonDirective } from 'primeng/button';
import { DialogService, DynamicDialogComponent, DynamicDialogRef } from 'primeng/dynamicdialog';
import { InstantiateItemInput } from '@app/models/itens/instances/instantiate-item-input';
import { ItemTemplateModel } from '@app/models/itens/ItemTemplateModel';

@Component({
  selector: 'rr-confirm-name-and-level-item',
  imports: [
    ReactiveFormsModule,
    InputText,
    ButtonDirective
  ],
  templateUrl: './confirm-name-and-level-item.component.html',
  styleUrl: './confirm-name-and-level-item.component.scss'
})
export class ConfirmNameAndLevelItemComponent {

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
        id: new UntypedFormControl(crypto.randomUUID(), Validators.required),
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
