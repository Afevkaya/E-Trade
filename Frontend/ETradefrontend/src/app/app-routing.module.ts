import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './auth-pages/login/login.component';
import { SigninComponent } from './auth-pages/signin/signin.component';
import { AuthLayoutComponent } from './layouts/auth-layout/auth-layout.component';
import { MainLayoutComponent } from './layouts/main-layout/main-layout.component';

const routes: Routes = [
  {
    // www.etrade.com
    path:"",
    component: MainLayoutComponent
  },

  {
    // www.etrade.com/auth
    path:"auth",
    component:AuthLayoutComponent,
    children: [
      {
        // www.etrade.com/auth
        path: "",
        component: LoginComponent,
      },

      {
        // www.etrade.com/auth/login
        path: "login",
        component: LoginComponent,
      },

      {
        // www.etrade.com/auth/signin
        path: "signin",
        component: SigninComponent
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
