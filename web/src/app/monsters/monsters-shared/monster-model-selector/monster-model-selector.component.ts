import {Component, Injector, OnInit} from '@angular/core';
import {BaseSelectorComponent} from 'src/app/shared/base-selector/base-selector/base-selector.component';
import {MonsterModel} from 'src/app/shared/models/MonsterModel.model';
import {MatDialogRef} from '@angular/material/dialog';
import {Router} from '@angular/router';
import {MonstersBaseService} from '../../monsters-bases/monsters-base.service';

@Component({
  selector: 'loh-monster-model-selector',
  templateUrl: './monster-model-selector.component.html',
  styleUrls: ['./monster-model-selector.component.css']
})
export class MonsterBaseSelectorComponent extends BaseSelectorComponent<MonsterModel> implements OnInit {

  constructor(
    injector: Injector,
    protected service: MonstersBaseService,
    protected dialogRef: MatDialogRef<MonsterBaseSelectorComponent>,
    protected router: Router
  ) {
    super(injector, service);
   }

  ngOnInit() {
    this.getAll();
  }

}
