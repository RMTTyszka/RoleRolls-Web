import { Injectable } from '@angular/core';
import {LohAuthLoggedOnApp, LohAuthTokenName, LohAuthUserId, LohAuthUserName} from './AuthTokens';
import {pipe, Subject} from 'rxjs';
import {ActivatedRoute, ActivatedRouteSnapshot, Router} from '@angular/router';
import {ToastMessageOptions, MessageService} from 'primeng/api';
import {debounceTime, tap, throttleTime} from 'rxjs/operators';
import { LoggedApp } from '../shared/models/login/LoggedApp';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  userName: string;
  userId: string;
  token: string;
  userNameChanged = new Subject<string>();
  onUserUnauthorized = new Subject<string>();
  lastRoute: string;
  public loggedOn: LoggedApp;
  get isLogged() {
    return Boolean(this.token && this.userName);
  }
  constructor(
    private router: Router, private messageService: MessageService, private activatedRoute: ActivatedRoute
  ) {
    this.onUserUnauthorized
      .pipe(tap(() => {
        if ( this.router.routerState.snapshot.url !== '/home') {
          this.lastRoute = this.router.routerState.snapshot.url;
          this.cleanTokenAndUserName();
          this.router.navigateByUrl(`/home`);
        }
        }),
        throttleTime(10000))
      .subscribe((message: string) => {
      this.notifyUserAboutUnauthorizedAccess(message);
    });
    this.getUser();
  }

  public setToken(token: string) {
    localStorage.setItem(LohAuthTokenName, token);
    localStorage.setItem(LohAuthLoggedOnApp, this.loggedOn.toString());
    this.token = token;
  }
  public getToken() {
    return localStorage.getItem(LohAuthTokenName);
  }
  public isMaster(masterId: string): boolean {
    return this.userId === masterId;
  }

  public publishNewUserName(userName: string, userId: string) {
    localStorage.setItem(LohAuthUserName, userName);
    localStorage.setItem(LohAuthUserId, userId);
    this.userNameChanged.next(userName);
    this.userName = userName;
    this.userId = userId;
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
    const userId = localStorage.getItem(LohAuthUserId);
    if (userId) {
      this.userId = userId;
    }
    const loggedOn = localStorage.getItem(LohAuthLoggedOnApp);
    if (loggedOn) {
      this.loggedOn = loggedOn as unknown as LoggedApp;
    }

  }
  public cleanTokenAndUserName() {
    this.token = null;
    this.userName = null;
    this.userNameChanged.next(null);
    this.loggedOn = null;
    localStorage.removeItem(LohAuthTokenName);
    localStorage.removeItem(LohAuthUserName);
    localStorage.removeItem(LohAuthLoggedOnApp);
  }


  private  notifyUserAboutUnauthorizedAccess(message: string) {
    this.messageService.add(<ToastMessageOptions>{
      severity: 'error',
      summary: 'Non Authorized',
      details: message
    });
  }
}
