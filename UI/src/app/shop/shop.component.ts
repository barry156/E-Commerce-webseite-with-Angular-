
import { Component, Input } from '@angular/core';
import { Product as ProductInBackend } from '../model/product_model';
import { ProductsService } from '../products.service';
import { Router } from '@angular/router';
import { ShoppingcartService } from '../contact/shoppingcart.service';
import { AuthentificationService } from '../authentification.service';
import { SearchProductPipe } from '../search-product.pipe';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent {
  @Input() products!: ProductInBackend[];
  filteredProducts: ProductInBackend[] = [];
  changeResult  = 29;
  searchText: string = '';

  colorFilter: { id: string ,name : string, check: boolean}[] = [
    { id: 'color-1' , name: 'black', check : false },
    { id: 'color-2' , name :'white', check :false},
    { id: 'color-3', name : 'red' , check : false},
    
  ];
  checkedColors : string [] = [];
  

  constructor(private productsService: ProductsService, 
    private router: Router ,private shoppingCartService : ShoppingcartService,
    private authService : AuthentificationService) {}

  ngOnInit() {
    
    this.productsService.getProductsFromBackend()
      .subscribe((data) => {
        console.log(data);
        this.products = data;
        
      });
   
  }

  onViewProductDetail(productId: string, event: Event) {
    event.preventDefault();
    this.router.navigateByUrl('productdetail/' + productId);
  }

  onPriceRangeChange(event: Event) {
    
  
    this.products = this.products
      .filter(product => (event.target as HTMLInputElement)?.value && product.price < parseInt((event.target as HTMLInputElement).value));
  
  }
  
  addProductToShoppingCartInBackend(product : ProductInBackend , event : Event)   {
    
  
    event.preventDefault();
    this.shoppingCartService.addProductToCartInBackend(product.id , this.authService.idOfLoggedUser).subscribe(
      (response) =>  {
        console.log("product added in the cart successfully");
       // this.shoppingCartService.shoppingCart.length ++;

      },
      (error) =>  {
        console.log("error when adding the product in the cart");
      }
    );

  }

  
}

