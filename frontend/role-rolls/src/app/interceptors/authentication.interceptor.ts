import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor, HttpErrorResponse
} from '@angular/common/http';
import {Observable, of, throwError} from 'rxjs';
import {AuthenticationService} from '../authentication/services/authentication.service';
import {catchError} from 'rxjs/operators';

@Injectable()
export class AuthenticationInterceptor implements HttpInterceptor {

  constructor(private readonly service: AuthenticationService
  ) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const token = this.service.getToken();
    if (token) {
      const authReq = request.clone({
        headers: request.headers.set('Authorization', token)
      });
      return next.handle(authReq).pipe(catchError(x => this.handleAuthError(x)));
    } else {
      return next.handle(request).pipe(catchError(x => this.handleAuthError(x)));
    }
  }
  private handleAuthError(err: HttpErrorResponse): Observable<any> {
    if (err.status === 401 || err.status === 403) {
      this.service.onUserUnauthorized.next(err.message);
      return of(err.message); // or EMPTY may be appropriate here
    }
    return throwError(err);
  }
}
