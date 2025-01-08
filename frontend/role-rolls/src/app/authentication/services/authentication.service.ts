import { Injectable } from '@angular/core';
import {pipe, Subject} from 'rxjs';
import {ActivatedRoute, ActivatedRouteSnapshot, Router} from '@angular/router';
import {ToastMessageOptions, MessageService} from 'primeng/api';
import {debounceTime, tap, throttleTime} from 'rxjs/operators';
import { AuthTokenName, AuthUserId, AuthUserName } from '../tokens/AuthTokens';
import { StorageService } from '../../services/storage.service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  userName: string | null = null;
  userId: string | null = null;
  token: string | null = null;
  userNameChanged = new Subject<string | null>();
  onUserUnauthorized = new Subject<string>();
  lastRoute: string | null = null;
  get isLogged() {
    return Boolean(this.token && this.userName);
  }
  constructor(
    private router: Router,
    private messageService: MessageService,
    private storageService: StorageService,
    private activatedRoute: ActivatedRoute
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
    this.storageService.setItem(AuthTokenName, token);
    this.token = token;
  }
  public getToken() {
    return this.storageService.getItem(AuthTokenName);
  }
  public isMaster(masterId: string): boolean {
    return this.userId === masterId;
  }

  public publishNewUserName(userName: string, userId: string) {
    this.storageService.setItem(AuthUserName, userName);
    this.storageService.setItem(AuthUserId, userId);
    this.userNameChanged.next(userName);
    this.userName = userName;
    this.userId = userId;
  }
  public getUser() {
    const userName = this.storageService.getItem(AuthUserName);
    if (userName) {
      this.userNameChanged.next(userName);
      this.userName = userName;
    }
    const token = this.storageService.getItem(AuthTokenName);
    if (token) {
      this.token = token;
    }
    const userId = this.storageService.getItem(AuthUserId);
    if (userId) {
      this.userId = userId;
    }
  }
  public cleanTokenAndUserName() {
    this.token = null;
    this.userName = null;
    this.userNameChanged.next(null);
    this.storageService.removeItem(AuthTokenName);
    this.storageService.removeItem(AuthUserName);
  }


  private  notifyUserAboutUnauthorizedAccess(message: string) {
    this.messageService.add(<ToastMessageOptions>{
      severity: 'error',
      summary: 'Non Authorized',
      details: message
    });
  }
}
