import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class CustomerguardGuard implements CanActivate {
  /**
   *
   */
   constructor(public authService:AuthService, public router:Router,private helperService:JwtHelperService) { }

   canActivate(route: ActivatedRouteSnapshot) {

     const expectedRole = route.data.expectedRole;
     const accessToken = localStorage.getItem('accessToken') || '';

     const tokenPayload = this.helperService.decodeToken(accessToken);

     if(!this.authService.isAuthenticated() || tokenPayload.roles !== expectedRole){
       this.router.navigate(['auth/customerlogin']);
       return false;
     }
     return true;

     // if(this.authService.isLoggedIn()){
     //   return true;
     // }
     // else{
     //   this.router.navigate(["auth/adminlogin"]);
     //   return false;
     // }

   }
}
