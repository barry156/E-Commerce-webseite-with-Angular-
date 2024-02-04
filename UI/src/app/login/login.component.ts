import { Component, OnInit } from '@angular/core';
import { AuthentificationService } from '../authentification.service';
import { NgForm } from '@angular/forms';
import { first } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent  {
  constructor(private authService : AuthentificationService , private router: Router) {}
  email! : string;
  password!: string;
  

  onSubmit(f: NgForm) {
    
    this.authService.login(f.value).subscribe( {
      next: (response) =>  {
        
        console.log(response);
       this.authService.userIsLogged = true; 
       this.authService.idOfLoggedUser = response;
       
       this.router.navigateByUrl('/shop');
        
        console.log("login  is successfull");
      },
      error : (error) =>   {
        console.log('an error occurs during the registration');
      }

    });;
      
  }
  
  }

