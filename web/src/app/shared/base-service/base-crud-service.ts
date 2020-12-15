import {EventEmitter, Injector, OnInit, Type} from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {Observable, Subject} from 'rxjs';
import {Entity} from '../models/Entity.model';
import {LOH_API} from '../../loh.api';
import {PagedOutput} from '../dtos/PagedOutput';
import {tap} from 'rxjs/operators';
import {BaseCrudResponse} from '../models/BaseCrudResponse';
import {RRColumns} from '../components/cm-grid/cm-grid.component';
import {BaseComponentConfig} from '../components/base-component-config';
export abstract class BaseCrudService<T extends Entity, TCreateInput> {
  public abstract path: string;
  public abstract selectPlaceholder: string;
  public abstract fieldName: string;
  public abstract selectModalTitle: string;
  public abstract selectModalColumns: RRColumns[];
  public abstract entityListColumns: RRColumns[];
  config: BaseComponentConfig;
  onSaveAction = new EventEmitter<boolean>();
  onEntityChange = new Subject<T>();
  serverUrl = LOH_API.myBackUrl;
  entityUpdated = new Subject<T>();
  entityDeleted = new Subject<T>();
  entityCreated = new Subject<T>();
  protected http: HttpClient;
  constructor(
    injector: Injector,
  ) {
    this.http = injector.get(HttpClient);
  }

  list(filter: string = '', skipCount: number = 0, maxResultCount: number = 15): Observable<PagedOutput<T>> {
    return this.http.get<PagedOutput<T>>(this.serverUrl + this.path, {
      params: new HttpParams()
        .set('filter', filter)
        .set('skipCount', skipCount.toString())
        .set('maxResultCount', maxResultCount.toString())
    });
  }

  get(id: string): Observable<T> {
    return this.http.get<T>(this.serverUrl + this.path + `/${id}`);
  }
  getNew(): Observable<TCreateInput> {
    return this.http.get<TCreateInput>(this.serverUrl + this.path + `/new`);
  }

  create(entity: TCreateInput): Observable<BaseCrudResponse<T>> {
    return this.http.post<BaseCrudResponse<T>>(this.serverUrl + this.path, entity).pipe(
      tap((response: BaseCrudResponse<T>) => this.entityCreated.next(response.entity))
    );
  }
  update(entity: T): Observable<BaseCrudResponse<T>> {
    return this.http.put<BaseCrudResponse<T>>( this.serverUrl + this.path, entity).pipe(
      tap((response: BaseCrudResponse<T>) => {
        if (!response.success) {
        } else {
          this.entityUpdated.next(response.entity);
        }
      }));
  }
  delete(id: string): Observable<BaseCrudResponse<T>> {
    return this.http.delete<BaseCrudResponse<T>>(this.serverUrl + this.path + `/${id}`).pipe(
      tap((response: BaseCrudResponse<T>) => this.entityDeleted.next(response.entity))
    );
  }
}
