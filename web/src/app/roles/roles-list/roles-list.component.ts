import {Component, Injector, OnInit} from '@angular/core';
import {Role} from 'src/app/shared/models/Role.model';
import {RolesService} from '../roles.service';
import {RoleConfig} from '../role-config';
import {DialogService} from 'primeng/dynamicdialog';

@Component({
  selector: 'rr-roles-list',
  templateUrl: './roles-list.component.html',
  styleUrls: ['./roles-list.component.css'],
  providers: [DialogService]
})
export class RolesListComponent implements  OnInit {
  config = new RoleConfig();
  constructor(
    injector: Injector,
    protected service: RolesService,
  ) {
   }

  ngOnInit() {
  }

  create() {
  }

}
