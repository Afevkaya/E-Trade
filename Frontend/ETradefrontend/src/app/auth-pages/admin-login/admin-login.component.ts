import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { SigninvalidationService } from 'src/app/services/signinvalidation.service';

@Component({
  selector: 'app-admin-login',
  templateUrl: './admin-login.component.html',
  styleUrls: ['./admin-login.component.css']
})
export class AdminLoginComponent implements OnInit {

  // propertyler
  loginForm!:FormGroup;
  loading!:boolean;
  success!:boolean;
  info!:string;
  hide = true;

  // Kullanılacak serviceler ve moduller
  constructor(private authService:AuthService,private router:Router, public signinValidation:SigninvalidationService) { }

  ngOnInit(): void {
    // Reactive form
    this.loginForm = new FormGroup({
      email: new FormControl('',[Validators.required,Validators.email]),
      password: new FormControl('',Validators.required)
    })
  }

  login(){
    // form valid kontrol
    if(this.loginForm.valid){
      this.loading = true;
      // login ol
      this.authService.createToken(this.loginForm.value).subscribe(result=>{
        if(result.data != null){
          localStorage.setItem("accessToken",result.data.accessToken);
          localStorage.setItem("refreshToken",result.data.refreshToken);
          localStorage.setItem("accessTokenExpiration",result.data.accessTokenExpiration);
          localStorage.setItem("refreshTokenExpiration",result.data.refreshTokenExpiration);
          this.router.navigate(["/admin"]);
        }
      },error =>{
        this.success = false;
        this.info = 'Bir hata meydana geldi '+error.message;
      });
    }
  }
}
