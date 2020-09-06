import {Component, OnInit} from '@angular/core';
import {EditorAction} from '../../../../../shared/dtos/ModalEntityData';
import {FormGroup} from '@angular/forms';
import {DynamicDialogConfig, DynamicDialogRef} from 'primeng/dynamicdialog';
import {ArmorTemplateService} from '../armor-template.service';
import {ArmorModel} from 'src/app/shared/models/items/ArmorModel.model';

@Component({
  selector: 'loh-armor-editor',
  templateUrl: './armor-editor.component.html',
  styleUrls: ['./armor-editor.component.css']
})
export class ArmorEditorComponent implements OnInit {
  entity: ArmorModel;
  action: EditorAction;
  form: FormGroup = new FormGroup({});
  isLoading = true;
  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    public service: ArmorTemplateService,
  ) {
    this.action = config.data.action;
    if (config.data.action === EditorAction.create) {
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
