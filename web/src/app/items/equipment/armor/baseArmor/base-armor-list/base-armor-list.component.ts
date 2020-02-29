import {Component, OnInit} from '@angular/core';
import {BaseArmor} from '../../../../../shared/models/BaseArmor.model';
import {BaseArmorService} from '../base-armor.service';
import {CmColumns} from '../../../../../shared/components/cm-grid/cm-grid.component';
import {BaseArmorEditorComponent} from '../base-armor-editor/base-armor-editor.component';
import {ModalEntityAction, ModalEntityData} from '../../../../../shared/dtos/ModalEntityData';
import {DialogService} from 'primeng/api';

@Component({
  selector: 'loh-base-armor-list',
  templateUrl: './base-armor-list.component.html',
  styleUrls: ['./base-armor-list.component.css'],
  providers: [DialogService]
})
export class BaseArmorListComponent implements OnInit {
  columns: CmColumns[] = [
    {
      header: 'Name',
      property: 'name'
    },    {
      header: 'Defense',
      property: 'category.defense'
    },    {
      header: 'Evasion',
      property: 'category.evasion'
    },    {
      header: 'Base Defense',
      property: 'category.baseDefense'
    },
  ];

  constructor(
    public dialog: DialogService,
    public service: BaseArmorService
  ) {
  }

  ngOnInit() {
  }

  create() {
    this.openEditor(ModalEntityAction.create).onClose.subscribe();
  }

  update(baseArmor: BaseArmor) {
    this.openEditor(ModalEntityAction.update, baseArmor)
      .onClose.subscribe();
  }

  openEditor(action: ModalEntityAction, baseArmor?: BaseArmor) {
    return this.dialog.open(BaseArmorEditorComponent, {
      data: <ModalEntityData<BaseArmor>> {
        entity: baseArmor,
        action: action
      }
    });
  }

  baseArmorSelected(baseArmor: BaseArmor) {
    this.update(baseArmor);
  }
}
