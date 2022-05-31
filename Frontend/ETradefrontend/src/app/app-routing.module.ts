import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './auth-pages/login/login.component';
import { SigninComponent } from './auth-pages/signin/signin.component';
import { AuthLayoutComponent } from './layouts/auth-layout/auth-layout.component';
import { MainLayoutComponent } from './layouts/main-layout/main-layout.component';
import { HomeComponent } from './main-pages/home/home.component';
import { MyaccountComponent } from './main-pages/myaccount/myaccount.component';
import { ProductComponent } from './main-pages/product/product.component';

// Url deki yazana göre gösterilecek componentleri belirlediğimiz router mekanizması.
const routes: Routes = [

  // www.etrade.com
  {path: "", component: MainLayoutComponent,
    children: [

      // www.etrade.com
      {path: "", component: HomeComponent},

      // www.etrade.com/hesabim
      {path: "hesabim", component: MyaccountComponent},

      {path:"ürün/:id", component: ProductComponent}
    ]
  },

  // www.etrade.com/auth
  {path: "auth", component:AuthLayoutComponent,
    children: [

      // www.etrade.com/auth
      {path: "",component: LoginComponent},

      // www.etrade.com/auth/login
      {path: "login",component: LoginComponent},

      // www.etrade.com/auth/signin
      {path: "signin",component: SigninComponent},
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
