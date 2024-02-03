import { Component , ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { Categorie } from '../model/categorie_model';
import { CategoriesService } from '../categories.service';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.scss']
})
export class CategoriesComponent {
  @Input() categories ! : Categorie[];
  @ViewChild('categorieLink') categorieLink! : ElementRef;
  
  constructor(public router : Router , private categoriesService : CategoriesService) {}

  ngOnInit() : void  {
    this.categories = this.categoriesService.categorie;
  }

  handelClikOnCategorie(event : Event)  { 
    event.preventDefault();
    this.router.navigateByUrl('shop');
    
  }


}
