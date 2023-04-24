import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {BaseEntityService} from '../../base-entity-service';
import {Entity} from '../../models/Entity.model';
import {LazyLoadEvent} from 'primeng/api';
import {BaseCrudService} from '../../base-service/base-crud-service';
import {DialogService} from 'primeng/dynamicdialog';
import {BaseComponentConfig} from '../base-component-config';
import {EditorAction} from '../../dtos/ModalEntityData';
import {UniverseService} from '../../../universes/universe.service';
import { Router } from '@angular/router';

@Component({
  selector: 'rr-rr-grid',
  templateUrl: './r-r-grid.component.html',
  styleUrls: ['./r-r-grid.component.css'],
  providers: [DialogService]
})
export class RRGridComponent<T extends Entity> implements OnInit {
  data: T[] = [];
  @Input('columns') _columns: RRColumns[];
  @Input() service: BaseCrudService<T, T>;
  @Input() create: Function;
  @Input() config: BaseComponentConfig<T>;
  @Input() isSelect: boolean;
  totalCount: number;
  loading = true;
  first = 0;
get columns() {
  return this._columns || this.config.entityListColumns;
}
get actions() {
  return this.config.entityListActions;
}

  @Output() rowSelectedEvent = new EventEmitter<T>();
  constructor(
    private dialogService: DialogService,
    private universeService: UniverseService,
    private router: Router,
  ) { }

  ngOnInit() {
    this.universeService.universeChanged.subscribe(() => {
      this.get();
    });
    this.service.entityUpdated.subscribe(entity => this.updateData(entity));
    this.service.entityCreated.subscribe(entity => this.addData(entity));
    this.service.entityDeleted.subscribe(entity => this.deleteData(entity));
    if (!this.create) {
      this.create = () => {
        this.dialogService.open(this.config.creator, {
          height: this.config.creatorOptions ? this.config.creatorOptions.height : '80%',
          data: {
            action: EditorAction.create
          }
        });
      };
    }
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
      this.service.list(filter, skipCount, maxResultCount).subscribe(response => {
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
    if (!this.isSelect) {
      if (this.config.navigateOnRowSelect) {
        this.router.navigate([this.config.navigateUrlOnRowSelect]);
      } else {
        this.dialogService.open(this.config.editor, {
          data: {
            entityId: event.data.id,
            service: this.service,
            action: EditorAction.update
          },
          width: '100vw',
          height: '100vh',
          header: this.config.editorTitle
        })
          .onClose.subscribe();
      }
    }
  }


}

export interface RRColumns {
  header: string;
  property: string;
}
export class RRAction<T> {
  icon: string;
  csClass: string | null | undefined;
  toolType: string | null | undefined;
  callBack: (rowData: T) => void;
  condition: (rowData: T) => boolean;
}
