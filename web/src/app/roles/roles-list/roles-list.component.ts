import {Component, Injector, OnInit} from '@angular/core';
import {LegacyBaseListComponent} from 'src/app/shared/base-list/legacy-base-list.component';
import {Role} from 'src/app/shared/models/Role.model';
import {RolesService} from '../roles.service';
import {RolesEditorComponent} from '../roles-editor/roles-editor.component';

@Component({
  selector: 'loh-roles-list',
  templateUrl: './roles-list.component.html',
  styleUrls: ['./roles-list.component.css']
})
export class RolesListComponent extends LegacyBaseListComponent<Role> implements  OnInit {

  constructor(
    injector: Injector,
    protected service: RolesService,
  ) {
    super(injector, service);
    this.editor = RolesEditorComponent;
   }

  ngOnInit() {
    this.getAllFiltered();
  }

  create() {
    this.edit(new Role());
  }

}
