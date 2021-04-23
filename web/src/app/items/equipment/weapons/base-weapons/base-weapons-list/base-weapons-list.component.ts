import {Component, OnInit} from '@angular/core';
import {BaseWeaponService} from '../base-weapon.service';
import {BaseWeapon} from 'src/app/shared/models/BaseWeapon.model';
import {BaseWeaponsEditorComponent} from '../base-weapons-editor/base-weapons-editor.component';
import {RRColumns} from 'src/app/shared/components/cm-grid/cm-grid.component';
import {DialogService} from 'primeng/dynamicdialog';
import {EditorAction, ModalEntityData} from 'src/app/shared/dtos/ModalEntityData';

@Component({
  selector: 'rr-base-weapons-list',
  templateUrl: './base-weapons-list.component.html',
  styleUrls: ['./base-weapons-list.component.css'],
  providers: [DialogService]
})
export class BaseWeaponsListComponent implements OnInit {
  columns: RRColumns[] = [
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
    this.openEditor(EditorAction.create).onClose.subscribe();
  }

  update(baseWeapon: BaseWeapon) {
    this.openEditor(EditorAction.update, baseWeapon)
      .onClose.subscribe();
  }

  openEditor(action: EditorAction, baseWeapon?: BaseWeapon) {
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
