import {Component, OnInit} from '@angular/core';
import {BaseWeaponService} from '../base-weapon.service';
import {BaseWeapon} from 'src/app/shared/models/BaseWeapon.model';
import {EditorAction} from 'src/app/shared/dtos/ModalEntityData';
import {FormGroup} from '@angular/forms';
import {DynamicDialogConfig, DynamicDialogRef} from 'primeng/dynamicdialog';

@Component({
  selector: 'rr-base-weapons-editor',
  templateUrl: './base-weapons-editor.component.html',
  styleUrls: ['./base-weapons-editor.component.css']
})
export class BaseWeaponsEditorComponent implements OnInit {
  entity: BaseWeapon;
  action: EditorAction;
  form: FormGroup = new FormGroup({});
  isLoading = true;
  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    public service: BaseWeaponService,
  ) {
    this.action = config.data.action;
    if (config.data.action === EditorAction.create) {
      this.entity = new BaseWeapon();
    } else {
      this.entity = config.data.entity;
    }
  }

  ngOnInit() {
  }
  loaded(hasLoaded) {
    this.isLoading = !hasLoaded;
  }

  saved(baseWeapon: BaseWeapon) {
    this.ref.close(baseWeapon);
  }
  deleted(baseWeapon: BaseWeapon) {
    this.ref.close(baseWeapon);
  }
}
