import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from "@angular/common/http";
import { MainModule } from './main-pages/main.module';
import { AdminModule } from "./admin-pages/admin.module";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';


import { ProductService } from './services/product.service';
import { CategoryService } from './services/category.service';
import { AuthService } from './services/auth.service';
import { AuthModule } from './auth-pages/auth.module';


@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MainModule,
    AdminModule,
    AuthModule
  ],
  providers: [ProductService,CategoryService,AuthService],
  bootstrap: [AppComponent]
})
export class AppModule { }
