import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { SigninvalidationService } from 'src/app/services/signinvalidation.service';

@Component({
  selector: 'app-customer-login',
  templateUrl: './customer-login.component.html',
  styleUrls: ['./customer-login.component.css'],
})
export class CustomerLoginComponent implements OnInit {

  // propertyler
  loginForm!: FormGroup;
  loading!: boolean;
  success!: boolean;
  info!: string;
  hide = true;

  // Kullanılacak serviceler ve moduller
  constructor(
    private authService: AuthService,
    public signinValidation: SigninvalidationService,
    private router:Router
  ) {}


  ngOnInit(): void {
    // Reactive form
    this.loginForm = new FormGroup({
      email: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
    });
  }

  onSubmit() {
    // login ol
    if (this.loginForm.valid) {
      this.loading = true;
      this.authService.createToken(this.loginForm.value).subscribe((result) => {
        if (result.data != null) {
          localStorage.setItem('accessToken', result.data.accessToken);
          localStorage.setItem('refreshToken', result.data.refreshToken);
          localStorage.setItem('accessTokenExpiration', result.data.accessTokenExpiration);
          localStorage.setItem('refreshTokenExpiration', result.data.refreshTokenExpiration);
          this.router.navigate(['/']);
          this.success = true;
        }
      },error=>{
        this.success = false;
        this.info = 'Bir hata meydana geldi ' +error.message;
      });
    }
  }
}
