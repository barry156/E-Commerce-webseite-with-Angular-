import { Component } from '@angular/core';
import { AuthentificationService } from '../authentification.service';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  constructor(private authService : AuthentificationService,private router: Router)  {}

  oneSubmit(f: NgForm)   {
    
    this.authService.register(f.value).subscribe( {
      next: (response) => {
        console.log("registration successfull");
        this.router.navigateByUrl('/login');
      },
      error :(error) =>  {
        console.error('Erreur lors de l\'enregistrement de l\'utilisateur:', error);
       
      }

    })

  }

}
