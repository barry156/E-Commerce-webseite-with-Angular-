import { Component, DoCheck, OnInit } from '@angular/core';
import { ShoppingcartService } from '../contact/shoppingcart.service';
import { Product, ProductInBackend, ShoppingCartResponse } from '../model/product_model';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { AuthentificationService } from '../authentification.service';



@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {
  subTotal!: number;
  shipping = 10;

  products: ProductInBackend[] = [];

  constructor(private shoppingCartService: ShoppingcartService, private router: Router,
    private authService: AuthentificationService) {}

  ngOnInit() {
      this.shoppingCartService.getShoppingCartFromBackend().subscribe(
        (response: any) => {
          this.products = response?.products;
          this.subTotal = response?.total_price;
        },
        (error) =>  {
          console.error('Error:', error);
        }
      );
       
  }
  //code for the backend
  removeProductFromCardInBackend(product: Product) {
    const userId = this.authService.idOfLoggedUser;
    this.shoppingCartService.removeProductFromCardInBackend(product.id , userId).subscribe(
      {
        next: (response) => {
          this.ngOnInit();
         
          console.log(response);
          
        },
        error :(error) =>  {
          console.error('Error :', error);
         
        }
  
      }

    )

  }

  increaseProductQuantityFromBackend(product : Product)  {
    const userId = this.authService.idOfLoggedUser;
    this.shoppingCartService.addProductToCartInBackend(product.id , userId).subscribe(
      {
        next: (response) => {
          this.ngOnInit();
          
          console.log(response);
          
        },
        error :(error) =>  {
          console.error('Error :', error);
         
        }
  
      }

    )

  }
  decreaseProductQuantityFromBackend(product : Product)  {
    const userId = this.authService.idOfLoggedUser;
    this.shoppingCartService.decreaseProductQuantityFromBackend(product.id , userId).subscribe(
      {
        next: (response) => {
          this.ngOnInit();
          console.log(response);
          
        },
        error:(error) =>  {
          console.error('Error :', error);
         
        }
  
      }

    )

  }

  decreaseQuantity(product: ProductInBackend) {
    if (product.amount && product.amount > 1) {
      product.amount--;
    }
  }

  increaseQuantity(product: ProductInBackend) { 
    if(product.amount ) {
      product.amount++;
    }
  }
  calculateTotal(productCart: ProductInBackend[]): number {
    return productCart.reduce((total, product) => {
      return total + (product.price * (product.amount || 0));
    }, 0);
  }

  navigateToPayPal() {
    this.router.navigateByUrl('/paypal');

  }
}
