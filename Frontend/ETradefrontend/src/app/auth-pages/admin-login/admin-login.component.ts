import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Login } from 'src/app/models/login';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-admin-login',
  templateUrl: './admin-login.component.html',
  styleUrls: ['./admin-login.component.css']
})
export class AdminLoginComponent implements OnInit {

  loginModel!:Login;
  constructor(private authService:AuthService,private router:Router) { }

  ngOnInit(): void {
  }

  login(email:string,password:string){
    this.loginModel = new Login();
    this.loginModel.email = email;
    this.loginModel.password = password;

    this.authService.createToken(this.loginModel).subscribe(result=>{
      if(result.data != null){
        localStorage.setItem("accessToken",result.data.accessToken);
        localStorage.setItem("refreshToken",result.data.refreshToken);
        localStorage.setItem("accessTokenExpiration",result.data.accessTokenExpiration);
        localStorage.setItem("refreshTokenExpiration",result.data.refreshTokenExpiration);
        this.router.navigate(["/admin"]);
      }
    });
  }
}
