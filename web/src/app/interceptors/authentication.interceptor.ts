import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import {LohAuthTokenName} from '../authentication/AuthTokens';

@Injectable()
export class AuthenticationInterceptor implements HttpInterceptor {

  constructor() {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const token = localStorage.getItem(LohAuthTokenName)
    console.log(token);
    if (token) {
      const authReq = request.clone({
        headers: request.headers.set('Authorization', token)
      });
      return next.handle(authReq);
    } else {
      return next.handle(request);
    }

  }
}
