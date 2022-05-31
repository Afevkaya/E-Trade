import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Category } from 'src/app/models/category';
import { Product } from 'src/app/models/product';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {

  product!:Product;
  category!:Category;
  constructor(public productService:ProductService,private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params=>{
      this.productService.loading = true;
      let id = Number(this.route.snapshot.paramMap.get("id"));
      this.productService.getProduct(id).subscribe(result=>{
        this.product = result.data;
        this.category = result.data.category;
      });
    });
  }

}
