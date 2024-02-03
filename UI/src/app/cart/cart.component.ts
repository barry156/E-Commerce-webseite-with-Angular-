import { Component, DoCheck } from '@angular/core';
import { ProductCart, ShoppingcartService } from '../contact/shoppingcart.service';
import { Product } from '../model/product_model';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';



@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements DoCheck {
  subTotal!: number;
  shipping = 10;

  products: ProductCart[] = [];

  constructor(private shoppingCartService: ShoppingcartService, private router: Router) {}

  ngDoCheck() {
   this.products = this.shoppingCartService.getAllProducts();
   this.shoppingCartService.getShoppingCartFromBackend()
      .subscribe((data) => {
       // this.products = data.products;
        
      });
    this.subTotal = this.calculateTotal(this.products);
    
    
  }

  remove(product: Product) {
    this.shoppingCartService.removeProduct(product);
  }

  decreaseQuantity(product: ProductCart) {
    if (product.cartQuantity && product.cartQuantity > 1) {
      product.cartQuantity--;
    }
  }

  increaseQuantity(product: ProductCart) { 
    if(product.cartQuantity ) {
      product.cartQuantity++;
    }
  }
  calculateTotal(productCart: ProductCart[]): number {
    return productCart.reduce((total, product) => {
      return total + (product.price * (product.cartQuantity || 0));
    }, 0);
  }

  navigateToPayPal() {
    this.router.navigateByUrl('/paypal');

  }
}
