import {Component, Inject, Injector, OnInit} from '@angular/core';
import {LegacyBaseCreatorComponent} from 'src/app/shared/base-creator/legacy-base-creator.component';
import {FormArray} from '@angular/forms';
import {Role} from 'src/app/shared/models/Role.model';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {RoleService} from './role.service';

@Component({
  selector: 'loh-roles-editor',
  templateUrl: './roles-editor.component.html',
  styleUrls: ['./roles-editor.component.css']
})
export class RolesEditorComponent extends LegacyBaseCreatorComponent<Role> implements OnInit {

  myBonuses: FormArray;
  constructor(
    injector: Injector,
    protected service: RoleService,
    protected dialogRef: MatDialogRef<RolesEditorComponent>,
    @Inject(MAT_DIALOG_DATA) data: any
  ) {
    super(injector, service);
    this.entity = data;
    this.createForm(this.form, this.entity);
    this.myBonuses = <FormArray>this.form.get('bonuses');

   }

  ngOnInit() {
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
