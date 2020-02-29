import {Component, Injector, OnInit} from '@angular/core';
import {MonstersService} from '../monsters.service';
import {BaseListComponent} from '../../shared/base-list/base-list.component';
import {DataService} from '../../shared/data.service';
import {Monster} from '../../shared/models/Monster.model';
import {MatDialogConfig} from '@angular/material/dialog';
import {MonsterComponent} from '../monster/monster.component';
import {Router} from '@angular/router';

@Component({
  selector: 'loh-monsters-list',
  templateUrl: './monsters-list.component.html',
  styleUrls: ['./monsters-list.component.css']
})
export class MonstersListComponent extends BaseListComponent<Monster> implements OnInit {

  attributes: string[];
  skills: string[];

  constructor(
    injector: Injector,
    private dataService: DataService,
    protected service: MonstersService,
    protected router: Router
  ) {
    super(injector, service);
    this.editor = MonsterComponent;
    }

  ngOnInit() {
    this.getAll();
    this.dataService.getAllAttributes().subscribe(data => this.attributes = data);
    this.dataService.getAllSkills().subscribe(data => this.skills = data);
  }

  openMonsterEditor(monster: Monster) {
    const dialogRef =  this.dialog.open(MonsterComponent, <MatDialogConfig> {
      data: monster
    }
    );
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.getAll();
      } else {
        return;
      }
    });
  }
  create () {
    const dialogRef =  this.dialog.open(MonsterComponent);
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.getAll();
      } else {
        return;
      }
    });
  }
}
