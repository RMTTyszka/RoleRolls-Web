import { OnInit, Injector } from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {Observable, Subject, of} from 'rxjs';
import { Entity } from '../models/Entity.model';
import { LOH_API } from '../../loh.api';
import {PagedAndFilteredDto} from '../dtos/PagedAndFilteredDto';
import {tap, catchError} from 'rxjs/operators';
import {BaseCrudResponse} from '../models/BaseCrudResponse';
import { isNullOrUndefined } from 'util';

export class BaseCrudServiceComponent<T extends Entity> implements OnInit {

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

  ngOnInit() {
  }

  getAllPaged(filter: string = '', skipCount: number = 0, maxResultCount: number = 15): Observable<PagedAndFilteredDto<T>> {
    let params: HttpParams;
    params = new HttpParams().set('filter', filter).set('skipCount', skipCount.toString()).set('maxResultCount', maxResultCount.toString());
    return this.http.get<PagedAndFilteredDto<T>>(this.serverUrl + this.path + '/allPaged', {params: params } );
  }

  getAllFiltered(filter?: string): Observable<T[]> {
    const params = new HttpParams();
    if (!isNullOrUndefined(filter)) {
      params.set('filter', filter);
    }
    return this.http.get<T[]>(this.serverUrl + this.path + '/allFiltered', {
      params: {filter: filter || ''}
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
