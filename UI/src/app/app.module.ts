import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TopbarComponent } from './topbar/topbar.component';
import { NavbarComponent } from './navbar/navbar.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CarousselComponent } from './caroussel/caroussel.component';
import { CategoriesComponent } from './categories/categories.component';
import { ProductsComponent } from './products/products.component';
import { FooterComponent } from './footer/footer.component';
import { ShopComponent } from './shop/shop.component';
import { HomeComponent } from './home/home.component';
import { ContactComponent } from './contact/contact.component';
import { CartComponent } from './cart/cart.component';
import { LoginComponent } from './login/login.component';
import { ProductdetailComponent } from './productdetail/productdetail.component';
import { FormsModule, NgForm, NgModel, ReactiveFormsModule } from '@angular/forms';
import { PaypalComponent } from './paypal/paypal.component';
import { HttpClientModule } from '@angular/common/http';
import { AuthentificationService } from './authentification.service';
import { RegisterComponent } from './register/register.component';
import { ProductsService } from './products.service';
import { SearchProductPipe } from './search-product.pipe';


@NgModule({
  declarations: [
    AppComponent,
    TopbarComponent,
    NavbarComponent,
    CarousselComponent,
    CategoriesComponent,
    ProductsComponent,
    FooterComponent,
    ShopComponent,
    HomeComponent,
    ContactComponent,
    CartComponent,
    LoginComponent,
    ProductdetailComponent,
    PaypalComponent,
    RegisterComponent,
    SearchProductPipe
    
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule
    
  ],
 
  providers: [ProductsService],
  bootstrap: [AppComponent]
})
export class AppModule { }
