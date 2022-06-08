import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Customer } from '../models/customer';

@Injectable({
  providedIn: 'root'
})
export class SigninService {

  apiUrl: string = "https://localhost:7192/api/users";
  constructor(private httpClient:HttpClient) { }

  addCustomer(customer:Customer){
    return this.httpClient.post<any>(this.apiUrl,customer);
  }
}
