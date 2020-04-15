import {Component, OnInit} from '@angular/core';
import {CmColumns} from 'src/app/shared/components/cm-grid/cm-grid.component';
import {DialogService} from 'primeng/api';
import {WeaponModelService} from '../weapon-model.service';
import {ModalEntityAction, ModalEntityData} from 'src/app/shared/dtos/ModalEntityData';
import {WeaponModel} from 'src/app/shared/models/WeaponModel.model';
import {WeaponModelEditorComponent} from '../weapon-model-editor/weapon-model-editor.component';

@Component({
  selector: 'loh-weapon-model-list',
  templateUrl: './weapon-model-list.component.html',
  styleUrls: ['./weapon-model-list.component.css'],
  providers: [DialogService]
})
export class WeaponModelListComponent implements OnInit {
  columns: CmColumns[] = [
    {
      header: 'Name',
      property: 'name'
    },    {
      header: 'Type',
      property: 'baseWeapon.category.weaponType'
    },    {
      header: 'Hit Attribute',
      property: 'baseWeapon.hitAttribute'
    },    {
      header: 'Damange Attribute',
      property: 'baseWeapon.damageAttribute'
    },
  ];

  constructor(
    public dialog: DialogService,
    public service: WeaponModelService
  ) {
  }

  ngOnInit() {
  }

  create() {
    this.openEditor(ModalEntityAction.create).onClose.subscribe();
  }

  update(weapon: WeaponModel) {
    this.openEditor(ModalEntityAction.update, weapon)
      .onClose.subscribe();
  }

  openEditor(action: ModalEntityAction, weapon?: WeaponModel) {
    return this.dialog.open(WeaponModelEditorComponent, {
      data: <ModalEntityData<WeaponModel>> {
        entity: weapon,
        action: action
      }
    });
  }

  weaponSelected(weapon: WeaponModel) {
    this.update(weapon);
  }
}
