import { Component, DoCheck, OnInit } from '@angular/core';
import { ShoppingcartService } from '../contact/shoppingcart.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-topbar',
  templateUrl: './topbar.component.html',
  styleUrls: ['./topbar.component.scss']
})
export class TopbarComponent implements DoCheck {
  shoppingCartLength! : number ;
  constructor(private shoppingCartService: ShoppingcartService , private router : Router) {}
  ngDoCheck()  {
    this.shoppingCartLength = this.shoppingCartService.getShoppingCartLength();

  }
  navigateToShoppingCart(event : Event)   {
    event.preventDefault();
    this.router.navigateByUrl('/cart');
  }

}
