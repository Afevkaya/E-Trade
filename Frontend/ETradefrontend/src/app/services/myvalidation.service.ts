import { Injectable } from '@angular/core';
import { AbstractControl } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class MyvalidationService {

  constructor() { }

  GetValidationMessage(f: AbstractControl, name:string):any{
    if (f.errors) {
      for (let errorname in f.errors) {
        if (errorname == "required") {
          return `${name} alanı boş bırakılamaz`;
        }
        else if(errorname == "email"){
          return `email formatı yanlış`;
        }
        else if(errorname == "minlength"){
          return `${name} alanı en az 5 karakter olmalıdır`;
        }
        else if(errorname == "maxlength"){
          return `${name} alanı en fazla ${String.length} karakter olmalıdır.`
        }
        else if(errorname == "min"){
          return `${name} değeri en az 1 olmalıdır.`
        }
        else if(errorname == "maxlength"){
          return `${name} değeri en fazla ${Number.MAX_SAFE_INTEGER} olmalıdır.`
        }
        else if(errorname == "pattern"){
          return `${name} sayı olmalıdır.`;
        }

      }
    }
  }
}
