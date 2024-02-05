import { Injectable } from '@angular/core';
import { ProductInBackend, Product as ProductInbackend } from '../model/product_model';
import { ProductsService } from '../products.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthentificationService } from '../authentification.service';

//export type ProductCart = ProductInbackend ;

@Injectable({
  providedIn: 'root'
})
export class ShoppingcartService{ 
  constructor(private productService: ProductsService,private http : HttpClient , private authService: AuthentificationService) { }

  shoppingCart: ProductInBackend[] = [];
  

  removeProductFromCardInBackend(productId : number , userId: number) : Observable <any> {
    const apiUrl = `http://127.0.0.1:7136/api/ui/delete/product/all/${productId}-${userId}`;
    return this.http.delete(apiUrl);
  }

  getAllProducts(): ProductInBackend[] {
    return this.shoppingCart;
  }
  getShoppingCartLength() : number    {
    
    return this.shoppingCart.map((product) => product.amount).reduce((a, b) => a + b, 0);
  }
  addProductToCartInBackend (productId  : number , userId : number) : Observable<any>  {
    
    const apiUrl =`http://127.0.0.1:7136/api/ui/put/product/${productId}-${userId}`;
    
    return this.http.put(apiUrl, {});

  }
  getShoppingCartFromBackend()   {
    const userId = this.authService.idOfLoggedUser;
    const apiUrl =`http://127.0.0.1:7136/api/ui/get/cart/${userId}`;
   
    return this.http.get(apiUrl);

  }
  increaseProductQuantityFromBackend(productId: number , userId : number): Observable <any>   {
    const apiUrl ="";
    return this.http.post(apiUrl ,{});

  }
  decreaseProductQuantityFromBackend (productId: number , userId : number): Observable <any>   {
    const apiUrl =`http://127.0.0.1:7136/api/ui/delete/product/${productId}-${userId}`;
    return this.http.delete(apiUrl);
  
  }

}

