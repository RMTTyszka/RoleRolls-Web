import {Component, Inject, Injector, OnInit} from '@angular/core';
import {EncountersService} from '../encounters.service';
import {Encounter} from '../../shared/models/Encounter.model';
import {FormArray, FormBuilder, FormControl, FormGroup} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {DynamicDialogConfig, DynamicDialogRef} from 'primeng/dynamicdialog';
import {BaseCreatorComponent} from '../../shared/base-creator/base-creator.component';
import {Race} from '../../shared/models/Race.model';
import {MonsterModel} from '../../shared/models/creatures/monsters/MonsterModel.model';
import {constants} from 'http2';
import {MessageService} from 'primeng/api';

export class EncounterEditorProperies {
  name: 'name';
  level: 'level';
  monsters: 'monsters';
}

@Component({
  selector: 'rr-encounter-create-edit',
  templateUrl: './encounter-create-edit.component.html',
  styleUrls: ['./encounter-create-edit.component.css']
})
export class EncounterCreateEditComponent extends BaseCreatorComponent<Encounter, Encounter> implements OnInit {

  get monsters(): FormArray {
    return this.form.get('monsters') as FormArray;
  }

  constructor(
    injector: Injector,
    protected dialogRef: DynamicDialogRef,
    protected dialogConfig: DynamicDialogConfig,
    protected service: EncountersService,
    private messageService: MessageService,
    ) {
    super(injector);
    if (this.dialogConfig.data) {
      this.getEntity(this.dialogConfig.data.entityId);
    } else {
      this.getEntity();
    }
  }

  ngOnInit() {

  }

  addMonsterPlace() {
    this.monsters.push(new FormControl(new MonsterModel()));
  }
  monsterTemplateSelected(monsterTemplate: MonsterModel, arrayIndex: number) {
    this.addMonster(monsterTemplate, arrayIndex);
  }

  addMonster(monsterTemplate: MonsterModel, arrayIndex: number) {
    this.service.addMonster(this.entity.id, monsterTemplate).subscribe(() => {
      this.monsters.controls[arrayIndex].setValue(monsterTemplate);
    }, error => {
      if (error.status === HTTP_STATUS_UNPROCESSABLE_ENTITY) {
        this.messageService.add({
          key: 'Failed to add monster',
          detail: 'That monster has already been added before.'
        });
      }
    });
  }

}
