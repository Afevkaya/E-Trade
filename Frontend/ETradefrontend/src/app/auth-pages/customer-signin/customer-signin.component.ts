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

  customerForm!: FormGroup;
  success!:boolean;
  loading!:boolean;
  info!:string;
  hide = true;

  constructor(private singinService:SigninService,private router:Router, public siginValidation:SigninvalidationService) { }

  ngOnInit(): void {
    this.customerForm = new FormGroup({
      userName: new FormControl('',Validators.required),
      email: new FormControl('',[Validators.required,Validators.email]),
      password: new FormControl('',Validators.required),
      role: new FormControl('',Validators.required)
    });
  }

  onSubmit(){
    if(this.customerForm.valid){
      this.loading = true;
      this.singinService.addCustomer(this.customerForm.value).subscribe(result=>{
        this.success = true;
        console.log(result.data);
        this.router.navigateByUrl('/hesabim');
      },error=>{
        this.success = false;
        this.info = 'bir hata meydana geldi '+error.message;
      });
    }
  }

}
