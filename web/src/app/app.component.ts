import {Component, OnInit, ViewEncapsulation} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {LOH_API} from './loh.api';
import { AuthenticationService } from './authentication/authentication.service';
import { LoggedApp } from './shared/models/login/LoggedApp';

@Component({
  selector: 'rr-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  serverUrl = LOH_API.myBackUrl;

  public loggedOnEnum = LoggedApp;
  get loggedOn() {
    return this.authenticationService.loggedOn;
  }
  constructor(
    private httpClient: HttpClient,
    private authenticationService: AuthenticationService,
  ) {

  }
  public ngOnInit(): void {
    if (this.loggedOn) {
      this.authenticationService.loggedOn = LoggedApp.Pocket;
    }
  }

  resetDummies() {
    this.httpClient.post(this.serverUrl + 'creature/deleteDummies', {}).subscribe();
  }
}
