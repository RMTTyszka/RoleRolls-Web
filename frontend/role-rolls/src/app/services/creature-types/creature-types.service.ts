import { Injectable } from '@angular/core';
import {BaseCrudService} from '@services/base-service/base-crud-service';
import {CreatureType} from '@app/models/creatureTypes/creature-type';
import {HttpClient, HttpParams} from '@angular/common/http';
import {PagedOutput} from '@app/models/PagedOutput';
import {Observable} from 'rxjs';
import {RR_API} from '@app/tokens/loh.api';

@Injectable({
  providedIn: 'root'
})
export class CreatureTypesService {
  public path(templateId: string): string {
    return `campaign-templates/${templateId}/creature-types`;
  };

  constructor(
    private http: HttpClient,
  ) {
  }
  public getList(templateId: string, query: { [key: string]: any }): Observable<PagedOutput<CreatureType>> {
    const params = Object.entries(query).reduce((httpParams, [key, value]) => {
      if (value !== undefined && value !== null) {
        return httpParams.set(key, value.toString());
      }
      return httpParams;
    }, new HttpParams());
    return this.http.get<PagedOutput<CreatureType>>(`${RR_API.backendUrl}${this.path(templateId)}`, { params });
  }
}
