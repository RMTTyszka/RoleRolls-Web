import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {LOH_API} from '../loh.api';
import {Observable} from 'rxjs';
import {CreateUserOutput} from './createUserOutput';
import {CreateUserInput} from './createUserInput';
import {LoginOutput} from './login/loginOutput';
import { AuthenticationService } from '../authentication/authentication.service';
import { LoggedApp } from '../shared/models/login/LoggedApp';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  serverUrl = LOH_API.myBackUrl;
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
      } ).pipe(tap(() => {
        this.authenticationService.loggedOn = LoggedApp.Normal;
      }));
  }
  addUser(input: CreateUserInput): Observable<CreateUserOutput> {
    return this.httpClient.post<CreateUserOutput>(this.serverUrl + 'users/create', input);
  }
  updateUser(username: string, password: string, email: string) {
    return this.httpClient.post(this.serverUrl + 'users/update', {
      username: username,
      password: password,
      email: email
    });
  }
}
