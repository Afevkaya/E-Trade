import { Injectable } from '@angular/core';
import { Router, CanActivate,ActivatedRouteSnapshot } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class RoleGuardService implements CanActivate {


  constructor(private auth: AuthService, private router: Router, private helperService:JwtHelperService) {}


  canActivate(route: ActivatedRouteSnapshot): boolean {

    const expectedRole = route.data.expectedRole;
    const accessToken = localStorage.getItem('accessToken') || '';

    const tokenPayload = this.helperService.decodeToken(accessToken);
    if (!this.auth.isAuthenticated() || tokenPayload.roles !== expectedRole) {
      this.router.navigate(['auth/adminlogin']);
      return false;
    }

    return true;
  }
}
