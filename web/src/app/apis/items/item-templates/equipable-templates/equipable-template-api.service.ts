import { Injectable } from '@angular/core';
import {LOH_API} from '../../../../loh.api';
import {Observable} from 'rxjs';
import {PagedOutput} from '../../../../shared/dtos/PagedOutput';
import {HttpClient, HttpParams} from '@angular/common/http';
import {EquipableTemplate} from '../../../../shared/models/items/EquipableTemplate';

@Injectable()
export class EquipableTemplateApiService {

  path = 'equipable-templates';
  basePath = LOH_API.myBackUrl;
  constructor(
    private httpClient: HttpClient
  ) { }

  list(filter: string = '', skipCount: number = 0, maxResultCount: number = 15): Observable<PagedOutput<EquipableTemplate>> {
    return this.httpClient.get<PagedOutput<EquipableTemplate>>(this.basePath + this.path, {
      params: new HttpParams()
        .set('filter', filter)
        .set('skipCount', skipCount.toString())
        .set('maxResultCount', maxResultCount.toString())
    });
  }
}
