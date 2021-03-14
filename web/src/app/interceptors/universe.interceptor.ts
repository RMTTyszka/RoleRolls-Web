import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import {UniverseService} from '../universes/universe.service';

@Injectable()
export class UniverseInterceptor implements HttpInterceptor {

  constructor(
    private universeService: UniverseService
  ) {
  }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const universe = this.universeService.universe;
      request = request.clone({
        headers: request.headers.set('universe-type', universe)
      });
      return next.handle(request);
  }
}
