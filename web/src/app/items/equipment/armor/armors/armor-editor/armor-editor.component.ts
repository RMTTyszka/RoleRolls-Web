import { Component, OnInit } from '@angular/core';
import {ModalEntityAction} from '../../../../../shared/dtos/ModalEntityData';
import {FormGroup} from '@angular/forms';
import {DynamicDialogConfig, DynamicDialogRef} from 'primeng/api';
import {ArmorService} from '../armor.service';
import { ArmorModel } from 'src/app/shared/models/ArmorModel.model';

@Component({
  selector: 'loh-armor-editor',
  templateUrl: './armor-editor.component.html',
  styleUrls: ['./armor-editor.component.css']
})
export class ArmorEditorComponent implements OnInit {
  entity: ArmorModel;
  action: ModalEntityAction;
  form: FormGroup = new FormGroup({});
  isLoading = true;
  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    public service: ArmorService,
  ) {
    this.action = config.data.action;
    if (config.data.action === ModalEntityAction.create) {
      this.entity = new ArmorModel();
    } else {
      this.entity = config.data.entity;
    }
  }

  ngOnInit() {
  }
  loaded(hasLoaded) {
    this.isLoading = !hasLoaded;
  }

  saved(armor: ArmorModel) {
    this.ref.close(armor);
  }
  deleted(armor: ArmorModel) {
    this.ref.close(armor);
  }
}
