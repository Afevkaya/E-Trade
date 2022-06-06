import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminHomeComponent } from './admin-pages/admin-home/admin-home.component';
import { AdminProductAddComponent } from './admin-pages/product/admin-product-add/admin-product-add.component';
import { AdminProductListComponent } from './admin-pages/product/admin-product-list/admin-product-list.component';
import { AdminProductUpdateComponent } from './admin-pages/product/admin-product-update/admin-product-update.component';
import { AdminProductComponent } from './admin-pages/product/admin-product/admin-product.component';
import { AdminLoginComponent } from './auth-pages/admin-login/admin-login.component';
import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';
import { AuthLayoutComponent } from './layouts/auth-layout/auth-layout.component';
import { MainLayoutComponent } from './layouts/main-layout/main-layout.component';
import { CategoryProductsComponent } from './main-pages/category-products/category-products.component';
import { HomeComponent } from './main-pages/home/home.component';
import { MyaccountComponent } from './main-pages/myaccount/myaccount.component';
import { ProductComponent } from './main-pages/product/product.component';
import { SearchComponent } from './main-pages/search/search.component';

// Url deki yazana göre gösterilecek componentleri belirlediğimiz router mekanizması.
const routes: Routes = [
  // www.etrade.com
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      // www.etrade.com
      { path: '', component: HomeComponent },

      // www.etrade.com/hesabim
      { path: 'hesabim', component: MyaccountComponent },

      { path: 'ürün/:name/:id', component: ProductComponent },

      { path: 'kategori/:name/:id', component: CategoryProductsComponent },

      { path: 'arama', component: SearchComponent },
    ],
  },
  {
    // www.etrade.com/admin
    path: 'admin',
    component: AdminLayoutComponent,
    children: [
      // www.etrade.com/admin
      { path: '', component: AdminHomeComponent },

      // www.etrade.com/admin/anasayfa
      { path: 'anasayfa', component: AdminHomeComponent },

      {
        // www.etrade.com/admin/urun
        path: 'urun',
        component: AdminProductComponent,
        children: [
          // www.etrade.com/admin/urun/ekle
          { path: 'ekle', component: AdminProductAddComponent },

          // www.etrade.com/admin/urun/guncelle/2
          { path: 'guncelle/:id', component: AdminProductUpdateComponent },

          // www.etrade.com/admin/urun/liste
          { path: 'liste', component: AdminProductListComponent },
        ],
      },
    ],
  },

  // www.etrade.com/auth
  {
    path: 'auth',
    component: AuthLayoutComponent,
    children: [
      // www.etrade.com/auth
      { path: '', component: AdminLoginComponent },

      // www.etrade.com/auth/adminlogin
      { path: 'adminlogin', component: AdminLoginComponent },

      // www.etrade.com/auth/adminlogin
      { path: 'customerlogin', component: AdminLoginComponent },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
