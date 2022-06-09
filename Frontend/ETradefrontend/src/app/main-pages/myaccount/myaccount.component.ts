import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from "@angular/material/paginator";
import { MatTableDataSource } from '@angular/material/table';
import { BasketService } from 'src/app/services/basket.service';
import { ResponseBasket } from 'src/app/models/response-basket';
import { JwtHelperService } from '@auth0/angular-jwt';


@Component({
  selector: 'app-myaccount',
  templateUrl: './myaccount.component.html',
  styleUrls: ['./myaccount.component.css']
})
export class MyaccountComponent implements OnInit {

  displayColumns: string[] = ['productName','appUserName','productPrice','productQuantity','total','action'];
  dataSource:any;
  responseBaskets: ResponseBasket[]=[];
  @ViewChild(MatPaginator, {static:true}) paginator!:MatPaginator
  constructor(private basketService:BasketService, private helperService:JwtHelperService) { }

  ngOnInit(): void {
    const accessToken = localStorage.getItem('accessToken') || '';
    const tokenPayload = this.helperService.decodeToken(accessToken);
    const userId = tokenPayload.userId;
    this.basketService.getByAppUserId(userId).subscribe(result=>{
      this.responseBaskets = result.data;
      this.dataSource = new MatTableDataSource<ResponseBasket>(result.data);
      this.dataSource.paginator = this.paginator;
    });
  }

  deleteBasket(id:number){
    debugger;
    this.basketService.deleteBasket(id).subscribe(result=>{
      let reponseBasket = this.responseBaskets.filter(x=>x.id==id)[0];
      let index = this.responseBaskets.indexOf(reponseBasket);
      this.responseBaskets.splice(index,1);
      this.dataSource = new MatTableDataSource<ResponseBasket>(this.responseBaskets);
      this.dataSource.paginator = this.paginator;
    });
  }

}
