import { Component, OnDestroy, OnInit } from '@angular/core';
import { Product } from 'src/app/models/product';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, OnDestroy {

  products: Product[] = [];
  ajax: any;

  constructor(private productService: ProductService) { }


  ngOnInit(): void {
    this.ajax = this.productService.getProducts().subscribe(result=>{
      console.log(result.data);
      this.products = result.data;
    });
  }

  ngOnDestroy(): void {
    if (this.ajax != null) {
      this.ajax.unsubscribe();
    }
  }

}
