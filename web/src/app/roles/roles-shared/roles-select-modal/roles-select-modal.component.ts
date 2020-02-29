import { Component, OnInit, Injector } from '@angular/core';
import { Role } from 'src/app/shared/models/Role.model';
import { BaseSelectorComponent } from 'src/app/shared/base-selector/base-selector/base-selector.component';
import { RolesService } from '../../roles.service';
import { MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';

@Component({
  selector: 'loh-roles-selector',
  templateUrl: './roles-select-modal.component.html',
  styleUrls: ['./roles-select-modal.component.css']
})
export class RolesSelectModalComponent extends BaseSelectorComponent<Role> implements OnInit {

  constructor(
    injector: Injector,
    protected service: RolesService,
    protected dialogRef: MatDialogRef<RolesSelectModalComponent>,
    protected router: Router

  ) {
    super(injector, service);
  }

  ngOnInit() {
    this.getAll();
  }

}
