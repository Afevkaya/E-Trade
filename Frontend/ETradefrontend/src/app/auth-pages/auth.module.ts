import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppRoutingModule } from "../app-routing.module";
import { MaterialModule } from "../modules/material.module";


import { AuthLayoutComponent } from '../layouts/auth-layout/auth-layout.component';
import { AdminLoginComponent } from './admin-login/admin-login.component';
import { ComponentsModule } from '../components/components.module';



@NgModule({
  declarations: [
    AuthLayoutComponent,
    AdminLoginComponent,
  ],
  imports: [
    CommonModule,
    AppRoutingModule,
    MaterialModule,
    ComponentsModule
  ],
  exports:[
  ]
})
export class AuthModule { }
