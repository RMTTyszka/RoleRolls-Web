import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormGroup} from '@angular/forms';
import {map, tap} from 'rxjs/operators';
import {Role} from '../../../shared/models/Role.model';
import {RoleService} from '../../roles-editor/role.service';
import {createForm} from '../../../shared/EditorExtension';
import {RolesService} from '../../roles.service';

@Component({
  selector: 'loh-role-select',
  templateUrl: './role-select.component.html',
  styleUrls: ['./role-select.component.css']
})
export class RoleSelectComponent implements OnInit {
  @Input() form: FormGroup;
  @Output() roleSelected = new EventEmitter<Role>();
  result: string[] = [];
  roles: Role[] = [];
  value: string;
  constructor(
    private service: RolesService
  ) { }

  ngOnInit() {
  }

  search(event) {
    this.service.list(event).pipe(
      tap(resp => this.roles = resp.content),
      map(resp => resp.content.map(role => role.name))
    ).subscribe(response => this.result = response);
  }
  selected(roleName: string) {
    const selectedRole = this.roles.find(r => r.name === roleName);
    const form = new FormGroup({});
    createForm(form , selectedRole);
    this.form.setControl('role', form);
  }

}
