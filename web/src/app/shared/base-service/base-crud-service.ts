import {EventEmitter, Injector, OnInit, Type} from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {Observable, of, Subject} from 'rxjs';
import {Entity} from '../models/Entity.model';
import {LOH_API} from '../../loh.api';
import {PagedOutput} from '../dtos/PagedOutput';
import {map, tap, switchMap} from 'rxjs/operators';
import {BaseCrudResponse} from '../models/BaseCrudResponse';
import {RRColumns} from '../components/cm-grid/cm-grid.component';
import {BaseComponentConfig} from '../components/base-component-config';
export abstract class BaseCrudService<T extends Entity, TCreateInput extends Entity> {
  constructor(
    injector: Injector,
  ) {
    this.http = injector.get(HttpClient);
    if (this.config) {
    }
  }
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
  public static setConfig(service: BaseCrudService<any, any>, config: BaseComponentConfig) {
    service.path = config.path;
    service.selectPlaceholder = config.selectPlaceholder;
    service.fieldName = config.fieldName;
    service.selectModalTitle = config.selectModalTitle;
    service.selectModalColumns = config.selectModalColumns;
    service.entityListColumns = config.entityListColumns;
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

  createV2(entity: TCreateInput): Observable<BaseCrudResponse<T>> {
    return this.http.post<never>(this.serverUrl + this.path, entity).pipe(
      switchMap(() => {
        return this.get(entity.id)
        .pipe(
          map((createEntity: T) => {
            this.entityCreated.next(createEntity);
            return {
              entity: createEntity,
              success: true
            } as BaseCrudResponse<T>;
          })
        );
      })
    );
  }
  update(entity: T): Observable<BaseCrudResponse<T>> {
    return this.http.put<BaseCrudResponse<T>>( this.serverUrl + this.path + `/${entity.id}`, entity).pipe(
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
  protected get completePath(): string {
    return this.serverUrl + this.path;
  }
}
