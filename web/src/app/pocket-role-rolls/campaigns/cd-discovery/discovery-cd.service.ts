import { Injectable } from '@angular/core';
import { HttpClient } from '../../../../../node_modules/@angular/common/http/http';
import { LOH_API } from 'src/app/loh.api';
import { DiscoveryCdInput } from 'src/app/pocket-role-rolls/campaigns/cd-discovery/tokens/discovery-cd-input';
import { Observable } from '../../../../../node_modules/rxjs';
import { DiscoveryCdResult } from 'src/app/pocket-role-rolls/campaigns/cd-discovery/tokens/discovery-cd-result';

@Injectable({
  providedIn: 'root'
})
export class DiscoveryCdService {
  private serverUrl = LOH_API.myPocketBackUrl;
  constructor(
    private readonly httpClient: HttpClient
  ) { }

  
  getChance(campaignid: string, creatureId: string, input: DiscoveryCdInput): Observable<DiscoveryCdResult[]> {
    return this.httpClient.post<DiscoveryCdResult[]>(this.path(campaignid, creatureId), input).pipe(
    );
  }

  private path(campaignid: string, creatureId: string): string {
    return `${this.serverUrl}campaigns/${campaignid}/creatures/${creatureId}/chance`
  };

}
