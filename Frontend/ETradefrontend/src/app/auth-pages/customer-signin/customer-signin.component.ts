import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { SigninService } from 'src/app/services/signin.service';
import { SigninvalidationService } from 'src/app/services/signinvalidation.service';

@Component({
  selector: 'app-customer-signin',
  templateUrl: './customer-signin.component.html',
  styleUrls: ['./customer-signin.component.css']
})
export class CustomerSigninComponent implements OnInit {

  // Property
  customerForm!: FormGroup;
  success!:boolean;
  loading!:boolean;
  info!:string;
  hide = true;

  // Kullanılacak serviceler ve moduller
  constructor(private singinService:SigninService,private router:Router, public siginValidation:SigninvalidationService) { }

  // Reactive form
  ngOnInit(): void {
    this.customerForm = new FormGroup({
      userName: new FormControl('',Validators.required),
      email: new FormControl('',[Validators.required,Validators.email]),
      password: new FormControl('',Validators.required),
      role: new FormControl('',Validators.required)
    });
  }

  // Kaydet
  onSubmit(){
    if(this.customerForm.valid){
      this.loading = true;
      this.singinService.addCustomer(this.customerForm.value).subscribe(result=>{
        this.success = true;
        console.log(result.data);
        this.router.navigateByUrl('/customerlogin');
      },error=>{
        this.success = false;
        this.info = 'bir hata meydana geldi '+error.message;
      });
    }
  }

}
