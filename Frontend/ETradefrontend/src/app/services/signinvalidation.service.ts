import { Injectable } from '@angular/core';
import { AbstractControl } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class SigninvalidationService {

  constructor() { }

  GetSigninValidationMessage(f: AbstractControl, arg:string):any {
    if(f.errors){
      for(let errorname in f.errors){
        if(errorname == "required"){
          return `${arg} alanı boş olamaz`;
        }
        else if(errorname == "email"){
          return `email formatı yanlış`;
        }
        else if(errorname == "minglength"){
          return `${arg} alanı en az 5 karakter olmalıdır`;
        }
        else if(errorname == "maxlength"){
          return `${arg} alanı en fazla 400 karakter olmalıdır.`
        }
      }
    }
  }
}
