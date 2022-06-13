import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BrowserModule } from "@angular/platform-browser";
import { HttpClientModule } from "@angular/common/http";
import { AppRoutingModule } from "../app-routing.module";
import { ComponentsModule } from "../components/components.module";
import { FormsModule } from "@angular/forms";
import { MaterialModule } from '../modules/material.module';


import { MainLayoutComponent } from '../layouts/main-layout/main-layout.component';
import { MainNavComponent } from '../nav/main-nav/main-nav.component';
import { HomeComponent } from './home/home.component';
import { MyaccountComponent } from './myaccount/myaccount.component';
import { ProductComponent } from './product/product.component';
import { CategoryProductsComponent } from './category-products/category-products.component';
import { SearchComponent } from './search/search.component';



@NgModule({
  declarations: [MainLayoutComponent,MainNavComponent,HomeComponent,MyaccountComponent, ProductComponent, CategoryProductsComponent, SearchComponent],
  imports: [
    CommonModule,
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    ComponentsModule,
    FormsModule,
    MaterialModule,
  ]
})
export class MainModule { }

// components.module > main.module > app.module
