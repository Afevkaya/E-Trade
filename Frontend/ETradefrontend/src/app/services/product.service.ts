import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { tap } from "rxjs/operators";
import { Product } from '../models/product';

@Injectable()
export class ProductService {

  public loading:boolean = true;
  private apiUrl: string = "https://localhost:7192/api/products";


  constructor(private httpClient:HttpClient){ }

  // Tüm productları getirme
  // Json tipinde bir obje dönüyor.
  // loading değişkeni datanın gelip gelmesiğini kontrol ediyor.
  getProducts(){
    return this.httpClient.get<any>(this.apiUrl).pipe(tap(x=>{
      this.loading = false;
    }));
  }

  getProduct(id:number){
    let newApiUrl = `${this.apiUrl}/${id}`;
    return this.httpClient.get<any>(newApiUrl).pipe(tap(x=>{
      this.loading = false;
    }));
  }

  getProductByCategory(id:number){
    let newApiUrl = `${this.apiUrl}/${id}`;
    return this.httpClient.get<any>(newApiUrl).pipe(tap(x=>{
      this.loading = false;
    }));
  }

  getSearchProduct(searchText:string){
    let newApiUrl = `${this.apiUrl}/Search/${searchText}`;
    return this.httpClient.get<any>(newApiUrl).pipe(tap(x=>{
      this.loading = false;
    }));
  }

  getProductsWithCategory(){
    let newApiUrl = `${this.apiUrl}/ProductsWithCategory`
    return this.httpClient.get<any>(newApiUrl);
  }

  addProduct(product:Product){
    return this.httpClient.post<any>(`${this.apiUrl}`,product);
  }

  updateProduct(product:Product){
    return this.httpClient.put<any>(`${this.apiUrl}`,product);
  }

  deleteProduct(id:number){
    return this.httpClient.delete<any>(`${this.apiUrl}/${id}`);
  }
}
