import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs';
import {PagedOutput} from '../../shared/dtos/PagedOutput';
import {EquipableTemplate} from '../../shared/models/items/EquipableTemplate';
import {LOH_API} from '../../loh.api';
import {Combat} from '../../shared/models/combat/Combat.model';

@Injectable()
export class CampaignEncounterService {
  basePath = LOH_API.myBackUrl;

  constructor(
    private httpClient: HttpClient
  ) { }

  instantiate(campaignId: string, encounterId: string): Observable<Combat> {
    return this.httpClient.post<Combat>(this.basePath + `combat-from-encounter/${campaignId}`, {
      encounterId: encounterId
    });
  }
}
