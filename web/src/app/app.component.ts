import {Component, ViewEncapsulation} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {LOH_API} from './loh.api';

@Component({
  selector: 'rr-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  serverUrl = LOH_API.myBackUrl;
  constructor(
    private httpClient: HttpClient,
  ) {

  }

  resetDummies() {
    this.httpClient.post(this.serverUrl + 'creature/deleteDummies', {}).subscribe();
  }
}
