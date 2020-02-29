import {Component, Injector, OnInit} from '@angular/core';
import {BaseListComponent} from 'src/app/shared/base-list/base-list.component';
import {MonsterModel} from 'src/app/shared/models/MonsterModel.model';
import {MonsterModelComponent} from '../monster-model-editor/monster-model.component';
import {Router} from '@angular/router';
import {DeviceDetectorService} from 'ngx-device-detector';
import {MonstersBaseService} from '../monsters-base.service';

@Component({
  selector: 'loh-monster-model-list',
  templateUrl: './monster-model-list.component.html',
  styleUrls: ['./monster-model-list.component.css']
})
export class MonsterBaseListComponent extends BaseListComponent<MonsterModel> implements OnInit {

  constructor(
    injector: Injector,
    protected service: MonstersBaseService,
    protected router: Router,
    private deviceDetector: DeviceDetectorService
  ) {
    super(injector, service);
    this.editor = MonsterModelComponent;
   }

  ngOnInit() {
    this.getAll();
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
