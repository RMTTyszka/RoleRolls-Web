import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {BaseEntityService} from '../../base-entity-service';
import {Entity} from '../../models/Entity.model';
import {LazyLoadEvent} from 'primeng/api';


@Component({
  selector: 'loh-cm-grid',
  templateUrl: './cm-grid.component.html',
  styleUrls: ['./cm-grid.component.css']
})
export class CmGridComponent<T extends Entity> implements OnInit {
  data: T[] = [];
  @Input() columns: CmColumns[];
  @Input() service: BaseEntityService<T>;
  totalCount: number;
  loading = true;
  first = 0;

  @Output() rowSelectedEvent = new EventEmitter<T>();
  constructor() { }

  ngOnInit() {
    this.service.entityUpdated.subscribe(entity => this.updateData(entity));
    this.service.entityCreated.subscribe(entity => this.addData(entity));
    this.service.entityDeleted.subscribe(entity => this.deleteData(entity));
    this.get();
  }
  private updateData(entity: T) {
    const index = this.data.findIndex(e => e.id === entity.id);
    this.data[index] = entity;
  }
  private addData(entity: T) {
    this.data.push(entity);
  }
  private deleteData(entity: T) {
    const index = this.data.findIndex(e => e.id === entity.id);
    this.data = this.data.filter(e => e.id !== entity.id);
  }
  get(filter?: string, skipCount?: number, maxResultCount?: number) {
      this.service.getAllPaged(filter, skipCount, maxResultCount).subscribe(response => {
        this.data = response.content;
        this.totalCount = response.totalElements;
        this.loading = false;
      });
  }
  resolve(path, obj) {
    return path.split('.').reduce(function(prev, curr) {
      return prev ? prev[curr] : null;
    }, obj || self);
  }

  onLazyLoadEvent(event: LazyLoadEvent) {
    this.loading = true;
    this.get('', event.first / event.rows, event.rows);
  }

  rowSelected(event: any) {
    this.rowSelectedEvent.emit(event.data);
  }


}

export interface CmColumns {
  header: string;
  property: string;
}
