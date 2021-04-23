import {Component, OnInit} from '@angular/core';
import {RRColumns} from '../../../../../shared/components/cm-grid/cm-grid.component';
import {DialogService} from 'primeng/dynamicdialog';
import {EditorAction, ModalEntityData} from '../../../../../shared/dtos/ModalEntityData';
import {ArmorEditorComponent} from '../armor-editor/armor-editor.component';
import {ArmorTemplateService} from '../armor-template.service';
import {ArmorModel} from 'src/app/shared/models/items/ArmorModel.model';

@Component({
  selector: 'rr-armor-list',
  templateUrl: './armor-list.component.html',
  styleUrls: ['./armor-list.component.css'],
  providers: [DialogService]
})
export class ArmorListComponent implements OnInit {
  columns: RRColumns[] = [
    {
      header: 'Name',
      property: 'name'
    },    {
      header: 'Defense',
      property: 'baseArmor.category.defense'
    },    {
      header: 'Evasion',
      property: 'baseArmor.category.evasion'
    },    {
      header: 'Base Defense',
      property: 'baseArmor.category.baseDefense'
    },
  ];

  constructor(
    public dialog: DialogService,
    public service: ArmorTemplateService
  ) {
  }

  ngOnInit() {
  }

  create() {
    this.openEditor(EditorAction.create).onClose.subscribe();
  }

  update(armor: ArmorModel) {
    this.openEditor(EditorAction.update, armor)
      .onClose.subscribe();
  }

  openEditor(action: EditorAction, armor?: ArmorModel) {
    return this.dialog.open(ArmorEditorComponent, {
      data: <ModalEntityData<ArmorModel>> {
        entity: armor,
        action: action
      }
    });
  }

  armorSelected(armor: ArmorModel) {
    this.update(armor);
  }
}
