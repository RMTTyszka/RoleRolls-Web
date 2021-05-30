import {Component, Inject, Injector, OnInit} from '@angular/core';
import {EncountersService} from '../encounters.service';
import {Encounter} from '../../shared/models/Encounter.model';
import {FormArray, FormBuilder, FormControl, FormGroup} from '@angular/forms';
import {DynamicDialogConfig, DynamicDialogRef} from 'primeng/dynamicdialog';
import {BaseCreatorComponent} from '../../shared/base-creator/base-creator.component';
import {MonsterModel} from '../../shared/models/creatures/monsters/MonsterModel.model';
import {MessageService} from 'primeng/api';
import {HttpStatusCode} from '@angular/common/http';

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
    public service: EncountersService,
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
      this.messageService.add({
        key: 'mainToast',
        detail: 'Monster template added',
        severity: 'success',
      });
    }, error => {
      if (error.status === HttpStatusCode.UnprocessableEntity) {
        this.messageService.add({
          key: 'mainToast',
          detail: 'That monster has already been added before.',
          severity: 'error',
        });
        this.monsters.controls[arrayIndex].setValue(new MonsterModel());
      }
    });
  }

  removeMonsterPlace(index: number) {
    const monsterTemplate = this.monsters.controls[index].value as MonsterModel;
    if (monsterTemplate.id) {
      this.service.removeMonster(this.entity.id, monsterTemplate.id).subscribe(() => {
        this.monsters.removeAt(index);
        this.messageService.add({
          key: 'mainToast',
          detail: 'Monster template removed',
          severity: 'success',
        });
      });
    } else {
      this.monsters.removeAt(index);
    }
  }

}
