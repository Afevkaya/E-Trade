import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Product } from 'src/app/models/product';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css'],
})
export class SearchComponent implements OnInit {
  products: Product[] = [];
  searchText!: string;
  ajax: any;
  constructor(
    private route: ActivatedRoute,
    private productService: ProductService
  ) {}

  ngOnInit(): void {
    this.route.url.subscribe(params => {
      if (this.ajax != null) {
        this.ajax.unsubscribe();
      }

      this.products = [];
      this.productService.loading = true;
      this.searchText = String(this.route.snapshot.queryParamMap.get('s'));
      this.ajax = this.productService
        .getSearchProduct(this.searchText)
        .subscribe((result) => {
          this.products = result.data;
        });
    });
  }
}
