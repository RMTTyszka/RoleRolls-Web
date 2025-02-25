import { Injectable } from '@angular/core';
import {BaseCrudService} from '@services/base-service/base-crud-service';
import {CreatureType} from '@app/models/creatureTypes/creature-type';
import {HttpClient, HttpParams} from '@angular/common/http';
import {PagedOutput} from '@app/models/PagedOutput';
import {Observable} from 'rxjs';
import {RR_API} from '@app/tokens/loh.api';
import { Bonus } from '@app/models/bonuses/bonus';

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
  public getById(templateId: string, creatureTypeId: string): Observable<CreatureType> {
    return this.http.get<CreatureType>(`${RR_API.backendUrl}${this.path(templateId)}/${creatureTypeId}`);
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
  public addBonus(templateId: string, creatureTypeId: string, bonus: Bonus): Observable<Bonus> {
    return this.http.post<Bonus>(
      `${RR_API.backendUrl}${this.path(templateId)}/${creatureTypeId}/bonuses`,
      bonus
    );
  }

  public updateBonus(templateId: string, creatureTypeId: string, bonus: Bonus): Observable<Bonus> {
    return this.http.put<Bonus>(
      `${RR_API.backendUrl}${this.path(templateId)}/${creatureTypeId}/bonuses/${bonus.id}`,
      bonus
    );
  }

  public removeBonus(templateId: string, creatureTypeId: string, bonusId: string): Observable<void> {
    return this.http.delete<void>(
      `${RR_API.backendUrl}${this.path(templateId)}/${creatureTypeId}/bonuses/${bonusId}`
    );
  }
}
