import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Login } from '../models/login';

@Injectable()
export class AuthService {

  private apiUrl: string = 'https://localhost:7192/api/auths';
  constructor(private httpClient:HttpClient, public jwtHelper:JwtHelperService) {}

  createToken(login:Login){
    return this.httpClient.post<any>(`${this.apiUrl}/CreateToken`,login);
  }

  public isAuthenticated(): boolean {
    const accessToken = localStorage.getItem('accessToken') || '';
    return !this.jwtHelper.isTokenExpired(accessToken);
  }

  // isLoggedIn(){
  //   return localStorage.getItem('accessToken')!=null;
  // }
}
