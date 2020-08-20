import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {LOH_API} from '../loh.api';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  serverUrl = LOH_API.myBackUrl;
  constructor(
    private httpClient: HttpClient
  ) { }

  login(email: string, password: string) {
    return this.httpClient.post(this.serverUrl + 'users/login', {
      username: email,
      password: password
    },
      {
        headers: new HttpHeaders({
          'Content-Type':  'application/json',
        })
      } );
  }
  addUser(username: string, password: string, email: string) {
    return this.httpClient.post(this.serverUrl + 'users/create', {
      username: username,
      password: password,
      email: email
    });
  }
  updateUser(username: string, password: string, email: string) {
    return this.httpClient.post(this.serverUrl + 'users/update', {
      username: username,
      password: password,
      email: email
    });
  }
}
