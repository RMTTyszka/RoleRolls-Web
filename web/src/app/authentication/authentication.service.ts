import { Injectable } from '@angular/core';
import {LohAuthTokenName, LohAuthUserName} from './AuthTokens';
import {pipe, Subject} from 'rxjs';
import {Router} from '@angular/router';
import {Message, MessageService} from 'primeng/api';
import {debounceTime, tap, throttleTime} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  userName: string;
  token: string;
  userNameChanged = new Subject<string>();
  onUserUnauthorized = new Subject<string>()
  get isLogged() {
    return this.token && this.userName;
  }
  constructor(
    private router: Router, private messageService: MessageService
  ) {
    this.onUserUnauthorized
      .pipe(tap(() => {
          this.router.navigateByUrl(`/home`);
          this.cleanTokenAndUserName();
        }),
        throttleTime(10000))
      .subscribe((message: string) => {
      this.notifyUserAboutUnauthorizedAccess(message);
    });
  }

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


  private  notifyUserAboutUnauthorizedAccess(message: string) {
    this.messageService.add(<Message>{
      severity: 'error',
      summary: 'Non Authorized',
      details: message
    });
  }
}
