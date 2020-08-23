import { Injectable } from '@angular/core';
import {LohAuthTokenName, LohAuthUserName} from './AuthTokens';
import {Subject} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  userName: string;
  token: string;
  userNameChanged = new Subject<string>();

  get isLogged() {
    return this.token && this.userName;
  }
  constructor() { }

  public setToken(token: string) {
    localStorage.setItem(LohAuthTokenName, token);
    this.token = token;
  }
  public getToken() {
    return localStorage.getItem(LohAuthTokenName);
  }

  public publishNewUserName(userName: string) {
    localStorage.setItem(LohAuthUserName, userName);
    this.userNameChanged.next(userName);
    this.userName = userName;
  }
  public getUser() {
    const userName = localStorage.getItem(LohAuthUserName);
    if (userName) {
      this.userNameChanged.next(userName);
      this.userName = userName;
    }
    const token = localStorage.getItem(LohAuthTokenName);
    if (token) {
      this.token = token;
    }
  }
  public cleanTokenAndUserName() {
    this.token = null;
    this.userName = null;
    this.userNameChanged.next(null);
    localStorage.removeItem(LohAuthTokenName);
    localStorage.removeItem(LohAuthUserName);
  }
}
