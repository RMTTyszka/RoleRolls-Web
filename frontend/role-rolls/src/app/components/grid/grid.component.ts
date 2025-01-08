import { Component, EventEmitter, Input, Output } from '@angular/core';
import { TableModule } from 'primeng/table';
import { DialogService } from 'primeng/dynamicdialog';
import { Router } from '@angular/router';
import { LazyLoadEvent } from 'primeng/api';
import { Entity } from '../../models/Entity.model';
import EventEmitter from 'node:events';

@Component({
  selector: 'rr-grid',
  imports: [
    TableModule
  ],
  templateUrl: './grid.component.html',
  styleUrl: './grid.component.scss'
})
export class GridComponent<T extends Entity> {
  data: T[] = [];
  @Input('columns') _columns: RRColumns[];
  @Input() create: Function;
  @Input() getList: Function;
  @Input() select: Function;
  @Input() isSelect: boolean;
  @Input() refresh: EventEmitter<void>;
  @Input() headerActions: RRAction<void>[] = [];
  @Input() actions: RRAction<void>[] = [];
  totalCount: number;
  loading = true;
  first = 0;
  get columns() {
    return this._columns;
  }
  get actions() {
    return this.actions;
  }
  public actionsWidth() {
    return {width: `${this.actions.length * 3}%`}
  }

  @Output() rowSelectedEvent = new EventEmitter<T>();
  constructor(
    private dialogService: DialogService,
    private router: Router,
  ) { }

  ngOnInit() {
    if (this.refresh) {
      this.refresh.subscribe(() => {
        this.get();
      })
    }
/*    this.service.entityUpdated.subscribe(entity => this.updateData(entity));
    this.service.entityCreated.subscribe(entity => this.addData(entity));
    this.service.entityDeleted.subscribe(entity => this.deleteData(entity));*/
/*    if (!this.create) {
      this.create = () => {
        this.dialogService.open(this.config.creator, {
          height: this.config.creatorOptions ? this.config.creatorOptions.height : '80%',
          width: this.config.creatorOptions ? this.config.creatorOptions.height : '60%',
          data: {
            action: EditorAction.create
          }
        });
      };
    }*/
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
    this.getList(filter, skipCount, maxResultCount).subscribe(response => {
      this.data = response.content;
      this.totalCount = response.totalElements;
      this.loading = false;
    }, error => {
      this.data = [];
      this.totalCount = 0;
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

  rowSelected(event: {data: T}) {
    this.rowSelectedEvent.emit(event.data);
    this.select(event.data);
  }
}

export interface RRColumns {
  header: string;
  property: string;
  format?: (obj: any, value: any) => any;
}
export interface RRAction<T> {
  icon: string;
  csClass: string | null | undefined;
  tooltip: string | null | undefined;
  callBack: (rowData: T, target: any) => void;
  condition: (rowData: T) => boolean;
}
