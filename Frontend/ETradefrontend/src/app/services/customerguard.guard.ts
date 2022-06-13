import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { RefreshToken } from '../models/refresh-token';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class CustomerguardGuard implements CanActivate {
  refreshToken!: RefreshToken;
  constructor(
    public authService: AuthService,
    public router: Router,
    private helperService: JwtHelperService
  ) {}

  canActivate(route: ActivatedRouteSnapshot) {
    const expectedRole = route.data.expectedRole;
    const accessToken = localStorage.getItem('accessToken') || '';
    const refreshToken = localStorage.getItem('refreshToken');

    const tokenPayload = this.helperService.decodeToken(accessToken);
    const refreshTokenExpiration = localStorage.getItem(
      'refreshTokenExpiration'
    );


    let now = new Date();
    let resfreshDate;

    // Otantike değilse
    if (!this.authService.isAuthenticated()) {
      // RefreshToken kontrolü
      if (refreshToken == null) {
        this.router.navigate(['auth/customerlogin']);
        return false;
      }
      // RefreshToken varsa tarih kontrolü
      else {
        resfreshDate = new Date(refreshTokenExpiration || now);
        if (now.toISOString() > resfreshDate.toISOString()) {
          this.router.navigate(['auth/customerlogin']);
          return false;
        }
      }

      this.refreshToken = new RefreshToken();
      this.refreshToken.token = refreshToken || '';

      // refreshtoken'a göre yeni token üretme
      this.authService
        .createTokenByRefreshtoken(this.refreshToken)
        .subscribe((result) => {
          console.log(result.data);
          localStorage.setItem('accessToken', result.data.accessToken);
          localStorage.setItem('refreshToken', result.data.refreshToken);
          localStorage.setItem(
            'accessTokenExpiration',
            result.data.accessTokenExpiration
          );
          localStorage.setItem(
            'refreshTokenExpiration',
            result.data.refreshTokenExpiration
          );
        });
    }
    if(tokenPayload.roles != null){
      if (tokenPayload.roles !== expectedRole) {
        this.router.navigate(['auth/customerlogin']);
        return false;
      }
    }
    return true;
  }
}
