import { Component, OnInit } from '@angular/core';
import {CmColumns} from '../../../../../shared/components/cm-grid/cm-grid.component';
import {DialogService} from 'primeng/api';
import {ModalEntityAction, ModalEntityData} from '../../../../../shared/dtos/ModalEntityData';
import {ArmorModel} from '../../../../../shared/models/Armor.model';
import {ArmorEditorComponent} from '../armor-editor/armor-editor.component';
import {ArmorService} from '../armor.service';

@Component({
  selector: 'loh-armor-list',
  templateUrl: './armor-list.component.html',
  styleUrls: ['./armor-list.component.css'],
  providers: [DialogService]
})
export class ArmorListComponent implements OnInit {
  columns: CmColumns[] = [
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
    public service: ArmorService
  ) {
  }

  ngOnInit() {
  }

  create() {
    this.openEditor(ModalEntityAction.create).onClose.subscribe();
  }

  update(armor: ArmorModel) {
    this.openEditor(ModalEntityAction.update, armor)
      .onClose.subscribe();
  }

  openEditor(action: ModalEntityAction, armor?: ArmorModel) {
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
