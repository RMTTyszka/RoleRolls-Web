import {Component, OnInit} from '@angular/core';
import {BaseWeaponService} from '../base-weapon.service';
import {BaseWeapon} from 'src/app/shared/models/BaseWeapon.model';
import {BaseWeaponsEditorComponent} from '../base-weapons-editor/base-weapons-editor.component';
import {CmColumns} from 'src/app/shared/components/cm-grid/cm-grid.component';
import {DialogService} from 'primeng/dynamicdialog';
import {ModalEntityAction, ModalEntityData} from 'src/app/shared/dtos/ModalEntityData';

@Component({
  selector: 'loh-base-weapons-list',
  templateUrl: './base-weapons-list.component.html',
  styleUrls: ['./base-weapons-list.component.css'],
  providers: [DialogService]
})
export class BaseWeaponsListComponent implements OnInit {
  columns: CmColumns[] = [
    {
      header: 'Name',
      property: 'name'
    },
    {
      header: 'Weapon Type',
      property: 'category.weaponType'
    },
    {
      header: 'Hit Attribute',
      property: 'hitAttribute'
    },
    {
      header: 'Damage Attribute',
      property: 'damageAttribute'
    }
  ];

  constructor(
    public dialog: DialogService,
    public service: BaseWeaponService
  ) {
  }

  ngOnInit() {
  }

  create() {
    this.openEditor(ModalEntityAction.create).onClose.subscribe();
  }

  update(baseWeapon: BaseWeapon) {
    this.openEditor(ModalEntityAction.update, baseWeapon)
      .onClose.subscribe();
  }

  openEditor(action: ModalEntityAction, baseWeapon?: BaseWeapon) {
    return this.dialog.open(BaseWeaponsEditorComponent, {
      data: <ModalEntityData<BaseWeapon>> {
        entity: baseWeapon,
        action: action
      },
      header: 'Base Weapon'
    });
  }

  baseWeaponSelected(baseWeapon: BaseWeapon) {
    this.update(baseWeapon);
  }
}
