import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppRoutingModule } from "../app-routing.module";
import { MaterialModule } from "../modules/material.module";
import { ComponentsModule } from "../components/components.module";

import { AdminLayoutComponent } from "../layouts/admin-layout/admin-layout.component";
import { AdminNavComponent } from "../nav/admin-nav/admin-nav.component";
import { AdminHomeComponent } from './admin-home/admin-home.component';
import { AdminProductComponent } from './product/admin-product/admin-product.component';
import { AdminProductListComponent } from './product/admin-product-list/admin-product-list.component';
import { AdminProductAddComponent } from './product/admin-product-add/admin-product-add.component';
import { AdminProductUpdateComponent } from './product/admin-product-update/admin-product-update.component';

@NgModule({
  declarations: [AdminLayoutComponent, AdminNavComponent, AdminHomeComponent, AdminProductComponent, AdminProductListComponent, AdminProductAddComponent, AdminProductUpdateComponent],
  imports: [
    CommonModule, AppRoutingModule, MaterialModule, ComponentsModule
  ]
})
export class AdminModule { }
