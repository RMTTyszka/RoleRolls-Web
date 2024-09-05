import {Component, Injector, OnInit} from '@angular/core';
import {LegacyBaseSelectorComponent} from 'src/app/shared/legacy-base-selector/legacy-base-selector.component';
import {MonsterModel} from 'src/app/shared/models/creatures/monsters/MonsterModel.model';
import {MatLegacyDialogRef as MatDialogRef} from '@angular/material/legacy-dialog';
import {Router} from '@angular/router';
import {LegacyMonstersBaseService} from '../../monsters-bases/legacy-monsters-base.service';

@Component({
  selector: 'rr-monster-model-selector',
  templateUrl: './monster-model-selector.component.html',
  styleUrls: ['./monster-model-selector.component.css']
})
export class MonsterBaseSelectorComponent extends LegacyBaseSelectorComponent<MonsterModel> implements OnInit {

  constructor(
    injector: Injector,
    protected service: LegacyMonstersBaseService,
    protected dialogRef: MatDialogRef<MonsterBaseSelectorComponent>,
    protected router: Router
  ) {
    super(injector, service);
   }

  ngOnInit() {
    this.getAll();
  }

}
