import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ShopComponent } from './shop/shop.component';
import { HomeComponent } from './home/home.component';
import { ContactComponent } from './contact/contact.component';
import { CartComponent } from './cart/cart.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { CategoriesComponent } from './categories/categories.component';
import { ProductdetailComponent } from './productdetail/productdetail.component';
import { PaypalComponent } from './paypal/paypal.component';
import { FormsModule } from '@angular/forms';

const routes: Routes = [
  {path: '', redirectTo: 'shop' , pathMatch: 'full'},
  {path:'shop', component: ShopComponent },
  {path:'home', component: HomeComponent},
  {path: 'contact', component : ContactComponent},
  {path: 'cart', component: CartComponent},
  {path : 'login', component: LoginComponent},
  {path : 'register',  component : RegisterComponent},
  {path : 'categorie', component: CategoriesComponent},
  {path : 'productdetail/:id', component: ProductdetailComponent},
  {path:'paypal', component: PaypalComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes), FormsModule],
  exports: [RouterModule]
})
export class AppRoutingModule { }
