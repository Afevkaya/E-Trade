import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ProductService } from 'src/app/services/product.service';
import { Product } from "../../../models/product";

@Component({
  selector: 'app-admin-product-list',
  templateUrl: './admin-product-list.component.html',
  styleUrls: ['./admin-product-list.component.css']
})
export class AdminProductListComponent implements OnInit {

  displayColumns: string[] = ['name','price','description','imageUrl','stockQuantity','categoryId'];
  dataSource:any;
  products:Product[] = [];
  @ViewChild(MatPaginator, {static:true}) paginator!:MatPaginator;
  constructor(private productService:ProductService) { }

  ngOnInit(): void {
    this.productService.getProductsWithCategory().subscribe(result=>{
      //this.products = result.data;
      this.dataSource = new MatTableDataSource<Product>(result.data);
    })
  }

}
