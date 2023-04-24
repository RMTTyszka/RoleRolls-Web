import { Injectable } from "@angular/core";
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router, CanActivateChild } from "@angular/router";
import { AuthenticationService } from '../../authentication/authentication.service';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
  })
export class CanActivateAuthGuard implements CanActivateChild {
  constructor(private authService: AuthenticationService, public router: Router) {}

  canActivateChild(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean|UrlTree>|Promise<boolean|UrlTree>|boolean|UrlTree {
    if (this.authService.isLogged) {
        return true
    }
    this.router.navigate(['pocket/login'])
    return false;
  }
}