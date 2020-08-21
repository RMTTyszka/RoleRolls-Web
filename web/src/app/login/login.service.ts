import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {LOH_API} from '../loh.api';
import {Observable} from 'rxjs';
import {CreateUserOutput} from './createUserOutput';
import {CreateUserInput} from './createUserInput';
import {LoginOutput} from './login/loginOutput';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  serverUrl = LOH_API.myBackUrl;
  constructor(
    private httpClient: HttpClient
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
      } );
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
