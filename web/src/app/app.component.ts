import {Component} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {LOH_API} from './loh.api';
import {AuthenticationService} from './authentication/authentication.service';
import {ActivatedRoute, ActivatedRouteSnapshot} from '@angular/router';

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
