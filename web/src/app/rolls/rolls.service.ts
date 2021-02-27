import {Injectable} from '@angular/core';
import {LOH_API} from '../loh.api';
import {HttpClient, HttpParams} from '@angular/common/http';
import {DCChance} from '../shared/models/rolls/DCChanceResult';

@Injectable({
  providedIn: 'root'
})
export class RollsService {
  private basePath = LOH_API.myBackUrl;
  private path: 'rolls'
  constructor(
    private httpClient: HttpClient
  ) { }


}
