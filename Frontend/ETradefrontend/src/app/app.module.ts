import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from "@angular/common/http";

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuthLayoutComponent } from './layouts/auth-layout/auth-layout.component';
import { LoginComponent } from "./auth-pages/login/login.component";
import { SigninComponent } from "./auth-pages/signin/signin.component";
import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';
import { MainLayoutComponent } from './layouts/main-layout/main-layout.component';
import { HomeComponent } from './main-pages/home/home.component';
import { MyaccountComponent } from './main-pages/myaccount/myaccount.component';
import { MainNavComponent } from './nav/main-nav/main-nav.component';
import { ProductService } from './services/product.service';
import { CategoryService } from './services/category.service';
import { MenuCategoryComponent } from './components/menu-category/menu-category.component';

@NgModule({
  declarations: [
    AppComponent,
    AuthLayoutComponent,
    AdminLayoutComponent,
    MainLayoutComponent,
    LoginComponent,
    SigninComponent,
    HomeComponent,
    MyaccountComponent,
    MainNavComponent,
    MenuCategoryComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
  ],
  providers: [ProductService,CategoryService],
  bootstrap: [AppComponent]
})
export class AppModule { }
