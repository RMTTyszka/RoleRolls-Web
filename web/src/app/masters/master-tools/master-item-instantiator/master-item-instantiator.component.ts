import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {DynamicDialogConfig, DynamicDialogRef} from 'primeng/dynamicdialog';
import {UpdateCreatureToolService} from '../update-creature-tool/update-creature-tool.service';
import {MasterInstantiateItemActionInput} from '../../../shared/models/inputs/MasterInstantiateItemActionInput';
import {Creature} from '../../../shared/models/creatures/Creature.model';
import {ItemTemplate} from '../../../shared/models/items/ItemTemplate';

export class ItemInstantiatorModalData {
  combatId: string;
  campaignId: string;
  creatureId: string;
}

@Component({
  selector: 'loh-master-item-instantiator',
  templateUrl: './master-item-instantiator.component.html',
  styleUrls: ['./master-item-instantiator.component.css']
})
export class MasterItemInstantiatorComponent implements OnInit {
  form: FormGroup;
  config: ItemInstantiatorModalData
  constructor(
    private formBuilder: FormBuilder,
    private dialogRef: DynamicDialogRef,
    private dialogConfig: DynamicDialogConfig,
    private masterToolService: UpdateCreatureToolService
  ) {
    this.config = this.dialogConfig.data;
    this.createForm();
  }

  ngOnInit(): void {
  }

  private createForm() {
    this.form = this.formBuilder.group({
      itemTemplate: [null, [Validators.required]],
      level: [1, [Validators.required]],
      quantity: [1, [Validators.required]]
    });
  }

  save() {
    if (this.canSave()) {
      const input = this.buildInput();
      this.masterToolService.instantiateItem(this.config.combatId, this.config.creatureId, input)
        .subscribe((creature: Creature) => {
          this.dialogRef.close(creature);
        });
    }
  }

  private buildInput(): MasterInstantiateItemActionInput {
    const input = new MasterInstantiateItemActionInput();
    input.itemTemplateId = (this.form.get('itemTemplate').value as ItemTemplate).id;
    input.level = this.form.get('level').value;
    input.quantity = this.form.get('quantity').value;
    return input;
  }

  canSave() {
    return this.form.valid && this.form.dirty;
  }

  cancel() {
    this.dialogRef.close(false);
  }
}
