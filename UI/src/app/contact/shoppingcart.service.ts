import { Injectable } from '@angular/core';
import { Product as ProductInbackend } from '../model/product_model';
import { ProductsService } from '../products.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthentificationService } from '../authentification.service';

export type ProductCart = ProductInbackend & { cartQuantity: number };

@Injectable({
  providedIn: 'root'
})
export class ShoppingcartService {
  constructor(private productService: ProductsService,private http : HttpClient , private authService: AuthentificationService) { }

  shoppingCart: ProductCart[] = [];

  addProduct(product: ProductInbackend) {
    // find the product in  the Cart
    const findProduct = this.shoppingCart.find(p => p.id === product.id);

    // if the product is in the cart , increase the quantity
    if (findProduct) {
      findProduct.cartQuantity = (findProduct.cartQuantity || 0) + 1;
    } else {
      // else add the product with an initial value of 1
      const productWithQuantity: ProductCart = { ...product, cartQuantity: 1 };
      this.shoppingCart.push(productWithQuantity);
    }
  }

  removeProduct(product: ProductInbackend) {
    const index = this.shoppingCart.findIndex(p => p === product);

    if (index !== -1) {
      this.shoppingCart.splice(index, 1);
    }
  }
  removeProductFromCardInBackend(productId : number , userId: number) : Observable <any> {
    const apiUrl = `http://127.0.0.1:7136/api/ui/put/product/${productId}-${userId}`;
    return this.http.post(apiUrl,{})
  }

  getAllProducts(): ProductCart[] {
    return this.shoppingCart;
  }
  getShoppingCartLength() : number    {
    
    return this.shoppingCart.map((product : ProductCart) => product.cartQuantity).reduce((a, b) => a + b, 0);



  }
  addProductToCartInBackend (productId  : number , userId : number) : Observable<any>  {
    
    const apiUrl =`http://127.0.0.1:7136/api/ui/put/product/${productId}-${userId}`;
    
    return this.http.put(apiUrl, {});

  }
  getShoppingCartFromBackend()   {
    const userId = this.authService.idOfLoggedUser;
    const apiUrl =`http://127.0.0.1:7136/api/ui/get/cart/${userId}`;
   
    return this.http.get<{id : number , products: ProductInbackend[]}>(apiUrl);

  }
  
}

