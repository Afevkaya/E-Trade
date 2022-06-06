import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Login } from '../models/login';

@Injectable()
export class AuthService {

  private apiUrl: string = 'https://localhost:7192/api/auths';
  constructor(private httpClient:HttpClient) {}

  createToken(login:Login){
    return this.httpClient.post<any>(`${this.apiUrl}/CreateToken`,login);
  }
}
