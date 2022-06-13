import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from "@angular/material/paginator";
import { MatTableDataSource } from '@angular/material/table';
import { BasketService } from 'src/app/services/basket.service';
import { ResponseBasket } from 'src/app/models/response-basket';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgbModal,ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';


@Component({
  selector: 'app-myaccount',
  templateUrl: './myaccount.component.html',
  styleUrls: ['./myaccount.component.css']
})
export class MyaccountComponent implements OnInit {

  displayColumns: string[] = ['productName','appUserName','productPrice','productQuantity','total','action'];
  dataSource:any;
  responseBaskets: ResponseBasket[]=[];
  closeResult = '';
  @ViewChild(MatPaginator, {static:true}) paginator!:MatPaginator
  constructor(private basketService:BasketService, private helperService:JwtHelperService, private modalService:NgbModal) { }

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
    this.basketService.deleteBasket(id).subscribe(result=>{
      let reponseBasket = this.responseBaskets.filter(x=>x.id==id)[0];
      let index = this.responseBaskets.indexOf(reponseBasket);
      this.responseBaskets.splice(index,1);
      this.dataSource = new MatTableDataSource<ResponseBasket>(this.responseBaskets);
      this.dataSource.paginator = this.paginator;
    });
  }

  // open(responseBaskets:ResponseBasket) {
  //   debugger;
  //   this.modalService.open(responseBaskets, {ariaLabelledBy: 'modal-basic-title'}).result.then((result) => {
  //     this.closeResult = `Closed with: ${result}`;
  //   }, (reason) => {
  //     this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
  //   });
  // }

  // private getDismissReason(reason: any): string {
  //   if (reason === ModalDismissReasons.ESC) {
  //     return 'by pressing ESC';
  //   } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
  //     return 'by clicking on a backdrop';
  //   } else {
  //     return `with: ${reason}`;
  //   }
  // }

}
