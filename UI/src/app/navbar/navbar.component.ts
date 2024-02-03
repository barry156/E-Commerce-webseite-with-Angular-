import { Component, DoCheck, OnDestroy } from '@angular/core';
import { CarousselAndSidebarVisibilityService } from 'src/caroussel-and-sidebar-visibility.service';
import { Subscription } from 'rxjs';
import { ViewChild, ElementRef } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { AuthentificationService } from '../authentification.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})

export class NavbarComponent implements DoCheck  {
  showCaroussel : boolean = false;
  isUserLogged : boolean = true;
  @ViewChild('caroussel') caroussel! : ElementRef;
  @ViewChild('nav') nav! : ElementRef;
  constructor(private router: Router , private authService : AuthentificationService) {}
  
ngDoCheck() {
  this.router.events.subscribe((event)=> {
    if(event instanceof NavigationEnd) {
      this.showCaroussel = (event.urlAfterRedirects === '/home');
    }
  });
  this.isUserLogged = this.authService.userIsLogged;
}


  hideElementInOtherComponent() {
    /*const categoriesToggle = document.querySelector('#navbar-vertical');
    if(this.showCaroussel) {
      this.showCaroussel = false;
      categoriesToggle?.classList.toggle('show');
      this.caroussel.nativeElement.style.display = 'none';
    }*/
  }
  deconnexion()  {
    this.authService.userIsLogged = false;
    this.router.navigateByUrl('/login');
  }
}
