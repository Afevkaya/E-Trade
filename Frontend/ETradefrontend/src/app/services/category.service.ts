import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Category } from "../models/category";
import { tap } from 'rxjs/operators';
import { ProductService } from './product.service';

@Injectable()
export class CategoryService {

  private apiUrl: string = "https://localhost:7192/api/categories";

  constructor(private httpClient: HttpClient,private productService:ProductService) { }

  // Backend tarafında bir yapı dönüyor.
  // Bu yapı içinde success ise data değilse error geliyor.
  // Bu yüzden dönüş tipi any
  getCategories(){
    return this.httpClient.get<any>(this.apiUrl);
  }

  getCategoryWithProducts(categoryId:number){
    let newApiUrl = `${this.apiUrl}/GetCategoryWithProducts/${categoryId}`;
    return this.httpClient.get<any>(newApiUrl).pipe(tap(x=>{
      this.productService.loading = false;
    }));
  }
}
