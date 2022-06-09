import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Basket } from '../models/basket';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  apiUrl: string = "https://localhost:7192/api/baskets";
  constructor(private httpClient:HttpClient) { }

  addBasket(basket:Basket){
    return this.httpClient.post<any>(this.apiUrl,basket);
  }

  getByAppUserId(userId:string){
    let newApiUrl = `${this.apiUrl}/GetByAppUserId/${userId}`;
    return this.httpClient.get<any>(newApiUrl);
  }

  deleteBasket(id:number){
    return this.httpClient.delete<any>(`${this.apiUrl}/${id}`);
  }
}
