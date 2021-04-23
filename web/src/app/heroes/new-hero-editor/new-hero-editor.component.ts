import {Component, OnInit} from '@angular/core';
import {HeroesService} from '../heroes.service';
import {MessageService} from 'primeng/api';
import {EditorAction} from '../../shared/dtos/ModalEntityData';
import {FormGroup} from '@angular/forms';
import {DataService} from '../../shared/data.service';
import {Hero} from '../../shared/models/NewHero.model';
import {take} from 'rxjs/operators';
import {Race} from '../../shared/models/Race.model';
import {Role} from '../../shared/models/Role.model';
import {Bonus} from '../../shared/models/Bonus.model';
import {DynamicDialogConfig, DynamicDialogRef} from 'primeng/dynamicdialog';
import {HeroFundsService} from '../../creatures-shared/hero-funds/hero-funds.service';
import {HeroManagementService} from '../hero-management.service';
import {CreatureType} from '../../shared/models/creatures/CreatureType';

@Component({
  selector: 'rr-new-hero-editor',
  templateUrl: './new-hero-editor.component.html',
  styleUrls: ['./new-hero-editor.component.css'],
  providers: [HeroFundsService]
})
export class NewHeroEditorComponent  implements OnInit {
  public entityId: string;
  creatureType = CreatureType.Hero;
  constructor(
    public service: HeroesService,
    private dataService: DataService,
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private messageService: MessageService,
    public heroManagement: HeroManagementService
  ) {
    this.entityId = config.data.entityId;
    this.config.header = 'Hero';
  }

  ngOnInit() {
  }
}
