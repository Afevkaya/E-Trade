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
}

enum MainPage {
  home = 1,
  myaccount = 2,
}
