import {Component, OnInit} from '@angular/core';
import {BaseArmor} from '../../../../../shared/models/BaseArmor.model';
import {BaseArmorService} from '../base-armor.service';
import {RRColumns} from '../../../../../shared/components/cm-grid/cm-grid.component';
import {BaseArmorEditorComponent} from '../base-armor-editor/base-armor-editor.component';
import {EditorAction, ModalEntityData} from '../../../../../shared/dtos/ModalEntityData';
import {DialogService} from 'primeng/dynamicdialog';

@Component({
  selector: 'loh-base-armor-list',
  templateUrl: './base-armor-list.component.html',
  styleUrls: ['./base-armor-list.component.css'],
  providers: [DialogService]
})
export class BaseArmorListComponent implements OnInit {
  columns: RRColumns[] = [
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
    this.openEditor(EditorAction.create).onClose.subscribe();
  }

  update(baseArmor: BaseArmor) {
    this.openEditor(EditorAction.update, baseArmor)
      .onClose.subscribe();
  }

  openEditor(action: EditorAction, baseArmor?: BaseArmor) {
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
