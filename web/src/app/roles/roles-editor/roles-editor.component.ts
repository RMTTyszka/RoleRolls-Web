import {Component, Inject, Injector, OnInit} from '@angular/core';
import {LegacyBaseCreatorComponent} from 'src/app/shared/base-creator/legacy-base-creator.component';
import {FormArray} from '@angular/forms';
import {Role} from 'src/app/shared/models/Role.model';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {RoleService} from './role.service';
import {DynamicDialogConfig, DynamicDialogRef} from 'primeng/dynamicdialog';
import {BaseCreatorComponent} from '../../shared/base-creator/base-creator.component';
import {RolesService} from '../roles.service';

@Component({
  selector: 'loh-roles-editor',
  templateUrl: './roles-editor.component.html',
  styleUrls: ['./roles-editor.component.css']
})
export class RolesEditorComponent extends BaseCreatorComponent<Role, Role> implements OnInit {

  myBonuses: FormArray;
  constructor(
    injector: Injector,
    protected service: RolesService,
    protected dialogRef: DynamicDialogRef,
    protected dialogConfig: DynamicDialogConfig
  ) {
    super(injector);
    if (dialogConfig.data) {
      this.getEntity(dialogConfig.data.entityId);
    } else {
      this.getEntity();
    }

   }

  ngOnInit() {
  }
  protected afterGetEntity() {
    super.afterGetEntity();
    this.myBonuses = <FormArray>this.form.get('bonuses');
  }
  addBonus() {
    this.myBonuses.push(this.fb.group({
      property: [],
      level: [],
      bonus: []
    }));
  }
  removeBonus(index: number) {
    this.myBonuses.removeAt(index);
  }

}
