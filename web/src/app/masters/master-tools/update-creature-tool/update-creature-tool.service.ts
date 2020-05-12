import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Creature} from '../../../shared/models/creatures/Creature.model';
import {LOH_API} from '../../../loh.api';
import {Observable} from 'rxjs';
import {UpdateCreatureLifeOrMoralInput} from '../../../shared/models/inputs/UpdateCreatureLifeOrMoralInput';
import {RemoveEffectInput} from '../../../shared/models/inputs/RemoveEffectInput';
import {UpdateEffectInput} from '../../../shared/models/inputs/UpdateEffectInput';

@Injectable({
  providedIn: 'root'
})
export class UpdateCreatureToolService {
  serverUrl = LOH_API.myBackUrl;
  path = 'masterTools';
  constructor(private httpClient: HttpClient) { }

  public updateLife(input: UpdateCreatureLifeOrMoralInput): Observable<Creature> {
    return this.httpClient.post<Creature>(this.serverUrl + this.path + '/updateLife', input);
  }
  public updateMoral(input: UpdateCreatureLifeOrMoralInput): Observable<Creature> {
    return this.httpClient.post<Creature>(this.serverUrl + this.path + '/updateMoral', input);
  }
  public heal(input: UpdateCreatureLifeOrMoralInput): Observable<Creature> {
    return this.httpClient.post<Creature>(this.serverUrl + this.path + '/heal', input);
  }
  public removeEffect(input: RemoveEffectInput): Observable<Creature> {
    return this.httpClient.post<Creature>(this.serverUrl + this.path + '/removeEffect', input);
  }
  public updateEffect(input: UpdateEffectInput): Observable<Creature> {
    return this.httpClient.post<Creature>(this.serverUrl + this.path + '/updateEffect', input);
  }
}

