import { Injectable } from '@angular/core';
import { Product, ProductInBackend } from './model/product_model';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {
  constructor (private http: HttpClient) {}

/*products : Product[] = [
  {
    id : '1',
    name : 'product 1',
    price : 100 ,
    color : ['red' ,'black' ,'white'],
    size : ['S'] ,
    url : 'assets/img/product-1.jpg',
    galerie : {
      red : 'assets/img/product-1.jpg',
      black : 'assets/img/product-2.jpg',
      white : 'assets/img/product-3.jpg'
    }
  },
  {
    id : '2',
    name : 'product 1',
    price : 99 ,
    color : ['red'],
    size : ['S'] ,
    url : 'assets/img/product-2.jpg',
    galerie : {
      red : 'assets/img/product-1.jpg'
    }
    
  },
  {
    id : '3',
    name : 'product 1',
    price : 123 ,
    color : ['red'],
    size : ['S'] ,
    url : 'assets/img/product-3.jpg',
    galerie : {
      red : 'assets/img/product-1.jpg'
    }
    
  },
  {
    id : '4',
    name : 'product 1',
    price : 123 ,
    color : ['red'],
    size : ['S'] ,
    url : 'assets/img/product-4.jpg',
    galerie : {
      red : 'assets/img/product-1.jpg'
    }
  },
  {
    id : '5',
    name : 'product 1',
    price : 123 ,
    color : ['black'],
    size : ['S'] ,
    url : 'assets/img/product-5.jpg',
    galerie : {
      red : 'assets/img/product-1.jpg'
    }
  },
  {
    id : '6',
    name : 'product 1',
    price : 123 ,
    color : ['red'],
    size : ['S'] ,
    url : 'assets/img/product-6.jpg',
    galerie : {
      red : 'assets/img/product-1.jpg'
    }
  },
]*/
products : ProductInBackend [] = [];

    

/*getAllProducts() : Product []  {
  return this.products
}*/

getProductById(productId: number): ProductInBackend {
    const product = this.products.find(product => product.id == productId);
    if(!product) {
      throw new Error('Product not found!');
    } else {
      return product ;
    }

}
getProductsFromBackend() {
  const apiUrl = "http://127.0.0.1:7136/api/ui/get/products";
  return this.http.get <ProductInBackend[]>(apiUrl);


}

}

