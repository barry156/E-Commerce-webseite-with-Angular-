import { Injectable } from '@angular/core';Categorie
import { Categorie } from './model/categorie_model'

@Injectable({
  providedIn: 'root'
})
export class CategoriesService {
  



  categorie : Categorie [] =  [
    {
      name : " Men's Dresses",
      quantity : 15,
      imagePath: "assets/img/cat-1.jpg"
    },
    {
      name : " Womens's Dresses",
      quantity : 15,
      imagePath: "assets/img/cat-2.jpg"
    },
    {
      name : " Baby's Dresses",
      quantity : 15,
      imagePath: "assets/img/cat-3.jpg"
    },
    {
      name : "Accessoires",
      quantity : 15,
      imagePath: "assets/img/cat-4.jpg"
    },
    {
      name : "Bags",
      quantity : 15,
      imagePath: "assets/img/cat-5.jpg"
    },
    {
      name : "Shoes",
      quantity : 15,
      imagePath: "assets/img/cat-6.jpg"
    },
  ]
}

