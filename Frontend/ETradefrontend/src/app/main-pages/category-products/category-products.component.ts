import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Product } from 'src/app/models/product';
import { CategoryService } from 'src/app/services/category.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-category-products',
  templateUrl: './category-products.component.html',
  styleUrls: ['./category-products.component.css']
})
export class CategoryProductsComponent implements OnInit {

  constructor(private route:ActivatedRoute, private categoryService:CategoryService, private productService:ProductService) { }
  products:Product[] = [];
  categoryId!:number;
  ajax:any;

  ngOnInit(): void {
    this.route.paramMap.subscribe(params=>{
      if (this.ajax != null) {
        this.ajax.unsubscribe();
      }
      this.productService.loading = true;
      this.products = [];
      if (params.get("id")) {
        this.categoryId = Number(params.get("id"));
      }
      this.ajax = this.categoryService.getCategoryWithProducts(this.categoryId).subscribe(result=>{
        this.products = result.data.products;
      });
    });
  }

}
