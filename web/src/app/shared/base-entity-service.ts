import { Entity } from './models/Entity.model';
import {Injector, EventEmitter} from '@angular/core';
import { BaseCrudServiceComponent } from './base-service/base-crud-service.component';
import { Subject, Observable } from 'rxjs';
import {finalize, tap} from 'rxjs/operators';

export class BaseEntityService<T extends Entity> extends BaseCrudServiceComponent<T> {

  id: string;
  entity: T;
  onSaveAction = new EventEmitter<boolean>();
  onEntityChange = new Subject<T>();
  constructor(injector: Injector, public type: new () => T
    ) {
    super(injector);
  }

  getEntity(id: string): Observable<T> {
    return this.get(id).pipe(
      tap(entity => {
        console.log(entity);
        this.entity = entity;
        this.onEntityChange.next(this.entity);
      }
    ),
      finalize(() => {
      }));
  }

  getNewEntity() {
    return this.http.get<T>(this.serverUrl + this.path + '/getNew').pipe(
      tap(entity => {
          console.log(entity);
          this.entity = entity;
        }
      ),
      finalize(() => {
        this.onEntityChange.next(this.entity);
      }));
  }
}
