import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { AuthenticationService } from '../authentication/authentication.service';
import { CreateUserInput } from '../login/createUserInput';
import { LoginOutput } from '../login/login/loginOutput';
import { LOH_API } from '../loh.api';
import { LoggedApp } from '../shared/models/login/LoggedApp';

@Injectable({
  providedIn: 'root'
})
export class PocketLoginService {
  serverUrl = LOH_API.myPocketBackUrl;
  constructor(
    private httpClient: HttpClient,
    private authenticationService: AuthenticationService
  ) { }

  login(email: string, password: string): Observable<LoginOutput> {
    return this.httpClient.post<LoginOutput>(this.serverUrl + 'users/login', {
        email: email,
        password: password
    },
      {
        headers: new HttpHeaders({
          'Content-Type':  'application/json',
        })
      } )
      .pipe((tap((loginOutput: LoginOutput) => {
        this.authenticationService.lastRoute = '/pocket/campaings';
        this.authenticationService.loggedOn = LoggedApp.Pocket;
        this.authenticationService.setToken(loginOutput.token);
      })));
  }
  addUser(input: CreateUserInput): Observable<never> {
    return this.httpClient.post<never>(this.serverUrl + 'users', input);
  }
}
