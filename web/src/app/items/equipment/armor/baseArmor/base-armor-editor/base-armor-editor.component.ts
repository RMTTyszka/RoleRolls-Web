import {Component, OnInit} from '@angular/core';
import {BaseArmor} from '../../../../../shared/models/BaseArmor.model';
import {BaseArmorService} from '../base-armor.service';
import {EditorAction} from '../../../../../shared/dtos/ModalEntityData';
import {FormGroup} from '@angular/forms';
import {DynamicDialogConfig, DynamicDialogRef} from 'primeng/dynamicdialog';

@Component({
  selector: 'loh-base-armor-editor',
  templateUrl: './base-armor-editor.component.html',
  styleUrls: ['./base-armor-editor.component.css']
})
export class BaseArmorEditorComponent implements OnInit {
  entity: BaseArmor;
  action: EditorAction;
  form: FormGroup = new FormGroup({});
  isLoading = true;
  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    public service: BaseArmorService,
  ) {
    this.action = config.data.action;
    if (config.data.action === EditorAction.create) {
      this.entity = new BaseArmor();
    } else {
      this.entity = config.data.entity;
    }
  }

  ngOnInit() {
  }
  loaded(hasLoaded) {
    this.isLoading = !hasLoaded;
  }

  saved(baseArmor: BaseArmor) {
    this.ref.close(baseArmor);
  }
  deleted(baseArmor: BaseArmor) {
    this.ref.close(baseArmor);
  }
}
