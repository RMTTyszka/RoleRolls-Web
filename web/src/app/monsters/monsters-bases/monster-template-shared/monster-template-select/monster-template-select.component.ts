import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormGroup} from '@angular/forms';
import {Race} from '../../../../shared/models/Race.model';
import {RaceService} from '../../../../races/race-editor/race.service';
import {map, tap} from 'rxjs/operators';
import {createForm} from '../../../../shared/EditorExtension';
import {MonsterModel} from '../../../../shared/models/creatures/monsters/MonsterModel.model';
import {MonsterModelsService} from '../../monster-template-provider/monster-models.service';
import {DialogService} from 'primeng/dynamicdialog';
import {RrSelectModalComponent} from '../../../../shared/components/rr-select-modal/rr-select-modal.component';
import {RRSelectModalInjector} from '../../../../shared/components/rr-select-field/rr-select-field.component';

@Component({
  selector: 'rr-monster-template-select',
  templateUrl: './monster-template-select.component.html',
  styleUrls: ['./monster-template-select.component.css']
})
export class MonsterTemplateSelectComponent implements OnInit {

  @Output() monsterTemplateSelected = new EventEmitter<MonsterModel>();
  @Input() monsterTemplate: MonsterModel = new MonsterModel();

  constructor(
    private service: MonsterModelsService,
    private dialogService: DialogService,
  ) {
  }

  ngOnInit() {
  }

  search() {
    this.dialogService.open(RrSelectModalComponent, {
      data: <RRSelectModalInjector<MonsterModel>>{
        service: this.service
      }
    }).onClose.subscribe((monsterTemplate: MonsterModel) => {
      if (monsterTemplate) {
        this.selected(monsterTemplate);
      }
    });
  }

  selected(template: MonsterModel) {
    this.monsterTemplate = template;
    this.monsterTemplateSelected.next(template);
  }
}
