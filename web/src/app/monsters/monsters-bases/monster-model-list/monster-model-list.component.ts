import {Component, Injector, OnInit} from '@angular/core';
import {MonsterModelComponent} from '../monster-model-editor/monster-model.component';
import {Router} from '@angular/router';
import {MonsterModelsService} from '../monster-template-provider/monster-models.service';
import {DialogService} from 'primeng/dynamicdialog';
import {MonsterModelConfig} from '../monster-model-config';

@Component({
  selector: 'rr-monster-model-list',
  templateUrl: './monster-model-list.component.html',
  styleUrls: ['./monster-model-list.component.scss'],
  providers: [DialogService]
})
export class MonsterBaseListComponent implements OnInit {
  config = new MonsterModelConfig();
  constructor(
    public service: MonsterModelsService,
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
