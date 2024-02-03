
import { Component, Input } from '@angular/core';
import { Product as ProductInBackend } from '../model/product_model';
import { ProductsService } from '../products.service';
import { Router } from '@angular/router';
import { ShoppingcartService } from '../contact/shoppingcart.service';
import { AuthentificationService } from '../authentification.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent {
  @Input() products!: ProductInBackend[];
  filteredProducts: ProductInBackend[] = [];
  changeResult  = 29;

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
        this.products = data;
        
      });
    
    this.filteredProducts = [...this.products];
    const priceRange = document.getElementById('priceRange') as HTMLInputElement;
    const priceValue = document.getElementById('priceValue') as HTMLSpanElement;
   
  }

  onViewProductDetail(productId: string, event: Event) {
    event.preventDefault();
    this.router.navigateByUrl('productdetail/' + productId);
  }

  onPriceRangeChange(event: Event) {
    
  
    this.products = this.products
      .filter(product => (event.target as HTMLInputElement)?.value && product.price < parseInt((event.target as HTMLInputElement).value));
  
    /*if (this.checkedColors && this.checkedColors.length > 0) {
      this.products = this.products.filter(product => this.checkedColors.includes(product.color[0]) && product.price <= this.changeResult);
    }*/
  }
  
  

 /*onColorCheckBoxChange(event: Event) {
    const selectedColorId = (event.target as HTMLElement)?.id;
    const findIndexOfColorSelected = this.colorFilter.findIndex(color => color.id === selectedColorId);
  
    if (findIndexOfColorSelected !== -1) {
      this.colorFilter[findIndexOfColorSelected].check = (event.target as HTMLInputElement)?.checked;
    }
    this.checkedColors = this.colorFilter.filter(color =>color.check).map(color =>color.name);
   
  
    this.products = this.productsService.products.filter(product => this.checkedColors.includes(product.color[0]) && product.price <= this.changeResult);
  }*/
  addProductToShoppingCart(product: ProductInBackend , event : Event)  {
    event.preventDefault();
    this.shoppingCartService.addProduct(product);
    console.log(this.shoppingCartService.getAllProducts());

  }
  addProductToShoppingCartInBackend(product : ProductInBackend , event : Event)   {
  
    event.preventDefault();
    this.shoppingCartService.addProductToCartInBackend(product.id , this.authService.idOfLoggedUser).subscribe(
      (response) =>  {
        alert('product added in the cart successfully');

      },
      (error) =>  {
        console.log("error when adding the product in the cart");
      }
    );

  }

  
}

