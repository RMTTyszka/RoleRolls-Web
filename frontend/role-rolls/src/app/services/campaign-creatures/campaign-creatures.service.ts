import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RR_API } from '@app/tokens/loh.api';
import { PagedOutput } from '@app/models/PagedOutput';
import { Creature } from '@app/campaigns/models/creature';

@Injectable({
  providedIn: 'root'
})
export class CampaignCreaturesService {
  public path(campaignId: string): string {
    return `campaigns/${campaignId}/creatures`;
  };

  constructor(
    private http: HttpClient,
  ) {
  }
  public getById(campaignId: string, creatureId: string): Observable<Creature> {
    return this.http.get<Creature>(`${RR_API.backendUrl}${this.path(campaignId)}/${creatureId}`);
  }

  public getList(campaignId: string, query: { [key: string]: any }): Observable<PagedOutput<Creature>> {
    const params = Object.entries(query).reduce((httpParams, [key, value]) => {
      if (value !== undefined && value !== null) {
        return httpParams.set(key, value.toString());
      }
      return httpParams;
    }, new HttpParams());
    return this.http.get<PagedOutput<Creature>>(`${RR_API.backendUrl}${this.path(campaignId)}`, { params });
  }
  public create(campaignId: string, creature: Creature): Observable<void> {
    return this.http.post<void>(
      `${RR_API.backendUrl}${this.path(campaignId)}/`,
      creature
    );
  }

  public update(campaignId: string, creatureId: string, creature: Creature): Observable<void> {
    return this.http.put<void>(
      `${RR_API.backendUrl}${this.path(campaignId)}/${creatureId}/`,
      creature
    );
  }

  public remove(campaignId: string, creatureId: string): Observable<void> {
    return this.http.delete<void>(
      `${RR_API.backendUrl}${this.path(campaignId)}/${creatureId}`
    );
  }
}
