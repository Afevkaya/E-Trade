import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Customer } from '../models/customer';

@Injectable({
  providedIn: 'root'
})
// Customer signin services
// Backend ile bağlantı
export class SigninService {

  apiUrl: string = "https://localhost:7192/api/users";
  constructor(private httpClient:HttpClient) { }

  // customer ekleme metodu
  addCustomer(customer:Customer){
    return this.httpClient.post<any>(this.apiUrl,customer);
  }
}
