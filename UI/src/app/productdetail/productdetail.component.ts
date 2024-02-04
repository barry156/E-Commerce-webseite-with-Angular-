import { Component } from '@angular/core';
import { Product } from '../model/product_model';
import { ProductsService } from '../products.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-productdetail',
  templateUrl: './productdetail.component.html',
  styleUrls: ['./productdetail.component.scss']
})
export class ProductdetailComponent {
  product! : Product;
  productColor :string = "";
  selectedImage : string = "assets/img/product-1.jpg";

  constructor(private productsservice: ProductsService ,private router : ActivatedRoute) {}
  ngOnInit() {
  }

}
