import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {LOH_API} from '../loh.api';
import {MessageService} from 'primeng/api';

@Injectable({
  providedIn: 'root'
})
export class TestsService {
  serverUrl = LOH_API.myBackUrl;
  weaponTestPath = 'weaponTest';

  constructor(
    private messageService: MessageService,
  private httpClient: HttpClient
  ) { }


  public makeWeaponTest() {
    this.httpClient.get(this.serverUrl + this.weaponTestPath + '/makeTest').subscribe(() => this.messageService.add({severity: 'success', detail: 'completed'}), (error: any) => this.messageService.add({severity: 'error', detail: error.message}));
  }
  public test() {
    this.httpClient.get(this.serverUrl + 'glovesModels' + '/getNew').subscribe();
  }
}
