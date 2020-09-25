import {Component, Injector, OnInit} from '@angular/core';
import {Role} from 'src/app/shared/models/Role.model';
import {LegacyBaseSelectorComponent} from 'src/app/shared/legacy-base-selector/legacy-base-selector.component';
import {RolesService} from '../../roles.service';
import {MatDialogRef} from '@angular/material/dialog';
import {Router} from '@angular/router';

@Component({
  selector: 'loh-roles-selector',
  templateUrl: './roles-select-modal.component.html',
  styleUrls: ['./roles-select-modal.component.css']
})
export class RolesSelectModalComponent extends LegacyBaseSelectorComponent<Role> implements OnInit {

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
