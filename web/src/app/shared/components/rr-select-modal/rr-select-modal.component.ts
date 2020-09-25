import { Component, OnInit } from '@angular/core';
import {DynamicDialogRef} from 'primeng/dynamicdialog';
import {BaseCrudService} from '../../base-service/base-crud-service';
import {DynamicDialogConfig} from 'primeng/dynamicdialog';
import {Entity} from '../../models/Entity.model';
import {RRSelectModalInjector} from '../rr-select-field/rr-select-field.component';

@Component({
  selector: 'loh-rr-select-modal',
  templateUrl: './rr-select-modal.component.html',
  styleUrls: ['./rr-select-modal.component.css']
})
export class RrSelectModalComponent<T extends Entity> implements OnInit {

  hasLoaded = false;
  service: BaseCrudService<T>;
  constructor(
    private dialogRef: DynamicDialogRef,
    public config: DynamicDialogConfig,
  ) {
    this.service = (<RRSelectModalInjector<T>> config.data).service;
    this.hasLoaded = true;
  }

  ngOnInit(
  ): void {
  }

  entitySelected(event: T) {
    this.dialogRef.close(event);
  }
}
