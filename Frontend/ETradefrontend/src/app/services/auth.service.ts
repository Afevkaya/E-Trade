import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Login } from '../models/login';
import { RefreshToken } from '../models/refresh-token';

@Injectable()
// Auth service class
// Backend ile bağlanti kuracak class
export class AuthService {

  private apiUrl: string = 'https://localhost:7192/api/auths';
  constructor(private httpClient:HttpClient, public jwtHelper:JwtHelperService) {}

  // login metod
  createToken(login:Login){
    return this.httpClient.post<any>(`${this.apiUrl}/CreateToken`,login);
  }

  createTokenByRefreshtoken(refreshToken:RefreshToken){
    console.log(refreshToken);
    return this.httpClient.post<any>(`${this.apiUrl}/CreateTokenByRefreshToken`, refreshToken);
  }

  public isAuthenticated(): boolean {
    const accessToken = localStorage.getItem('accessToken') || '';
    return !this.jwtHelper.isTokenExpired(accessToken);
  }

  // isLoggedIn(){
  //   return localStorage.getItem('accessToken')!=null;
  // }
}
