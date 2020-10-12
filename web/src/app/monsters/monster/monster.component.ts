import {Component, OnInit, ViewEncapsulation} from '@angular/core';
import {MonsterService} from './monster.service';
import {DynamicDialogConfig} from 'primeng/dynamicdialog';
import {MonsterManagementService} from './monster-management.service';
import {CreatureType} from '../../shared/models/creatures/CreatureType';

@Component({
  selector: 'loh-monster',
  templateUrl: './monster.component.html',
  styleUrls: ['./monster.component.scss']
})
export class MonsterComponent implements OnInit {
  attributes: string[];
  skills: string[];
  entityId: string;
  creatureType = CreatureType.Monster;
  constructor(
    public service: MonsterService,
    public monsterManagementService: MonsterManagementService,
    data: DynamicDialogConfig
  ) {
    console.log(data);
    this.entityId = data.data.entityId;
  }
  ngOnInit() {
  }
}
