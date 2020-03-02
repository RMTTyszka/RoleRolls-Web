import { Component, OnInit } from '@angular/core';
import { BaseWeaponService } from '../base-weapon.service';
import { BaseWeapon } from 'src/app/shared/models/BaseWeapon.model';
import { ModalEntityAction } from 'src/app/shared/dtos/ModalEntityData';
import { FormGroup } from '@angular/forms';
import { DynamicDialogRef, DynamicDialogConfig } from 'primeng/api';
import { BaseArmor } from 'src/app/shared/models/BaseArmor.model';

@Component({
  selector: 'loh-base-weapons-editor',
  templateUrl: './base-weapons-editor.component.html',
  styleUrls: ['./base-weapons-editor.component.css']
})
export class BaseWeaponsEditorComponent implements OnInit {
  entity: BaseWeapon;
  action: ModalEntityAction;
  form: FormGroup = new FormGroup({});
  isLoading = true;
  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    public service: BaseWeaponService,
  ) {
    this.action = config.data.action;
    if (config.data.action === ModalEntityAction.create) {
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

  saved(baseWeapon: BaseArmor) {
    this.ref.close(baseWeapon);
  }
  deleted(baseWeapon: BaseWeapon) {
    this.ref.close(baseWeapon);
  }
}
