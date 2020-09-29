import {Component, Inject, Injector, OnInit} from '@angular/core';
import {LegacyBaseCreatorComponent} from '../../shared/base-creator/legacy-base-creator.component';
import {Monster} from '../../shared/models/creatures/monsters/Monster.model';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {MonsterBaseSelectorComponent} from '../monsters-shared/monster-model-selector/monster-model-selector.component';
import {MonsterModel} from 'src/app/shared/models/creatures/monsters/MonsterModel.model';
import {MonsterService} from './monster.service';
import {Bonus} from 'src/app/shared/models/Bonus.model';
import {DynamicDialogConfig, DynamicDialogRef} from 'primeng/dynamicdialog';
import {BaseCreatorComponent} from '../../shared/base-creator/base-creator.component';
import {MonsterCreateInput} from '../../shared/models/creatures/monsters/MonsterCreateInput.model';
import {CreatureEditorComponent} from '../../creatures-shared/creature-editor/creature-editor.component';
import {MonsterManagementService} from './monster-management.service';

@Component({
  selector: 'loh-monster',
  templateUrl: './monster.component.html',
  styleUrls: ['./monster.component.css']
})
export class MonsterComponent implements OnInit {
  attributes: string[];
  skills: string[];
  entityId: string;
  constructor(
    public service: MonsterService,
    public monsterManagementService: MonsterManagementService,
    data: DynamicDialogConfig
  ) {
    this.entityId = data.data.entityId;
  }
  ngOnInit() {
  }
}
