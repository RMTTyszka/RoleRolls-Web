import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {LOH_API} from '../loh.api';

@Injectable({
  providedIn: 'root'
})
export class TestsService {
  serverUrl = LOH_API.myBackUrl;
  weaponTestPath = 'weaponTest';

  constructor(
    private httpClient: HttpClient
  ) { }


  public makeWeaponTest() {
    return this.httpClient.get(this.serverUrl + this.weaponTestPath + '/makeTest');
  }
  public test() {
    this.httpClient.get(this.serverUrl + 'glovesModels' + '/getNew').subscribe();
  }
}
