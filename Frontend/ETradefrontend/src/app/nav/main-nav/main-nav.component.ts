import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd, } from '@angular/router';

@Component({
  selector: 'app-main-nav',
  templateUrl: './main-nav.component.html',
  styleUrls: ['./main-nav.component.css'],
})
export class MainNavComponent implements OnInit {
  pageActive?: MainPage;


  constructor(private router: Router) {
    this.router.events.subscribe((x) => {
      if (x instanceof NavigationEnd) {
        if (x.url.indexOf('anasayfa') > 0) {
          this.pageActive = MainPage.home;
        } else if (x.url.indexOf('hesabim') > 0) {
          this.pageActive = MainPage.myaccount;
        } else {
          this.pageActive = MainPage.home;
        }
      }
    });
  }

  ngOnInit(): void {}

  search(searchText:string): void | false{
    if(searchText == "" || searchText == null || searchText == undefined){
      this.router.navigateByUrl("");
    }
    else{
      this.router.navigateByUrl(`/arama?s=${searchText}`);
    }
  }

  logout(){
    localStorage.clear();
  }
}

enum MainPage {
  home = 1,
  myaccount = 2,
}
