import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { LoginService } from './login.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService implements CanActivate {

  constructor(private loginService: LoginService, private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    if (this.loginService.getToken() == null || this.loginService.getToken() == '') {
      this.router.navigate(['login']);
      return false;
    }
    if (this.loginService.getUserType() == '1' && state.url.includes('admin')) {
      this.router.navigate(['login']);
      return false;
    }
    if (this.loginService.getUserType() == '2' && state.url.includes('customer')) {
      this.router.navigate(['login']);
      return false;
    }

    return true;
  }

}
