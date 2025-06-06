import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RR_API } from '@app/tokens/loh.api';
import { GetListInput } from '@app/tokens/get-list-input';
import { Encounter } from '@app/encounters/models/encounter';
import { Creature } from '@app/campaigns/models/creature';
import { PagedOutput } from '@app/models/PagedOutput';
import {Campaign} from '@app/campaigns/models/campaign';
import {DialogService} from 'primeng/dynamicdialog';
import {EncounterSelectComponent} from '@app/encounters/components/encounter-select/encounter-select.component';
export enum EncounterValidationMotive {
  Ok = 0,
  NotFound = 1
}

// encounter-validation-result.model.ts
export class EncounterValidationResult {
  motive: EncounterValidationMotive;
}
@Injectable({
  providedIn: 'root'
})
export class EncountersService {
  public path(campaignId: string): string {
    return `campaigns/${campaignId}/encounters`;
  };

  constructor(
    private http: HttpClient,
    private dialogService: DialogService,
  ) {
  }

  public getById(campaignId: string, encounterId: string): Observable<Encounter> {
    return this.http.get<Encounter>(`${RR_API.backendUrl}${this.path(campaignId)}/${encounterId}`);
  }

  public getAll(campaignId: string, input: GetListInput): Observable<PagedOutput<Encounter>> {
    let params = new HttpParams()
      .set('skipCount', input.skipCount.toString())
      .set('maxResultCount', input.maxResultCount.toString());

    if (input.filter) {
      params = params.set('filter', input.filter);
    }
    return this.http.get<PagedOutput<Encounter>>(`${RR_API.backendUrl}${this.path(campaignId)}`, {params});
  }
  public getAllCreatures(campaignId: string, encounterId: string, input: GetListInput): Observable<PagedOutput<Creature>> {
    let params = new HttpParams()
      .set('skipCount', input.skipCount.toString())
      .set('maxResultCount', input.maxResultCount.toString());

    if (input.filter) {
      params = params.set('filter', input.filter);
    }
    return this.http.get<PagedOutput<Creature>>(`${RR_API.backendUrl}${this.path(campaignId)}/${encounterId}/creatures`, {params});
  }
  public create(campaignId: string, encounter: Encounter): Observable<void> {
    return this.http.post<void>(`${RR_API.backendUrl}${this.path(campaignId)}`, encounter);
  }

  public update(campaignId: string, encounter: Encounter): Observable<void> {
    return this.http.put<void>(`${RR_API.backendUrl}${this.path(campaignId)}/${encounter.id}`, encounter);
  }

  public delete(campaignId: string, encounterId: string): Observable<EncounterValidationResult> {
    return this.http.delete<EncounterValidationResult>(`${RR_API.backendUrl}${this.path(campaignId)}/${encounterId}`);
  }

  public addCreature(campaignId: string, encounterId: string, creature: Creature): Observable<EncounterValidationResult> {
    return this.http.post<EncounterValidationResult>(
      `${RR_API.backendUrl}${this.path(campaignId)}/${encounterId}/creatures`,
      creature
    );
  }

  public removeCreature(campaignId: string, encounterId: string, creatureId: string, deleteCreature: boolean = false): Observable<EncounterValidationResult> {
    let params = new HttpParams().set('delete', deleteCreature.toString());

    return this.http.delete<EncounterValidationResult>(
      `${RR_API.backendUrl}${this.path(campaignId)}/${encounterId}/creatures/${creatureId}`,
      { params }
    );
  }
  public openSelectEncounter(campaign: Campaign) {
    return this.dialogService.open(EncounterSelectComponent, {
      width: '60vw',
      height: '900vh',
      data: {
        campaign: campaign,
      },
      focusOnShow: false,
      closable: true,
    }).onClose
  }
}
