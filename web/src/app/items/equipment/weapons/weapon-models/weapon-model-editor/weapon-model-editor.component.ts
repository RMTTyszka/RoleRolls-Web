import {Component, OnInit} from '@angular/core';
import {WeaponModel} from 'src/app/shared/models/WeaponModel.model';
import {ModalEntityAction} from 'src/app/shared/dtos/ModalEntityData';
import {FormGroup} from '@angular/forms';
import {DynamicDialogConfig, DynamicDialogRef} from 'primeng/api';
import {WeaponModelService} from '../weapon-model.service';

@Component({
  selector: 'loh-weapon-model-editor',
  templateUrl: './weapon-model-editor.component.html',
  styleUrls: ['./weapon-model-editor.component.css']
})
export class WeaponModelEditorComponent implements OnInit {
  entity: WeaponModel;
  action: ModalEntityAction;
  form: FormGroup = new FormGroup({});
  isLoading = true;
  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    public service: WeaponModelService,
  ) {
    this.action = config.data.action;
    if (config.data.action === ModalEntityAction.create) {
      this.entity = new WeaponModel();
    } else {
      this.entity = config.data.entity;
    }
  }

  ngOnInit() {
  }
  loaded(hasLoaded) {
    this.isLoading = !hasLoaded;
  }

  saved(weapon: WeaponModel) {
    this.ref.close(weapon);
  }
  deleted(weapon: WeaponModel) {
    this.ref.close(weapon);
  }
}
