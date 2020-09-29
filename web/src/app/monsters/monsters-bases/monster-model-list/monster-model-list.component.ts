import {Component, Injector, OnInit} from '@angular/core';
import {LegacyBaseListComponent} from 'src/app/shared/base-list/legacy-base-list.component';
import {MonsterModel} from 'src/app/shared/models/creatures/monsters/MonsterModel.model';
import {MonsterModelComponent} from '../monster-model-editor/monster-model.component';
import {Router} from '@angular/router';
import {DeviceDetectorService} from 'ngx-device-detector';
import {LegacyMonstersBaseService} from '../legacy-monsters-base.service';
import {MonsterModelsService} from '../monster-models.service';
import {DialogService} from 'primeng/dynamicdialog';
import {MonsterModelConfig} from '../monster-model-config';

@Component({
  selector: 'loh-monster-model-list',
  templateUrl: './monster-model-list.component.html',
  styleUrls: ['./monster-model-list.component.scss'],
  providers: [DialogService]
})
export class MonsterBaseListComponent implements OnInit {
  config = new MonsterModelConfig();
  constructor(
    protected service: MonsterModelsService,
    protected dialog: DialogService,
    protected router: Router,
  ) {
   }

  ngOnInit() {
  }
  create = () => {
    this.dialog.open(MonsterModelComponent, {}).onClose.subscribe();
  }

  //   this.service.changeEntity(monsterBase);
  //   let dialogRef: MatDialogRef<any>;
  //   if (this.deviceDetector.isMobile() || this.deviceDetector.isTablet()) {
  //   dialogRef = this.dialog.open(this.editor, {
  //       maxWidth: '100vw',
  //       maxHeight: '100vh',
  //       height: '100%',
  //       width: '100%',
  //       data: monsterBase || new MonsterModel()
  //   });
  // } else {
  //   dialogRef = this.dialog.open(this.editor, {
  //     data: monsterBase || new MonsterModel(),
  //     maxWidth: '900px'
  // });
  // }
  // dialogRef.afterClosed().subscribe(response => this.getAll());
}
