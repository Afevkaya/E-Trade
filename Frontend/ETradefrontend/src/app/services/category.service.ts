import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Category } from "../models/category";

@Injectable()
export class CategoryService {

  private apiUrl: string = "https://localhost:7192/api/categories";

  constructor(private httpClient: HttpClient) { }

  getCategories(){
    return this.httpClient.get<any>(this.apiUrl);
  }
}
