import {EventEmitter, Injector, OnInit, Type} from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {Observable, of, Subject} from 'rxjs';
import {map, tap, switchMap} from 'rxjs/operators';
import { Entity } from '../../models/Entity.model';
import { RR_API } from '../../tokens/loh.api';
import { PagedOutput } from '../../models/PagedOutput';
export abstract class BaseCrudService<T extends Entity> {
  constructor(
    protected http: HttpClient,
  ) {
  }
  public abstract path: string;
  onSaveAction = new EventEmitter<boolean>();
  onEntityChange = new Subject<T>();
  serverUrl = RR_API.backendUrl;
  entityUpdated = new Subject<T>();
  entityDeleted = new Subject<string>();
  entityCreated = new Subject<T>();

  getList(query: { [key: string]: any }): Observable<PagedOutput<T>> {
    const params = Object.entries(query).reduce((httpParams, [key, value]) => {
      if (value !== undefined && value !== null) {
        return httpParams.set(key, value.toString());
      }
      return httpParams;
    }, new HttpParams());
    return this.http.get<PagedOutput<T>>(`${this.serverUrl}${this.path}`, { params });
  }

  get(id: string): Observable<T> {
    return this.http.get<T>(this.serverUrl + this.path + `/${id}`);
  }

  create(entity: T): Observable<T> {
    const preparedEntity = this.beforeCreate(entity);
    return this.http.post<never>(`${this.serverUrl}${this.path}`, preparedEntity).pipe(
      switchMap(() =>
        this.get(preparedEntity.id).pipe(
          tap((retrievedEntity) => {
            this.entityCreated.next(retrievedEntity);
          })
        )
      )
    );
  }
  beforeCreate(entity: T): T {
    return entity;
  }
  update(entity: T): Observable<never> {
    return this.http.put<never>( this.serverUrl + this.path + `/${entity.id}`, entity).pipe(
      tap(() => {
          this.entityUpdated.next(entity);
      }));
  }
  delete(id: string): Observable<never> {
    return this.http.delete<never>(this.serverUrl + this.path + `/${id}`).pipe(
      tap(() => {
          this.entityDeleted.next(id)
      })
    );
  }
  protected get completePath(): string {
    return this.serverUrl + this.path;
  }
}
