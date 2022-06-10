import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Basket } from 'src/app/models/basket';
import { Category } from 'src/app/models/category';
import { Product } from 'src/app/models/product';
import { BasketService } from 'src/app/services/basket.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css'],
})
export class ProductComponent implements OnInit {
  product!: Product;
  category!: Category;
  basket!: Basket;
  productQuantity: number = 1;
  constructor(
    public productService: ProductService,
    private route: ActivatedRoute,
    private helperService: JwtHelperService,
    private basketService: BasketService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      this.productService.loading = true;
      let id = Number(this.route.snapshot.paramMap.get('id'));
      this.productService.getProduct(id).subscribe((result) => {
        this.product = result.data;
        this.category = result.data.category;
      });
    });
  }

  add(productId: number, productPrice: number) {
    const accessToken = localStorage.getItem('accessToken') || '';
    const tokenPayload = this.helperService.decodeToken(accessToken);
    const userId = tokenPayload.userId;

    this.basket = new Basket();
    this.basket.productId = productId;
    this.basket.appUserId = userId;
    this.basket.productQuantity = this.productQuantity;
    this.basket.productPrice = productPrice;

    if(this.productQuantity <= 0){
      alert("Ürün adedi 1 den küçük olamaz");
      this.router.navigate(["/"]);
    }

    this.basketService.addBasket(this.basket).subscribe((result) => {
      alert(result.data.productName + ' sepete eklendi');
      this.router.navigate(["/hesabim"]);
    });
  }
}
