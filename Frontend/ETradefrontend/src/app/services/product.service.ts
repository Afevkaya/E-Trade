import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";

@Injectable()
export class ProductService {

  private apiUrl: string = "https://localhost:7192/api/products";


  constructor(private httpClient:HttpClient)
  {

  }

  // Tüm productları getirme
  // Json tipinde bir obje dönüyor.
  getProducts(){
    return this.httpClient.get<any>(this.apiUrl);
  }

  // id'ye göre product getirme
  getProductsById(id:number){
    let newApiUrl = `${this.apiUrl}/${id}`;
    return this.httpClient.get<any>(newApiUrl);
  }
}
