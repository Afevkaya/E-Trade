import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { MenuCategoryComponent } from './menu-category/menu-category.component';
import { PageTitleComponent } from './page-title/page-title.component';
import { ProductsComponent } from './products/products.component';
import { UrlformatPipe } from '../pipes/urlformat.pipe';

@NgModule({
  declarations: [MenuCategoryComponent, PageTitleComponent, ProductsComponent, UrlformatPipe],
  imports: [CommonModule, RouterModule],
  exports: [MenuCategoryComponent, PageTitleComponent, ProductsComponent, UrlformatPipe],
})
export class ComponentsModule {}
