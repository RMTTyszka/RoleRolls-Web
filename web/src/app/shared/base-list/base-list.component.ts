import { OnInit, ViewChild, AfterViewInit, Injector } from '@angular/core';
import { Entity } from '../models/Entity.model';
import { BaseCrudServiceComponent } from '../base-service/base-crud-service.component';
import { MatPaginator } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';

export class BaseListComponent<T extends Entity> implements OnInit, AfterViewInit {

  isLoading = true;

  data: T[] = [];
  displayData: T[] = [];
  filter: string;
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;

  protected dialog: MatDialog;
  protected editor: any;
  constructor(
    injector: Injector,
    protected service: BaseCrudServiceComponent<T>
  ) {
    this.dialog = injector.get(MatDialog);
  }

  ngOnInit() {
  }
  ngAfterViewInit() {
  }

  getAll() {
    this.service.getAll().subscribe(data => {
      console.log(data);
      this.data = data;
      this.updateDisplayData();
      this.isLoading = false;
    });
  }

  getAllFiltered(filter?: string) {
    this.service.getAllFiltered(filter).subscribe(data => {
      console.log(data);
      this.data = data;
      this.updateDisplayData();
      this.isLoading = false;
    });
  }

  add() {
    this.dialog.open(this.editor, {
      maxHeight: '90vh',
      minWidth: '90vw'
    }).afterClosed().subscribe(() => {
      this.getAll();
    });
  }

  edit(entity?: T) {

    const dialogRef = this.dialog.open(this.editor, {
      maxHeight: '90vh',
      minWidth: '90vw',
      data: entity
    });

    dialogRef.afterClosed().subscribe(() => {
      this.getAll();
    });
  }

  updateDisplayData() {
    if (this.paginator) {
      this.paginator.length = this.data.length;
      this.displayData = this.data.slice(this.paginator.pageIndex * this.paginator.pageSize,
      this.paginator.pageIndex * this.paginator.pageSize + this.paginator.pageSize);
    }
  }
  updateFilter() {
    this.paginator.pageIndex = 0;
    console.log(this.filter);
    if (this.filter && this.filter.length > 0) {
      this.displayData = this.data.filter((data: T) => {
        let contains = false;
        Object.values(data).forEach(val => {
          if (val.toString().indexOf(this.filter) > -1) {
            contains = true;
          }
        });
        return contains;
      }).slice(this.paginator.pageIndex * this.paginator.pageSize,
        this.paginator.pageIndex * this.paginator.pageSize + this.paginator.pageSize);
        this.paginator.length = this.displayData.length;
    } else {
      this.displayData = this.data.slice(this.paginator.pageIndex * this.paginator.pageSize,
        this.paginator.pageIndex * this.paginator.pageSize + this.paginator.pageSize);
        this.paginator.length = this.data.length;
    }
  }

  resetPaginator() {
    this.paginator.length = this.data.length;
    this.paginator.pageIndex = 0;
  }

}
