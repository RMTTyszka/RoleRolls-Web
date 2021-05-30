import {Injector, OnInit} from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {Observable, Subject} from 'rxjs';
import {Entity} from '../models/Entity.model';
import {LOH_API} from '../../loh.api';
import {PagedOutput} from '../dtos/PagedOutput';
import {tap} from 'rxjs/operators';
import {BaseCrudResponse} from '../models/BaseCrudResponse';

export class LegacyBaseCrudServiceComponent<T extends Entity> {

  path: string;
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

  getAllPaged(filter: string = '', skipCount: number = 0, maxResultCount: number = 15): Observable<PagedOutput<T>> {
    let params: HttpParams;
    params = new HttpParams().set('filter', filter).set('skipCount', skipCount.toString()).set('maxResultCount', maxResultCount.toString());
    return this.http.get<PagedOutput<T>>(this.serverUrl + this.path + '/allPaged', {params: params } );
  }

  getAllFiltered(filter: string, skipCount?: number, maxResultCount?: number): Observable<T[]> {
    skipCount = skipCount || 0;
    maxResultCount = maxResultCount || 100;
    filter = filter || '';
    const params = new HttpParams().set('skipCount', skipCount.toString()).set('maxResultCount', maxResultCount.toString()).set('filter', filter);
    return this.http.get<T[]>(this.serverUrl + this.path + '/allFiltered', {
      params: params
    });
  }
  getAll(): Observable<T[]> {
    const params = new HttpParams();
    return this.http.get<T[]>(this.serverUrl + this.path + '/all', {
      params: params
    });
  }
  get(id: string): Observable<T> {
    return this.http.get<T>(this.serverUrl + this.path + '/find', {params: {id: id}});
  }

  create(entity: T): Observable<BaseCrudResponse<T>> {
    return this.http.post<BaseCrudResponse<T>>(this.serverUrl + this.path + '/create', entity).pipe(
      tap((response: BaseCrudResponse<T>) => this.entityCreated.next(response.entity))
    );
  }
  update(entity: T): Observable<BaseCrudResponse<T>> {
    return this.http.put<BaseCrudResponse<T>>( this.serverUrl + this.path + '/update', entity).pipe(
      tap((response: BaseCrudResponse<T>) => {
        if (!response.success) {
        } else {
        this.entityUpdated.next(response.entity);
        }
      }))
    }
  delete(entity: T): Observable<BaseCrudResponse<T>> {
    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      body: entity.id,
    };
    return this.http.delete<BaseCrudResponse<T>>(this.serverUrl + this.path + '/delete?' + 'id='
      + encodeURIComponent('' + entity.id), options).pipe(
      tap((response: BaseCrudResponse<T>) => this.entityDeleted.next(response.entity))
    );
  }

}
