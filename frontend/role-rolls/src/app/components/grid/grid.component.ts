import { Component, createComponent, EventEmitter, input, Input, Output, Type } from '@angular/core';
import { TableLazyLoadEvent, TableModule } from 'primeng/table';
import { DialogService, DynamicDialogConfig } from 'primeng/dynamicdialog';
import { Router } from '@angular/router';
import { Entity } from '../../models/Entity.model';
import { HttpErrorResponse } from '@angular/common/http';
import { PagedOutput } from '../../models/PagedOutput';
import { safeCast } from '../../tokens/utils.funcs';
import { Tooltip } from 'primeng/tooltip';
import { NgForOf, NgIf, NgStyle } from '@angular/common';
import { Button, ButtonDirective, ButtonIcon } from 'primeng/button';
import { BaseCrudService } from '../../services/base-service/base-crud-service';
import { GetListInput } from '../../tokens/get-list-input';
import { firstValueFrom } from 'rxjs';
import { InputText } from 'primeng/inputtext';

@Component({
  selector: 'rr-grid',
  imports: [
    TableModule,
    Tooltip,
    ButtonDirective,
    NgStyle,
    NgForOf,
    NgIf,
    InputText,
    ButtonIcon,
  ],
  templateUrl: './grid.component.html',
  styleUrl: './grid.component.scss'
})
export class GridComponent<T extends Entity> {
  data: T[] = [];
  public columns = input<RRColumns[]>();
  @Input() service!: BaseCrudService<T>;
  @Input() select!: Function;
  @Input() isSelect!: boolean;
  @Input() refresh!: EventEmitter<void>;
  @Input() headerActions: RRHeaderAction[] = [];
  @Input() actions: RRTableAction<T>[] = [];
  public editorComponent = input<Type<any>>();
  public editorRoute = input<string>();
  totalCount: number = 0;
  loading = true;
  first = 0;
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
        this.getList();
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
    this.getList();
  }
  public async create() {
    console.log('coco')
    const editor = this.editorComponent();
    if (!editor) {
      const route = this.editorRoute();
      if (route) {
        this.router.navigate([route]);
      }
      return;
    }
    await firstValueFrom(this.dialogService.open(editor, {
      data: null,
      focusOnShow: false
    } as DynamicDialogConfig).onClose)
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
  getList(filter?: string, skipCount?: number, maxResultCount?: number) {
    this.service.getList({
      filter,
      skipCount,
      maxResultCount
    } as GetListInput).subscribe((response: PagedOutput<T>) => {
      this.data = response.itens;
      this.totalCount = safeCast<number>(response.totalCount);
      this.loading = false;
    }, (error: HttpErrorResponse) => {
      this.data = [];
      this.totalCount = 0;
      this.loading = false;
    });
  }
  resolve(path: string, obj: any) {
    return path.split('.').reduce(function(prev, curr) {
      return prev ? prev[curr] : null;
    }, obj || self);
  }

  onLazyLoadEvent(event: TableLazyLoadEvent) {
    this.loading = true;
    this.getList('', (event.first ?? 0) / (event.rows ?? 0), event.rows ?? 0);
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
export interface RRTableAction<T> {
  icon: string;
  csClass: string | null | undefined;
  tooltip: string;
  callBack: (rowData: T, target: any) => void;
  condition: (rowData: T) => boolean;
}
export interface RRHeaderAction {
  icon: string;
  csClass: string | null | undefined;
  tooltip: string;
  callBack: (target: any) => void;
  condition: () => boolean;
}
