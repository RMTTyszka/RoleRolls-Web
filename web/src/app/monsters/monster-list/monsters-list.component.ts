import {Component, Injector, OnInit} from '@angular/core';
import {Monster} from '../../shared/models/Monster.model';
import {MonsterComponent} from '../monster/monster.component';
import {Router} from '@angular/router';
import {RRColumns} from '../../shared/components/cm-grid/cm-grid.component';
import {DialogService} from 'primeng/dynamicdialog';
import {MonsterCrudService} from '../monster-crud.service';
import {MonsterCreateComponent} from '../monster-create/monster-create.component';

@Component({
  selector: 'loh-monsters-list',
  templateUrl: './monsters-list.component.html',
  styleUrls: ['./monsters-list.component.css'],
  providers: [DialogService]
})
export class MonstersListComponent implements OnInit {
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
    injector: Injector,
    private dialog: DialogService,
    protected service: MonsterCrudService,
    protected router: Router
  ) {
    }

  ngOnInit() {
  }

  openMonsterEditor(monster: Monster) {
    this.dialog.open(MonsterComponent, {
      data: monster
    }
    );
  }
  create = () =>  {
    this.dialog.open(MonsterCreateComponent, {}).onClose.subscribe();
  }
}
