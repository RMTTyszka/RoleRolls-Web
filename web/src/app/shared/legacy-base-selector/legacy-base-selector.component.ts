import {Directive, Injector, OnInit} from '@angular/core';
import {LegacyBaseListComponent} from '../base-list/legacy-base-list.component';
import {Entity} from '../models/Entity.model';
import {LegacyBaseCrudServiceComponent} from '../legacy-base-service/legacy-base-crud-service.component';
import { MatDialogRef } from '@angular/material/dialog';

@Directive()
export class LegacyBaseSelectorComponent<T extends Entity> extends LegacyBaseListComponent<T> implements OnInit {

  protected dialogRef: MatDialogRef<any>;
  constructor(
    injector: Injector,
    protected service: LegacyBaseCrudServiceComponent<T>,
  ) {
    super(injector, service);
  }

  ngOnInit() {
    super.ngOnInit();
  }

  select(selected: T) {
    this.dialogRef.close(selected);
  }

}
