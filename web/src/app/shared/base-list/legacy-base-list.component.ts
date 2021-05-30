import {AfterViewInit, Directive, Injector, OnInit, ViewChild} from '@angular/core';
import {Entity} from '../models/Entity.model';
import {LegacyBaseCrudServiceComponent} from '../legacy-base-service/legacy-base-crud-service.component';
import {MatPaginator, PageEvent} from '@angular/material/paginator';
import {MatDialog} from '@angular/material/dialog';
import {Router} from '@angular/router';
@Directive()
export class LegacyBaseListComponent<T extends Entity> implements OnInit, AfterViewInit {

  isLoading = true;
  useRoute = false;
  route: string;
  data: T[] = [];
  displayData: T[] = [];
  filter = '';
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;

  protected dialog: MatDialog;
  protected editor: any;
  protected router: Router;
  constructor(
    injector: Injector,
    protected service: LegacyBaseCrudServiceComponent<T>
  ) {
    this.dialog = injector.get(MatDialog);
    this.router = injector.get(Router);

  }

  ngOnInit() {
    this.paginator.page.subscribe((page: PageEvent) => {
      this.getAllFiltered();
    });
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

  getAllFiltered() {
    const skipCount = this.paginator.pageIndex ||  0;
    const maxResultCount = this.paginator.pageSize || 10;
    this.service.getAllFiltered(this.filter, skipCount , maxResultCount).subscribe(data => {
      console.log(data);
      this.data = data;
      this.updateDisplayData();
      this.isLoading = false;
      this.paginator.length = 200
    });
  }

  add() {
    if (!this.useRoute) {
      this.dialog.open(this.editor, {
        maxHeight: '90vh',
        minWidth: '90vw'
      }).afterClosed().subscribe(() => {
        this.getAll();
      });
    } else {
      this.router.navigate([this.route]);
    }
  }

  edit(entity?: T) {
    if (!this.useRoute) {
      const dialogRef = this.dialog.open(this.editor, {
        maxHeight: '90vh',
        minWidth: '90vw',
        data: entity
      });

      dialogRef.afterClosed().subscribe(() => {
        this.getAll();
      });
    } else {
      this.router.navigate([this.route, {id: entity.id}]);
    }
  }

  updateDisplayData() {
    if (this.paginator) {
      this.paginator.length = this.paginator.length += this.data.length;
      this.displayData = this.data;
    }
  }
  updateFilter() {
    this.getAllFiltered();
  }

  resetPaginator() {
    this.paginator.length = this.data.length;
    this.paginator.pageIndex = 0;
  }

}
