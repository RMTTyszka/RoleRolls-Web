import { OnInit, Injector } from '@angular/core';
import { BaseListComponent } from '../../base-list/base-list.component';
import { Entity } from '../../models/Entity.model';
import { MatDialogRef } from '@angular/material/dialog';
import { BaseCrudServiceComponent } from '../../base-service/base-crud-service.component';

export class BaseSelectorComponent<T extends Entity> extends BaseListComponent<T> implements OnInit {

  protected dialogRef: MatDialogRef<any>;
  constructor(
    injector: Injector,
    protected service: BaseCrudServiceComponent<T>,
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
