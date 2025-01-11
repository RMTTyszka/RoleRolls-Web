import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';

import { tap } from 'rxjs/operators';
import { RR_API } from '../../../tokens/loh.api';
import { AuthenticationService } from '../../services/authentication.service';
import { Observable } from 'rxjs';
import { LoginOutput } from '../models/loginOutput';
import { CreateUserInput } from '../models/createUserInput';
import { CreateUserOutput } from '../models/createUserOutput';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  serverUrl = RR_API.backendUrl;
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
      } );
  }
  addUser(input: CreateUserInput): Observable<never> {
    return this.httpClient.post<never>(this.serverUrl + 'users', input);
  }
  updateUser(username: string, password: string, email: string) {
    return this.httpClient.put(this.serverUrl + 'users', {
      username: username,
      password: password,
      email: email
    });
  }
}
